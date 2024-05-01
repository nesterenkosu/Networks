using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp_TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8888);
            server.Start();

            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            TcpClient client = server.AcceptTcpClient();

            Console.WriteLine("Клиент подключен");

            var stream = client.GetStream();

            // определяем буфер для получения данных
            byte[] responseData = new byte[1024];

            string response;
            for (; ; ) {
               
                // получаем данные
                var bytes = stream.Read(responseData, 0, 10);
                // преобразуем полученные данные в строку
                response = Encoding.UTF8.GetString(responseData, 0, bytes);
                // выводим данные на консоль
                Console.Write(response);
                if (response == "Q") break;
            }

            client.Close();
            server.Stop();

            Console.ReadLine();
        }
    }
}
