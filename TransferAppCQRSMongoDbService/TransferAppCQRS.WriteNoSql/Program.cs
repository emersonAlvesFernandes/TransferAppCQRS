using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using TransferAppCQRS.model;

namespace TransferAppCQRS.WriteNoSql
{
    class Program
    {
        public static void Main()
        {
            //var factory = new ConnectionFactory() { HostName = "localhost" };

            var factory = new ConnectionFactory()
            {
                HostName = "localhost", 
                Port = 5672, 
                UserName = "guest",
                Password = "guest", 
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                  exchange: "logs",
                                  routingKey: "");

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    String jsonified = Encoding.UTF8.GetString(ea.Body);
                    var message = (Customer)JsonConvert.DeserializeObject<Customer>(jsonified);

                    // var body = ea.Body;
                    // var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] {0}", message.Name);
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
