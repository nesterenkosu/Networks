
namespace WindowsFormsApp_Server
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
            this.lb_server = new System.Windows.Forms.ListBox();
            this.lb_client = new System.Windows.Forms.ListBox();
            this.tb_message = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_server
            // 
            this.lb_server.FormattingEnabled = true;
            this.lb_server.ItemHeight = 16;
            this.lb_server.Location = new System.Drawing.Point(12, 24);
            this.lb_server.Name = "lb_server";
            this.lb_server.Size = new System.Drawing.Size(515, 468);
            this.lb_server.TabIndex = 0;
            // 
            // lb_client
            // 
            this.lb_client.FormattingEnabled = true;
            this.lb_client.ItemHeight = 16;
            this.lb_client.Location = new System.Drawing.Point(552, 24);
            this.lb_client.Name = "lb_client";
            this.lb_client.Size = new System.Drawing.Size(532, 308);
            this.lb_client.TabIndex = 0;
            // 
            // tb_message
            // 
            this.tb_message.Location = new System.Drawing.Point(552, 353);
            this.tb_message.Name = "tb_message";
            this.tb_message.Size = new System.Drawing.Size(532, 22);
            this.tb_message.TabIndex = 1;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(552, 381);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(532, 31);
            this.btn_send.TabIndex = 2;
            this.btn_send.Text = "Отправить";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 541);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tb_message);
            this.Controls.Add(this.lb_client);
            this.Controls.Add(this.lb_server);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_server;
        private System.Windows.Forms.ListBox lb_client;
        private System.Windows.Forms.TextBox tb_message;
        private System.Windows.Forms.Button btn_send;
    }
}

