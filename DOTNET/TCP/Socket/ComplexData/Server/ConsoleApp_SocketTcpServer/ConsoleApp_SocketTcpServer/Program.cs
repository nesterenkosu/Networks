using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace ConsoleApp_SocketTcpServer
{
    struct user
    {
        public int uid;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string name;
        public int age;
    }
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);   // связываем с локальной точкой ipPoint

            // получаем конечную точку, с которой связан сокет
            Console.WriteLine(socket.LocalEndPoint); // 0.0.0.0:8888

            socket.Listen(1000);
            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            Console.WriteLine($"Размер данных: {Marshal.SizeOf(typeof(user))}"); //Console.ReadLine();
            // получаем входящее подключение
            Socket client = socket.Accept();
            // получаем адрес клиента
            Console.WriteLine($"Адрес подключенного клиента: {client.RemoteEndPoint}");

            var stream = new NetworkStream(client);

            // буфер для получения данных
            var responseData = new byte[512];
            string response;

            user me;


            

            
            // получаем данные
            var bytes = stream.Read(responseData, 0, Marshal.SizeOf(typeof(user)));

            //stream.Read((byte[])u,0,sizeof(user))
            // преобразуем полученные данные в строку
            //response = Encoding.UTF8.GetString(responseData, 0, bytes);
            me = fromBytes(responseData);
            // выводим данные на консоль
            Console.Write($"[{me.uid}] [{me.name}] [{me.age}]");
            //if (response == "Q") break;
           

            Console.ReadLine();
        }

        static user fromBytes(byte[] arr)
        {
            user str = new user();

            int size = Marshal.SizeOf(str);
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(size);

                Marshal.Copy(arr, 0, ptr, size);

                str = (user)Marshal.PtrToStructure(ptr, str.GetType());
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            return str;
        }
        
    }
}
