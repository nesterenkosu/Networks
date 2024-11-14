using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp_SocketUdpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip_as_string = "";
            string message = "";

            Console.Write("Введите ip-адрес сервера: ");
            ip_as_string = Console.ReadLine();

            Console.Write("Введите сообщение для отправки: ");
            message = Console.ReadLine();

            //Создание сокета
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //Создание конечной точки - объекта, объединяющего ip-адрес и порт
            EndPoint remotePoint = new IPEndPoint(IPAddress.Parse(ip_as_string), 8888);

            //Преобразование текстовых данных в двоичные
            var data = Encoding.UTF8.GetBytes(message);

            //Отправка данных в сеть
            socket.SendTo(data, remotePoint);

            Console.WriteLine("Сообщение отправлено");

            Console.ReadLine();
        }
    }
}
