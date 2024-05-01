
namespace WindowsFormsApp_Client
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_client = new System.Windows.Forms.ListBox();
            this.lb_server = new System.Windows.Forms.ListBox();
            this.tb_address = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_message = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_client
            // 
            this.lb_client.FormattingEnabled = true;
            this.lb_client.ItemHeight = 16;
            this.lb_client.Location = new System.Drawing.Point(12, 23);
            this.lb_client.Name = "lb_client";
            this.lb_client.Size = new System.Drawing.Size(388, 388);
            this.lb_client.TabIndex = 0;
            // 
            // lb_server
            // 
            this.lb_server.FormattingEnabled = true;
            this.lb_server.ItemHeight = 16;
            this.lb_server.Location = new System.Drawing.Point(431, 23);
            this.lb_server.Name = "lb_server";
            this.lb_server.Size = new System.Drawing.Size(496, 228);
            this.lb_server.TabIndex = 0;
            // 
            // tb_address
            // 
            this.tb_address.Location = new System.Drawing.Point(457, 284);
            this.tb_address.Name = "tb_address";
            this.tb_address.Size = new System.Drawing.Size(185, 22);
            this.tb_address.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(431, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP";
            // 
            // tb_message
            // 
            this.tb_message.Location = new System.Drawing.Point(431, 339);
            this.tb_message.Name = "tb_message";
            this.tb_message.Size = new System.Drawing.Size(496, 22);
            this.tb_message.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(648, 281);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Подключиться";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(431, 388);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(496, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Отправить сообщение";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(794, 284);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(133, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Отключиться";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_message);
            this.Controls.Add(this.tb_address);
            this.Controls.Add(this.lb_server);
            this.Controls.Add(this.lb_client);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_client;
        private System.Windows.Forms.ListBox lb_server;
        private System.Windows.Forms.TextBox tb_address;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_message;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

