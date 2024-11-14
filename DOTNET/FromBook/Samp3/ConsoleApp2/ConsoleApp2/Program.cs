using System.Net;
using System.Net.Sockets;
using System.Text;

var messages = new string[] { "Hello World!", "Hello METANIT.COM", "Hello work", "END" };
var brodcastAddress = IPAddress.Parse("235.5.5.11"); ; // хост для отправки данных 
using var udpSender = new UdpClient();
Console.WriteLine("Начало отправки сообщений...");
// отправляем сообщения
foreach (var message in messages)
{
    Console.WriteLine($"Отправляется сообщение: {message}");
    byte[] data = Encoding.UTF8.GetBytes(message);
    await udpSender.SendAsync(data, new IPEndPoint(brodcastAddress, 8001));
    await Task.Delay(1000);
}