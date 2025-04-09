using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp31
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            button3.Image = Properties.Resources.Image1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Image = Image.FromFile("images/sample.png");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.ImageIndex++;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button3.ImageIndex--;

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button3.Image = imageList1.Images[1];
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button3.BackgroundImage = imageList1.Images[1];
            button3.BackgroundImageLayout = ImageLayout.Tile;
        }
    }
}
