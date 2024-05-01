using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace WindowsFormsApp_Server
{
    public partial class Form1 : Form
    {
        StreamReader reader;
        StreamWriter writer;

        TcpListener tcpListenter;
        TcpClient client;

        public Form1()
        {
            InitializeComponent();

            tcpListenter = new TcpListener(IPAddress.Any,8888);
            tcpListenter.Start();

            lb_server.Items.Add("Сервер запущен "+Dns.GetHostName());
            GetServerIpsAsync();

            WaitForClients();
        }

        public async void GetServerIpsAsync()
        {
            lb_server.Items.Add("Доступные адреса для подключений:");
            IPAddress[] ips = await Dns.GetHostAddressesAsync(Dns.GetHostName());
            foreach (var ip in ips.Where(i=>i.AddressFamily==AddressFamily.InterNetwork))
                lb_server.Items.Add(ip);
        }

        public async void WaitForClients()
        {
            while(true)
            {
                client=await tcpListenter.AcceptTcpClientAsync();
                reader=new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());
                lb_client.Items.Add("Клиент подключился");
                WaitForMessagesAsync();
            }
        }

        public async void WaitForMessagesAsync()
        {
            string message;
            while (true)
            {
                message=await reader.ReadLineAsync();
                lb_client.Items.Add(message);
            }
        }

        public async void SendMessageAsync(string message)
        {
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            SendMessageAsync(tb_message.Text);
            lb_server.Items.Add(tb_message.Text);
        }
    }
}
