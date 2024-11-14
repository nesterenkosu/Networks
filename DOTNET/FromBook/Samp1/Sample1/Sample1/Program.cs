using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            var udpClient = new UdpClient(8001);
            var brodcastAddress = IPAddress.Parse("235.5.5.11"); // хост для отправки данных 
                                                                   // присоединяемся к группе
            udpClient.JoinMulticastGroup(brodcastAddress);
            Console.WriteLine("Начало прослушивания сообщений");
            /*while (true)
            {
                var result = await udpClient.ReceiveAsync();
                string message = Encoding.UTF8.GetString(result.Buffer);
                if (message == "END") break;
                Console.WriteLine(message);
            }*/

            Console.ReadLine();
            // отсоединяемся от группы
            udpClient.DropMulticastGroup(brodcastAddress);
            Console.WriteLine("Udp-клиент завершил свою работу");
        }
    }
}
