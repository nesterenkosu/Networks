using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp_SocketTcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(new IPAddress(new byte[] {192,168,56,1 }), 8888);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);   // связываем с локальной точкой ipPoint

            // получаем конечную точку, с которой связан сокет
            Console.WriteLine(socket.LocalEndPoint); // 0.0.0.0:8888

            socket.Listen(1000);
            Console.WriteLine("Сервер запущен. Ожидание подключений...");
            // получаем входящее подключение
            Socket client = socket.Accept();
            // получаем адрес клиента
            Console.WriteLine($"Адрес подключенного клиента: {client.RemoteEndPoint}");

            var stream = new NetworkStream(client);

            // буфер для получения данных
            var responseData = new byte[512];
            string response;

            for (; ; )
            {
                // получаем данные
                var bytes = stream.Read(responseData, 0, 10);
                // преобразуем полученные данные в строку
                response = Encoding.UTF8.GetString(responseData, 0, bytes);
                // выводим данные на консоль
                Console.Write(response);
                if (response == "Q") break;
            }

            Console.ReadLine();
        }
    }
}
