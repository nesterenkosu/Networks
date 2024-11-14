using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace ConsoleApp1_SocketTcpClient
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
            string ip_as_string ="";
            string message = "";


            user me;
            me.uid = 1;
            me.name = "Serge";
            me.age = 30;

            //Console.WriteLine("before");Console.ReadLine();

            var data = getBytes<user>(me);

            Console.WriteLine($"Размер данных [{data.Length}]"); //Console.ReadLine();

            Console.Write("Введите ip-адрес сервера: ");            
            ip_as_string= Console.ReadLine();

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(IPAddress.Parse(ip_as_string),8888);

            Console.WriteLine("Подключение установлено");

            //Console.Write("Введите сообщение для отправки: ");
            //message = Console.ReadLine();

            Console.WriteLine("Отправка данных...");

            var stream = new NetworkStream(socket);

            // кодируем его в массив байт
            //var data = Encoding.UTF8.GetBytes(message);

            

            // отправляем массив байт на сервер 
            stream.Write(data,0,data.Length);

            Console.WriteLine("Сообщение отправлено");

            //socket.Shutdown(SocketShutdown.Send);
           // socket.Disconnect(true);

            //Console.WriteLine("Подключение закрыто");

            Console.ReadLine();
        }

        static byte[] getBytes<T>(T str)
        {
            int size = Marshal.SizeOf(str);
            byte[] arr = new byte[size];

            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(str, ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            return arr;
        }
    }
}
