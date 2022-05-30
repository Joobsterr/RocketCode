using MessageReader;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;


var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "Login",
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [x] Received {0}", message);
        SQLCon sqlcon = new();
        sqlcon.SendLog(message);
    };
    channel.BasicConsume(queue: "Login",
                         autoAck: true,
                         consumer: consumer);

    Console.WriteLine("now recieving fileupload");

    channel.QueueDeclare(queue: "FileUpload",
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumerr = new EventingBasicConsumer(channel);
    consumerr.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [x] Received {0}", message);
    };
    channel.BasicConsume(queue: "FileUpload",
                         autoAck: true,
                         consumer: consumerr);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}
