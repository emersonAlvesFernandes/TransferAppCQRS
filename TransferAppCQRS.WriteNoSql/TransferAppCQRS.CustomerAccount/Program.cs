using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TransferAppCQRS.CustomerAccount.model;

namespace TransferAppCQRS.CustomerAccount
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
                var queueName = "accountQ";

                channel.QueueBind(queue: queueName,
                                  exchange: _exchange,
                                  routingKey: _routingKey);

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    String jsonified = Encoding.UTF8.GetString(ea.Body);
                    var message = (Account)JsonConvert.DeserializeObject<Account>(jsonified);

                    // var body = ea.Body;
                    // var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] {0}", message.Agency);

                    message.Save(Configuration);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
