namespace PassVault
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            panel1 = new Panel();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            label5 = new Label();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            label4 = new Label();
            label1 = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Indigo;
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label5);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1504, 90);
            panel1.TabIndex = 7;
            // 
            // button4
            // 
            button4.Cursor = Cursors.Hand;
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = Color.Indigo;
            button4.Image = Properties.Resources.logobig;
            button4.Location = new Point(25, 0);
            button4.Name = "button4";
            button4.Size = new Size(132, 90);
            button4.TabIndex = 5;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click_1;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button3.BackColor = Color.Crimson;
            button3.Cursor = Cursors.Hand;
            button3.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.Transparent;
            button3.Location = new Point(1343, 43);
            button3.Name = "button3";
            button3.Size = new Size(112, 40);
            button3.TabIndex = 12;
            button3.Text = "LOGOUT";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.Transparent;
            button2.Location = new Point(1198, 43);
            button2.Name = "button2";
            button2.Size = new Size(139, 40);
            button2.TabIndex = 10;
            button2.Text = "CONTACT US";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Franklin Gothic Heavy", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(631, 18);
            label5.Name = "label5";
            label5.Size = new Size(248, 61);
            label5.TabIndex = 8;
            label5.Text = "ABOUT US";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 940);
            panel2.Name = "panel2";
            panel2.Size = new Size(1504, 71);
            panel2.TabIndex = 8;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1402, 18);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(62, 39);
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(719, 29);
            label4.Name = "label4";
            label4.Size = new Size(109, 15);
            label4.TabIndex = 7;
            label4.Text = "©JAKS CSSD 1161";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(237, 200);
            label1.Name = "label1";
            label1.Size = new Size(1057, 250);
            label1.TabIndex = 9;
            label1.Text = resources.GetString("label1.Text");
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(654, 144);
            label2.Name = "label2";
            label2.Size = new Size(202, 40);
            label2.TabIndex = 10;
            label2.Text = "Who Are We?";
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Bottom;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 522);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1504, 418);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1504, 1011);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "About";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button button4;
        private Button button3;
        private Button button2;
        private Label label5;
        private Panel panel2;
        private PictureBox pictureBox2;
        private Label label4;
        private Label label1;
        private Label label2;
        private PictureBox pictureBox1;
    }
}