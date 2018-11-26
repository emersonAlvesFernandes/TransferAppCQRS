using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using TransferAppCQRS.WriteNoSql;

namespace TransferAppCQRS.WriteNoSql
{
    class Program
    {
        public static void Main()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true);

            IConfiguration Configuration = builder.Build();

            var _hostName = Configuration["RabbitMq:Hostname"];
            var _port = string.IsNullOrEmpty(Configuration["RabbitMq:Port"]) ? 5672 : Convert.ToInt32(Configuration["RabbitMq:Port"]);
            var _username = Configuration["RabbitMq:UserName"];
            var _password = Configuration["RabbitMq:Password"];
            var _exchange = Configuration["RabbitMq:Exchange"];
            var _routingKey = Configuration["RabbitMq:RoutingKey"];
            var _type = Configuration["RabbitMq:Type"];

            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                Port = _port,
                UserName = _username,
                Password = _password,
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: _exchange, type: _type, durable: true);

                // var queueName = channel.QueueDeclare().QueueName;
                var queueName = "customerQ";

                channel.QueueBind(queue: queueName,
                                  exchange: _exchange,
                                  routingKey: _routingKey);

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    //requeue
                    try
                    {
                        String jsonified = Encoding.UTF8.GetString(ea.Body);
                        var message = (Customer)JsonConvert.DeserializeObject<Customer>(jsonified);

                        // var body = ea.Body;
                        // var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] {0}", message.Name);

                        message.Save(Configuration);

                        var age = new Age(message);

                        age.Save(Configuration, message);

                        channel.BasicAck(ea.DeliveryTag, true); // Manual Ack

                    }
                    catch (Exception ex)
                    {
                        // requeue the delivery
                        channel.BasicReject(ea.DeliveryTag, true);
                        
                        // requeue all unacknowledged deliveries up to
                        // this delivery tag
                        //channel.BasicNack(ea.DeliveryTag, true, true);
                    }
                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: false, // Manual Ack
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");

                Console.ReadLine();
            }
        }
    }
}
