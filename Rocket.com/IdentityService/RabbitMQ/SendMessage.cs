using System;
using RabbitMQ.Client;
using System.Text;
using IdentityServerHost.Quickstart.UI;

namespace IdentityServerHost.Workers
{
    public class SendMessage
    {
        public void Send(string Message)
        {
            var factory = new ConnectionFactory() { 
                HostName = "localhost",
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            {
                {
                    channel.QueueDeclare(queue: "Login",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = Message;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "Login",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}