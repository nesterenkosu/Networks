using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            Dictionary<string, string> getParams = new Dictionary<string, string>();
            Dictionary<string, string> postParams = new Dictionary<string, string>();

            string url = "";

            byte[] RequestBuffer = new byte[1024];
            byte[] ResponceBuffer = new byte[1024];

            string responce_headers="", responce_body="", responce, __get_params;
            //string request;
            string[] _headers_body, _headers, _key_val, _post_params, _get_params ;

            string _start_line="", _body;

            bool first_line;

           

            //Запуск сервера
            TcpListener server = new TcpListener(IPAddress.Any, 8787);
            server.Start();

            for (; ; )
            {
                first_line = true;

                //Ожидание подключения клиентов
                Console.WriteLine("Ожидание подключения...");

                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine("Клиент подключен");

                requestHeaders.Clear();
                postParams.Clear();
                getParams.Clear();
                
                //Принятие запроса от клиента
                client.GetStream().Read(RequestBuffer, 0, RequestBuffer.Length);                
                string request = Encoding.ASCII.GetString(RequestBuffer);

                Console.Write("[");
                Console.WriteLine(request);
                Console.Write("]");

                if (request.Trim().Trim('\0') == "") {
                    client.Close();
                    continue; 
                }

                //Console.ReadLine();

                //Парсинг заголовков запроса, и их помещение в массив
                _headers_body= request.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);                
                _headers = _headers_body[0].Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach(string _header in _headers)
                {
                    //Пропуск стартовой строки
                    if (first_line) { 
                        first_line = false;
                        _start_line = _header;
                        continue;
                    }
                    
                    if (_header.Trim() == "") continue;

                    _key_val = _header.Split(new string[] { ":" }, StringSplitOptions.None);

                    //if (_key_val[0].Trim() == "" || _key_val[1].Trim() == "") continue;

                    requestHeaders.Add(_key_val[0],_key_val[1]);
                }


                var len = _headers_body.Length;
                if ( len > 1 && _headers_body[len-1].Trim().Trim('\0')!="")
                {
                    _body = _headers_body[len-1];

                    _post_params = _body.Split(new string[] { "&" }, StringSplitOptions.None);
                    foreach (string _post_param in _post_params)
                    {
                        _key_val = _post_param.Split(new string[] { "=" }, StringSplitOptions.None);
                        postParams.Add(_key_val[0], _key_val[1]);
                    }
                }

                //Регулярное выражение для извлечения GET - параметров
                Match ReqMatch = Regex.Match(_start_line, @"^\w+\s+([^\s\?]+)\??([^\s]*)\s+HTTP/.*|");

                if (ReqMatch != Match.Empty)
                {
                    url = ReqMatch.Groups[1].Value;
                    __get_params = ReqMatch.Groups[2].Value;

                    if (__get_params.Trim() != "")
                    {
                        _get_params = __get_params.Split(new string[] { "&" }, StringSplitOptions.None);
                        foreach (string _get_param in _get_params)
                        {
                            _key_val = _get_param.Split(new string[] { "=" }, StringSplitOptions.None);
                            getParams.Add(_key_val[0], _key_val[1]);
                        }
                    }
                }

                

                Console.WriteLine("Результат парсинга заголовков:");

                foreach(KeyValuePair<string,string> h in requestHeaders)
                {
                    Console.WriteLine($"{h.Key} = {h.Value}");
                }

                Console.WriteLine("Результат парсинга POST-параметров:");

                foreach (KeyValuePair<string, string> h in postParams)
                {
                    Console.WriteLine($"{h.Key} = {h.Value}");
                }

                Console.WriteLine("Результат парсинга GET-параметров:");

                foreach (KeyValuePair<string, string> h in getParams)
                {
                    Console.WriteLine($"{h.Key} = {h.Value}");
                }

                Console.WriteLine("Запрошен файловый путь:");
                Console.WriteLine(url);

                if (requestHeaders.ContainsKey("Authorization"))
                {
                    Console.WriteLine("Аутентификация");
                    var a = requestHeaders["Authorization"].Split(new string[] { " " }, StringSplitOptions.None);
                    var b = Encoding.ASCII.GetString(Convert.FromBase64String(a[2]));
                    Console.WriteLine(requestHeaders["Authorization"].Split(new string[] { " " }, StringSplitOptions.None)[2]);

                    if(getParams.ContainsKey("relogin"))
                    {
                        responce_body = "";

                        responce_headers = "HTTP/1.1 301 Redirect\n" +
                                           "Location: "+requestHeaders["Host"]+"\n";
                    }
                    else { 
                    responce_body = "<h1>Greetings!</h1>";

                    responce_headers = "HTTP/1.1 200 OK\n" +                                       
                                       "Content-type: text/html; charset=utf8\n" +                                       
                                       "Content-Length: " + responce_body.Length + "\n\n";
                    }

                    responce = responce_headers + responce_body;

                    ResponceBuffer = Encoding.ASCII.GetBytes(responce);

                    client.GetStream().Write(ResponceBuffer, 0, ResponceBuffer.Length);
                    client.Close();
                }

                if (!requestHeaders.ContainsKey("Authorization") || getParams.ContainsKey("relogin"))
                {
                    responce_body = "<h1>Authentication required, but you pressed Cancel</h1>";

                    responce_headers = "HTTP/1.1 401 Authorization required\n" +
                                       "WWW-Authenticate: Basic realm=\"Welcome\" \n" +
                                       "Content-type: text/html; charset=utf8\n" +                                       
                                       "Content-Length: " + responce_body.Length + "\n\n";

                    responce = responce_headers + responce_body;

                    ResponceBuffer = Encoding.ASCII.GetBytes(responce);
                    client.GetStream().Write(ResponceBuffer, 0, ResponceBuffer.Length);
                    client.Close();
                }

                //Формирование ответа клиенту
                

                

                

                //Отправка ответа клиенту
                client.GetStream().Write(ResponceBuffer, 0, ResponceBuffer.Length);

                Thread.Sleep(500);

                //Отсоединение клиента
                client.Close();

                Console.WriteLine("Клиент отсоединён");
                Console.WriteLine("__________________________________________________________");
            }

            server.Stop();

            Console.ReadLine();
        }
    }
}
