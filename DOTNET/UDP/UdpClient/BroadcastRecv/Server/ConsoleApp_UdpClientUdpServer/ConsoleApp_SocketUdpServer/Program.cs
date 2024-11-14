using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp_SocketUdpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //IPEndPoint ipPoint = new IPEndPoint(new IPAddress(new byte[] { 192, 168, 56, 1 }), 8888);
            //IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);


            var udpServer = new UdpClient(8001);

            udpServer.JoinMulticastGroup(IPAddress.Parse("235.5.5.11"));

            Console.WriteLine("Широковещательный UDP-сервер запущен. Ожидание подключений...");

            byte[] data = new byte[256]; // буфер для получаемых данных
                                         //адрес, с которого пришли данные
                                         //65535 - максимальный размер
            IPEndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);
            
            // получаем данные в массив data
            data = udpServer.Receive(ref remoteIp);

            var message = Encoding.UTF8.GetString(data);

            Console.WriteLine($"Адрес подключенного клиента: {remoteIp}");
            Console.WriteLine($"Получено {data.Length} байт");
            Console.WriteLine(message);     // выводим полученное сообщение

            Console.ReadLine();
        }
    }
}
