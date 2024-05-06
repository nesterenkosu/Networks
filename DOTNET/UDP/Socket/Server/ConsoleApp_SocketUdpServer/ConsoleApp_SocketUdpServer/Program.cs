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
            IPEndPoint ipPoint = new IPEndPoint(new IPAddress(new byte[] { 192, 168, 56, 1 }), 8888);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(ipPoint);   // связываем с локальной точкой ipPoint

            Console.WriteLine("UDP-сервер запущен. Ожидание подключений...");

            byte[] data = new byte[256]; // буфер для получаемых данных
                                         //адрес, с которого пришли данные
                                         //65535 - максимальный размер
            EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);
            // получаем данные в массив data
            var message_length = socket.ReceiveFrom(data, ref remoteIp);
            var message = Encoding.UTF8.GetString(data, 0, message_length);

            Console.WriteLine($"Адрес подключенного клиента: {remoteIp}");
            Console.WriteLine($"Получено {message_length} байт");
            Console.WriteLine(message);     // выводим полученное сообщение

            Console.ReadLine();
        }
    }
}
