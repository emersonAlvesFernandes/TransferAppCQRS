using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using TransferAppCQRS.Domain.Core.Bus;

namespace TransferAppCQRS.Infra.CrossCutting.QueueManager
{
    public class QueueManager : IQueueManager
    {
        private readonly string _hostname;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly string _exchange;
        private string _routingKey;
        private readonly string _type;

        public QueueManager(IConfiguration _config)
        {
            _hostname = _config["RabbitMq:Hostname"];
            _port = string.IsNullOrEmpty(_config["RabbitMq:Port"]) ? 5672 : Convert.ToInt32(_config["RabbitMq:Port"]);
            _username = _config["RabbitMq:UserName"];
            _password = _config["RabbitMq:Password"];
            _exchange = _config["RabbitMq:Exchange"];
            _routingKey = _config["RabbitMq:RoutingKey"];
            _type = _config["RabbitMq:Type"];
        }

        public void Publish<T>(T value, string routingKey) where T : class
        {
            _routingKey = routingKey;
            //var factory = new ConnectionFactory()
            //{
            //    HostName = "localhost",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest",
            //};

            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                Port = _port,
                UserName = _username,
                Password = _password,
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: _exchange, type: _type, durable: true);
                
                var message = JsonConvert.SerializeObject(value);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: _exchange,
                                     routingKey: _routingKey,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($" [x] Sent {message}");
            }
        }
    }
}
