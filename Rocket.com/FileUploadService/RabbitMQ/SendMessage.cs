using System;
using RabbitMQ.Client;
using System.Text;

namespace FileUploadService
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
                    channel.QueueDeclare(queue: "FileUpload",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = Message;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "FileUpload",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}