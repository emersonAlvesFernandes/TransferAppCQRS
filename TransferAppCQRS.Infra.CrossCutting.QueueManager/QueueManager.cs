using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using TransferAppCQRS.Domain.Core.Bus;

namespace TransferAppCQRS.Infra.CrossCutting.QueueManager
{
    public class QueueManager : IQueueManager
    {
        public void Publish<T>(T value) where T : class
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost", //rabbitMQConfigurations.HostName,
                Port = 5672,            //rabbitMQConfigurations.Port,
                UserName = "guest",     //rabbitMQConfigurations.UserName,
                Password = "guest",     //rabbitMQConfigurations.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                //var message = GetMessage(value);
                var message = JsonConvert.SerializeObject(value);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "logs",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($" [x] Sent {message}");                
            }            
        }
    }
}
