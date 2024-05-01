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
        StreamWriter writer;
        StreamReader reader;

        TcpClient client;
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
            while (true)
            {
                message = await reader.ReadLineAsync();
                lb_server.Items.Add(message);
            }
        }

        public async void SendMessageAsync(string message)
        {
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            client.Connect(IPAddress.Parse(tb_address.Text), 8888);
            writer = new StreamWriter(client.GetStream());
            reader = new StreamReader(client.GetStream());
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
