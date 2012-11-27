namespace Hell_v2
{
    partial class Form_3D
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
            this.components = new System.ComponentModel.Container();
            this.AnT = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_centr_osi = new System.Windows.Forms.CheckBox();
            this.checkBox_osi2 = new System.Windows.Forms.CheckBox();
            this.checkBox_osi1 = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // AnT
            // 
            this.AnT.AccumBits = ((byte)(0));
            this.AnT.AutoCheckErrors = false;
            this.AnT.AutoFinish = false;
            this.AnT.AutoMakeCurrent = true;
            this.AnT.AutoSwapBuffers = true;
            this.AnT.BackColor = System.Drawing.Color.Black;
            this.AnT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AnT.ColorBits = ((byte)(32));
            this.AnT.DepthBits = ((byte)(16));
            this.AnT.Location = new System.Drawing.Point(7, 40);
            this.AnT.Name = "AnT";
            this.AnT.Size = new System.Drawing.Size(655, 349);
            this.AnT.StencilBits = ((byte)(0));
            this.AnT.TabIndex = 2;
            this.AnT.Load += new System.EventHandler(this.AnT_Load);
            this.AnT.Paint += new System.Windows.Forms.PaintEventHandler(this.AnT_Paint);
            this.AnT.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AnT_MouseMove);
            this.AnT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AnT_MouseDown);
            this.AnT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AnT_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.checkBox_centr_osi);
            this.panel1.Controls.Add(this.checkBox_osi2);
            this.panel1.Controls.Add(this.checkBox_osi1);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(670, 34);
            this.panel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(271, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Продолжить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 26);
            this.label1.TabIndex = 10;
            this.label1.Text = "скорость\r\nпрецессии";
            // 
            // checkBox_centr_osi
            // 
            this.checkBox_centr_osi.AutoSize = true;
            this.checkBox_centr_osi.Location = new System.Drawing.Point(578, 3);
            this.checkBox_centr_osi.Name = "checkBox_centr_osi";
            this.checkBox_centr_osi.Size = new System.Drawing.Size(78, 17);
            this.checkBox_centr_osi.TabIndex = 8;
            this.checkBox_centr_osi.Text = "Оси центр";
            this.checkBox_centr_osi.UseVisualStyleBackColor = true;
            // 
            // checkBox_osi2
            // 
            this.checkBox_osi2.AutoSize = true;
            this.checkBox_osi2.Location = new System.Drawing.Point(502, 5);
            this.checkBox_osi2.Name = "checkBox_osi2";
            this.checkBox_osi2.Size = new System.Drawing.Size(70, 17);
            this.checkBox_osi2.TabIndex = 7;
            this.checkBox_osi2.Text = "Оси 2зв.";
            this.checkBox_osi2.UseVisualStyleBackColor = true;
            // 
            // checkBox_osi1
            // 
            this.checkBox_osi1.AutoSize = true;
            this.checkBox_osi1.Location = new System.Drawing.Point(426, 5);
            this.checkBox_osi1.Name = "checkBox_osi1";
            this.checkBox_osi1.Size = new System.Drawing.Size(70, 17);
            this.checkBox_osi1.TabIndex = 6;
            this.checkBox_osi1.Text = "Оси 1зв.";
            this.checkBox_osi1.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(161, -2);
            this.trackBar1.Maximum = 50;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 42);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.TickFrequency = 5;
            this.trackBar1.Value = 20;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(5, 8);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Прецессия";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(468, 109);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(40, 20);
            this.textBox3.TabIndex = 9;
            this.textBox3.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(422, 109);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(40, 20);
            this.textBox2.TabIndex = 5;
            this.textBox2.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(375, 109);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(41, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Visible = false;
            // 
            // Form_3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 391);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.AnT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_3D";
            this.Text = "Трехмерная модель";
            this.Load += new System.EventHandler(this.Form_3D_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_3D_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl AnT;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox checkBox_centr_osi;
        private System.Windows.Forms.CheckBox checkBox_osi2;
        private System.Windows.Forms.CheckBox checkBox_osi1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}