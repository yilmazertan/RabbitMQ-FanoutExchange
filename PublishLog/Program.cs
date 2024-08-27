//Bu rabbitmq örneği fanout exchane için tasarladı.Loglar exchange atılır ve clientlar bu exchane bağlanıp aynı anda mesajlara erişebilir

using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory();
factory.HostName = "localhost";

var connection = factory.CreateConnection();

var channel = connection.CreateModel();
channel.ExchangeDeclare(exchange:"logs",ExchangeType.Fanout); //fanout kendisine bağlı ne kadar subscriber varsa hepsine mesajı iletir

var message = GetMessage(args);
var body=Encoding.UTF8.GetBytes(message);
channel.BasicPublish(exchange:"logs", routingKey:string.Empty, basicProperties:null , body:body);

Console.WriteLine("Mesaj gönderildi : " + message);



static string GetMessage(string[]args){
 return args.Length>0? string.Join(" ",args) : "Info : hello world";

}

