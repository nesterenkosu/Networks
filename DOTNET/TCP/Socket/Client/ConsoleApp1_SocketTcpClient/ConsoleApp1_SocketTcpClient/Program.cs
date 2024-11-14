using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp1_SocketTcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip_as_string ="";
            string message = "";

            Console.Write("Введите ip-адрес сервера: ");            
            ip_as_string= Console.ReadLine();

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Запрос на установку соединения с сервером
            socket.Connect(IPAddress.Parse(ip_as_string),8888);

            Console.WriteLine("Подключение установлено");

            Console.Write("Введите сообщение для отправки: ");
            message = Console.ReadLine();

            var stream = new NetworkStream(socket);

            // кодируем его в массив байт
            var data = Encoding.UTF8.GetBytes(message);
            // отправляем массив байт на сервер 
            stream.Write(data,0,data.Length);

            Console.WriteLine("Сообщение отправлено");

            //Запрос на разрыв соединения с сервером
            socket.Shutdown(SocketShutdown.Send);
            socket.Disconnect(true);

            Console.WriteLine("Подключение закрыто");

            Console.ReadLine();
        }
    }
}
