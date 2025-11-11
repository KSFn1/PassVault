namespace PassVault
{
    partial class ResetPass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResetPass));
            pictureBox1 = new PictureBox();
            label5 = new Label();
            panel1 = new Panel();
            label2 = new Label();
            textBox2 = new TextBox();
            label1 = new Label();
            textBox1 = new TextBox();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            label4 = new Label();
            panel3 = new Panel();
            button3 = new Button();
            button5 = new Button();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(25, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(132, 90);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Franklin Gothic Heavy", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(153, 25);
            label5.Name = "label5";
            label5.Size = new Size(303, 43);
            label5.TabIndex = 8;
            label5.Text = "Reset Password";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Indigo;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label5);
            panel1.Location = new Point(-7, -3);
            panel1.Name = "panel1";
            panel1.Size = new Size(598, 90);
            panel1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(136, 125);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 4;
            label2.Text = "Re-enter password";
            // 
            // textBox2
            // 
            textBox2.Cursor = Cursors.IBeam;
            textBox2.Location = new Point(136, 143);
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.Size = new Size(258, 23);
            textBox2.TabIndex = 2;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(136, 64);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 2;
            label1.Text = "New password";
            // 
            // textBox1
            // 
            textBox1.Cursor = Cursors.IBeam;
            textBox1.Location = new Point(136, 82);
            textBox1.Name = "textBox1";
            textBox1.PasswordChar = '*';
            textBox1.Size = new Size(258, 23);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(label4);
            panel2.Location = new Point(-7, 493);
            panel2.Name = "panel2";
            panel2.Size = new Size(598, 71);
            panel2.TabIndex = 4;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(523, 14);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(62, 39);
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(245, 26);
            label4.Name = "label4";
            label4.Size = new Size(109, 15);
            label4.TabIndex = 7;
            label4.Text = "©JAKS CSSD 1161";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveBorder;
            panel3.Controls.Add(button3);
            panel3.Controls.Add(button5);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(textBox2);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(textBox1);
            panel3.Location = new Point(26, 105);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(0, 0, 0, 10);
            panel3.Size = new Size(541, 334);
            panel3.TabIndex = 5;
            panel3.Paint += panel3_Paint;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ActiveBorder;
            button3.Cursor = Cursors.Hand;
            button3.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button3.ForeColor = Color.DarkOrchid;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(403, 143);
            button3.Name = "button3";
            button3.Size = new Size(20, 20);
            button3.TabIndex = 18;
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ActiveBorder;
            button5.Cursor = Cursors.Hand;
            button5.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button5.ForeColor = Color.DarkOrchid;
            button5.Image = (Image)resources.GetObject("button5.Image");
            button5.Location = new Point(403, 84);
            button5.Name = "button5";
            button5.Size = new Size(20, 20);
            button5.TabIndex = 1;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button1
            // 
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.DodgerBlue;
            button1.Location = new Point(120, 273);
            button1.Name = "button1";
            button1.Size = new Size(108, 29);
            button1.TabIndex = 4;
            button1.Text = "CANCEL";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.DarkOrchid;
            button2.Location = new Point(348, 264);
            button2.Name = "button2";
            button2.Size = new Size(75, 47);
            button2.TabIndex = 3;
            button2.Text = "RESET";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // ResetPass
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 561);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ResetPass";
            Text = "ResetPass";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label5;
        private Panel panel1;
        private Label label2;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox1;
        private Panel panel2;
        private PictureBox pictureBox2;
        private Label label4;
        private Panel panel3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button5;
    }
}