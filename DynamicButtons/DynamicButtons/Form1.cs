using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicButtons
{
    
    public partial class Form1 : Form
    {
        GameField[,] buttons = new GameField[5, 5];
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controls.Add(new GameField());
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    buttons[i, j] = new GameField
                    {
                        Width = 60,
                        Height = 60,
                        Top = 50 + i * 60,
                        Left = 50 + j * 60,
                        //Font = new System.Drawing.Font("Arial", 20),
                        Tag = $"{i},{j}",
                        Text = "T",
                        x=i,
                        y=j                       
                    };
                    buttons[i, j].Click += ButtonClick;
                    Controls.Add(buttons[i, j]);
                }
            }
        }

        void ButtonClick(object sender, EventArgs e)
        {
            
            MessageBox.Show((sender as GameField).XX.ToString()+" "+ (sender as GameField).y.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GameField b;            

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    b = new GameField
                    {
                        Width = 60,
                        Height = 60,
                        Top = 50 + i * 60,
                        Left = 50 + j * 60,
                        //Font = new System.Drawing.Font("Arial", 20),
                        Tag = $"{i},{j}",
                        Text = "T",
                        x = i,
                        y = j                        
                    };
                    b.Click += ButtonClick;
                    Controls.Add(b);
                }
            }
        }
    }

    class GameField : Button
    {
        public int x, y;

        public int XX
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }
    };
}
