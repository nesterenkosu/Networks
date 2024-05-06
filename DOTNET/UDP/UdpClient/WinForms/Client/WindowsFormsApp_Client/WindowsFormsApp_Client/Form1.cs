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

namespace WindowsFormsApp_Client
{
    public partial class Form1 : Form
    {
        UdpClient client;

        IPEndPoint server_addr;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void WaitForMessagesAsync()
        {
            string message;
            UdpReceiveResult result;
            while (true)
            {               
                    // получаем данные
                    result = await client.ReceiveAsync();
                    message = Encoding.UTF8.GetString(result.Buffer);
                    // выводим сообщение                    
                    lb_server.Items.Add(message);
            }
        }

        public async void SendMessageAsync(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(data, data.Length, server_addr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client = new UdpClient(8887);
            server_addr = new IPEndPoint(IPAddress.Parse(tb_address.Text), 8888);            
            lb_client.Items.Add("Подключение к серверу установлено");

            WaitForMessagesAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMessageAsync(tb_message.Text);
            lb_client.Items.Add(tb_message);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client.Close();
            lb_client.Items.Add("Подключение разорвано");
        }
    }
}
