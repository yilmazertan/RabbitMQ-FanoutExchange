using System.Globalization;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory();
factory.HostName = "localhost";

var connection = factory.CreateConnection();

var channel = connection.CreateModel();

var queueName = channel.QueueDeclare().QueueName; //rastgele bir queue name oluştur
channel.QueueBind(queue: queueName,exchange:"logs",routingKey:string.Empty); //queue logs adında exchange bind et

var consumer = new EventingBasicConsumer(channel);

consumer.Received +=(model,ea)=>{

var body = ea.Body.ToArray();
var message = Encoding.UTF8.GetString(body);
Console.WriteLine("Okunan Mesaj : " + message);

};

 channel.BasicConsume(queue:queueName,autoAck:false,consumer:consumer);

 Console.ReadKey();