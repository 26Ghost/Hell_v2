using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace Hell_v2
{
    public partial class Form1 : Form
    {
        public Class_Stars star1, star2;
        public Class_get_param parametrs;
        Form_3D f3d = new Form_3D();
        public Class_orbit_parametrs inform = new Class_orbit_parametrs();
        public int count = 0;
        public float Infinity = 1 / (float)0;

        //////TEMP/////
        PointF[] mas_speed1 = new PointF[720];
        PointF[] mas_speed2 = new PointF[720];
        PointF[] mas_vy1 = new PointF[720];
        PointF[] mas_vy2 = new PointF[720];
        PointF[] mas_lambda1 = new PointF[720];
        PointF[] mas_lambda2 = new PointF[720];
        double x_gr = 20;
        ///////////////

        public Form1()
        {
            InitializeComponent();
            star1 = new Class_Stars();
            star2 = new Class_Stars();
            parametrs = new Class_get_param();
            inform = new Class_orbit_parametrs();
        }

        private void начатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inform.system1_inf.Clear();
            inform.system2_inf.Clear();
            count = 0;
            parametrs = new Class_get_param(textBox1.Text, textBox2.Text, textBox3.Text);
            orbit_params();
            timer1.Start();
            toolStripButton1.Enabled = true;
            toolStripButton3.Enabled = true;
            dToolStripMenuItem.Enabled = true;
            parametrs.rotate = 0;
            graph();

        }
        private void star1_parametrs()
        {
            star1.mass = 4;
            star1.rad = (parametrs.a * (1 - parametrs.ecc * parametrs.ecc) / (1 - parametrs.ecc * Math.Cos(parametrs.ygol)));
            star1.x = star1.rad * Math.Cos(parametrs.ygol) + parametrs.a - parametrs.c;
            star1.y = star1.rad * Math.Sin(parametrs.ygol) + parametrs.b;
            star1.speed = 6.67 * star1.mass * Math.Pow((2 / (2 * parametrs.a - star1.rad) - 1 / parametrs.a), 2);
            star1.speed = Math.Sqrt(star1.speed);
            star1.speed *= 100;


            //if (((Math.Sqrt((parametrs.a) * (parametrs.a) - (star1.x - parametrs.a) * (star1.x - parametrs.a)) * (parametrs.a))) != 0)
                star1.tg = (parametrs.b * (parametrs.a - star1.x)) / ((Math.Sqrt(Math.Abs((parametrs.a) * (parametrs.a) - (star1.x - parametrs.a) * (star1.x - parametrs.a))) * (parametrs.a)));
            //else
                //star1.tg =10e10;

            inform.convert_params(star1.rad, star1.speed, star1.lambda, star1.x, star1.y, star1.speed_y,star1.tg);
            

        }
        private void star2_parametrs()
        {
            star2.mass = parametrs.a/parametrs.b*star1.mass;
            star2.rad = (parametrs.a2 * (1 - parametrs.ecc * parametrs.ecc) / (1 + parametrs.ecc * Math.Cos(parametrs.ygol+Math.PI)));
            star2.x = star2.rad * Math.Cos(parametrs.ygol+Math.PI) + parametrs.a + parametrs.c+parametrs.c2*2;
            star2.y = star2.rad * Math.Sin(parametrs.ygol+Math.PI) + parametrs.b;
            star2.speed = 6.67 * star2.mass * Math.Pow((2 / (2 * parametrs.a2 - star2.rad) - 1 / parametrs.a2), 2);
            star2.speed = Math.Sqrt(star2.speed);
            star2.speed *= 100;

            //if (((Math.Sqrt((parametrs.a2) * (parametrs.a2) - (star2.x - parametrs.a - parametrs.c - parametrs.c2) * (star2.x - parametrs.a - parametrs.c - parametrs.c2)) * (parametrs.a2))) != 0)
                star2.tg = (parametrs.b2 * (-(star2.x - parametrs.a - parametrs.c - parametrs.c2))) / ((Math.Sqrt(Math.Abs((parametrs.a2) * (parametrs.a2) - (star2.x - parametrs.a - parametrs.c - parametrs.c2) * (star2.x - parametrs.a - parametrs.c - parametrs.c2))) * (parametrs.a2)));
            //else
                //star2.tg = 10e10;

            inform.convert_params(star2.rad, star2.speed, star2.lambda, star2.x, star2.y, star2.speed_y,star2.tg,"так проще");
            
        }
        private void orbit_params()
        {
            parametrs.ygol = Math.PI;
            star1_parametrs();
            star2_parametrs();
            shift(star1, true);
            shift(star2, false);
            parametrs.ygol -= Math.PI / 180;
            while (Math.Cos(parametrs.ygol) != -1)
            {
                star1_parametrs();
                star2_parametrs();
                shift(star1,true);
                shift(star2,false);
                if (count > 0)
                {
                    if (inform.system1_inf[count][6] * inform.system1_inf[count - 1][6] <= 0)
                        parametrs.etalon = inform.system1_inf[count][2];

                    if (inform.system2_inf[count][6] * inform.system2_inf[count - 1][6] <= 0)
                        parametrs.etalon2 = inform.system2_inf[count][2];
                }
                parametrs.ygol -= Math.PI / 180;
                count++;
            }
            count = 0;
            inform.system1_inf[inform.system1_inf.Count-1] = inform.system1_inf[0];
            inform.system2_inf[inform.system2_inf.Count-1] = inform.system2_inf[0];

        }
        private void shift(Class_Stars star,bool str)
        {

            if (star.tg==Infinity)
                star.speed_y = star.speed;
            else if (star.tg == -Infinity)
                star.speed_y = -star.speed;

            else
                star.speed_y = star.tg * star.speed / Math.Sqrt(1+star.tg*star.tg);

            if (Math.Sqrt(1 - (star.speed_y / 300) * (star.speed_y / 300)) / (1 - (star.speed / 300)) > 0)
            {
                star.lambda = Math.Sqrt(1 - (star.speed_y / 300) * (star.speed_y / 300)) / (1 - (star.speed / 300));
                star.lambda = star.lambda * 550 - 550;
            }
            if (str)
            {
                inform.system1_inf[count][2] = star.lambda;
                inform.system1_inf[count][5] = star.speed_y;
            }
            else
            {
                inform.system2_inf[count][2] = star.lambda*-1;
                inform.system2_inf[count][5] = star.speed_y;
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {         
            //parametrs.ygol -= Math.PI / 180;
            pictureBox1.Refresh();
            pictureBox3.Refresh();
            pictureBox4.Refresh();
            pictureBox_lambda.Refresh();
            pictureBox_speed.Refresh();
            pictureBox_speed_y.Refresh();
            inf_output();
            count++;
            //parametrs.rotate++;
            if (count == 360-1)
                parametrs.period_par = !parametrs.period_par;
            if (count >= inform.system1_inf.Count - 1)
                count = 0;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (inform.system1_inf.Count !=0)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                e.Graphics.DrawEllipse(Pens.Yellow, 0, 0, 2 * (float)parametrs.a, 2 * (float)parametrs.b);
                //e.Graphics.FillEllipse(Brushes.Yellow, (float)star1.x - 20, (float)star1.y - 20, 40, 40);
                e.Graphics.FillEllipse(Brushes.Yellow, (float)inform.system1_inf[count][3] - 20, (float)inform.system1_inf[count][4] - 20, 40, 40);

                e.Graphics.DrawEllipse(Pens.Violet, (float)(parametrs.a + parametrs.c - parametrs.a2 + parametrs.c2), (float)(parametrs.b - parametrs.b2), 2 * (float)parametrs.a2, 2 * (float)parametrs.b2);
                //e.Graphics.FillEllipse(Brushes.Yellow, (float)star2.x - 10, (float)star2.y - 10, 20, 20);
                e.Graphics.FillEllipse(Brushes.Violet, (float)inform.system2_inf[count][3] - 10, (float)inform.system2_inf[count][4] - 10, 20, 20);
            }
        }
        private void inf_output()
        {
            textBox_ecc1.Text = parametrs.ecc.ToString();
            textBox_ecc2.Text = parametrs.ecc.ToString();
            textBox_speed1.Text = inform.system1_inf[count][1].ToString();
            textBox_speed2.Text = inform.system2_inf[count][1].ToString();
            textBox_speed_y1.Text = inform.system1_inf[count][5].ToString();
            textBox_speed_y2.Text = inform.system2_inf[count][5].ToString();
            textBox_x1.Text = inform.system1_inf[count][3].ToString();
            textBox_x2.Text = inform.system2_inf[count][3].ToString();
            textBox_y1.Text = inform.system1_inf[count][4].ToString();
            textBox_y2.Text = inform.system2_inf[count][4].ToString();
            textBox_centr_mass1.Text = inform.system1_inf[count][0].ToString();
            textBox_centr_mass2.Text = inform.system2_inf[count][0].ToString();
            textBox_tg1.Text = inform.system1_inf[count][6].ToString();
            textBox_tg2.Text = inform.system2_inf[count][6].ToString();
            textBox_lambda1.Text = (inform.system1_inf[count][2]-parametrs.etalon).ToString();
            textBox_lambda2.Text = (inform.system2_inf[count][2] - parametrs.etalon2).ToString();
            
        }
        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f3d.Owner = this;
            f3d.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Dispose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            if (inform.system1_inf.Count != 0)
                e.Graphics.DrawLine(Pens.Black, pictureBox3.Width / 2 + (float)inform.system1_inf[count][2] - (float)parametrs.etalon, pictureBox3.Width, pictureBox3.Width / 2 + (float)inform.system1_inf[count][2] - (float)parametrs.etalon, 0);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = 201-trackBar1.Value;
        }

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            if (inform.system1_inf.Count != 0)
            {
                e.Graphics.DrawLine(Pens.Black, pictureBox4.Width / 2 + (float)inform.system2_inf[count][2] - (float)parametrs.etalon2, pictureBox4.Width, pictureBox4.Width / 2 + (float)inform.system2_inf[count][2] - (float)parametrs.etalon2, 0);
                for (int i = 0; i < parametrs.pre_add * 8; i++)
                {
                    e.Graphics.DrawLine(Pens.Black, pictureBox4.Width / 2 + (float)inform.system2_inf[count][2] - (float)parametrs.etalon2 + (float)parametrs.pre_add * i, pictureBox4.Width, pictureBox4.Width / 2 + (float)inform.system2_inf[count][2] - (float)parametrs.etalon2 + (float)parametrs.pre_add * i, 0);
                    e.Graphics.DrawLine(Pens.Black, pictureBox4.Width / 2 + (float)inform.system2_inf[count][2] - (float)parametrs.etalon2 - (float)parametrs.pre_add * i, pictureBox4.Width, pictureBox4.Width / 2 + (float)inform.system2_inf[count][2] - (float)parametrs.etalon2 - (float)parametrs.pre_add * i, 0);
                }
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, pictureBox2.Width / 2 , pictureBox2.Width, pictureBox2.Width / 2 , 0);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            f3d.Owner = this;
            f3d.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void trackBar_a1_Scroll(object sender, EventArgs e)
        {
            if (trackBar_a1.Value >= parametrs.b)
                parametrs.a = trackBar_a1.Value;
            else
                trackBar_a1.Value = trackBar_b1.Value=(int)parametrs.b;
            textBox1.Text = trackBar_a1.Value.ToString();
            parametrs = new Class_get_param(trackBar_a1.Value.ToString(), trackBar_b1.Value.ToString(), trackBar_a2.Value.ToString());
            inform.system1_inf.Clear();
            inform.system2_inf.Clear();
            count = 0;
            orbit_params();
            parametrs.rotate = 0;
            graph();
        }

        private void trackBar_b1_Scroll(object sender, EventArgs e)
        {
            if (trackBar_b1.Value <= parametrs.a)
                parametrs.b = trackBar_b1.Value;
            else
                trackBar_b1.Value =trackBar_a1.Value= (int)parametrs.a;
            textBox2.Text = trackBar_b1.Value.ToString();
            parametrs = new Class_get_param(trackBar_a1.Value.ToString(), trackBar_b1.Value.ToString(), trackBar_a2.Value.ToString());
            inform.system1_inf.Clear();
            inform.system2_inf.Clear();
            count = 0;
            orbit_params();
            parametrs.rotate = 0;
            graph();
        }
        private void trackBar_a2_Scroll(object sender, EventArgs e)
        {
            if (trackBar_a2.Value <= parametrs.a)
                parametrs.b2 = trackBar_b1.Value;
            else
                trackBar_a2.Value = (int)parametrs.a;
            textBox3.Text = trackBar_a2.Value.ToString();
            parametrs = new Class_get_param(trackBar_a1.Value.ToString(), trackBar_b1.Value.ToString(), trackBar_a2.Value.ToString());
            inform.system1_inf.Clear();
            inform.system2_inf.Clear();
            count = 0;
            orbit_params();
            parametrs.rotate = 0;
            graph();
        }
        private void graph()
        {
            int param = 0;
            for (int par_gr = 0; par_gr < 720; par_gr++)
            {
                if (par_gr == 360)
                    param = 360;

                mas_speed1[par_gr].Y = pictureBox_speed.Height - 11 - (float)(inform.system1_inf[par_gr - param][1]) * 8;
                mas_speed1[par_gr].X = (float)x_gr;

                mas_speed2[par_gr].Y = pictureBox_speed.Height - 11 - (float)(inform.system2_inf[par_gr - param][1]) * 8;
                mas_speed2[par_gr].X = (float)x_gr;

                mas_vy1[par_gr].Y = pictureBox_speed_y.Height - 11 - (float)(inform.system1_inf[par_gr - param][5]) * 8;
                mas_vy1[par_gr].X = (float)x_gr;

                mas_vy2[par_gr].Y = pictureBox_speed_y.Height - 11 - (float)(inform.system2_inf[par_gr - param][5]) * 8;
                mas_vy2[par_gr].X = (float)x_gr;

                mas_lambda1[par_gr].Y = pictureBox_lambda.Height - 11 - (float)(inform.system1_inf[par_gr - param][2]) * 8;
                mas_lambda1[par_gr].X = (float)x_gr;

                mas_lambda2[par_gr].Y = pictureBox_lambda.Height - 11 - (float)(inform.system2_inf[par_gr - param][2]) * 8;
                mas_lambda2[par_gr].X = (float)x_gr;

                x_gr += 0.5;
                
            }
            param = 0;
            pictureBox_speed_y.Refresh();
            pictureBox_speed.Refresh();
            pictureBox_lambda.Refresh();
            x_gr = 20;
        }

        private void pictureBox_speed_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(0.5f, 0.5f);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.DrawCurve(Pens.Yellow, mas_speed1);
            e.Graphics.DrawCurve(Pens.Violet, mas_speed2);

            e.Graphics.DrawLine(Pens.White, 20, 10, 20, pictureBox_speed.Height);
            e.Graphics.DrawLine(Pens.White, 20, 10, 15, 25);
            e.Graphics.DrawLine(Pens.White, 20, 10, 25, 25);
            e.Graphics.DrawLine(Pens.White, 0, pictureBox_speed.Height - 10, 500, pictureBox_speed.Height - 10);
            e.Graphics.DrawLine(Pens.White, 500, 340, 485, 335);
            e.Graphics.DrawLine(Pens.White, 500, 340, 485, 345);

            if (count != 0)
            {
                e.Graphics.FillEllipse(Brushes.Red, mas_speed1[count + Convert.ToInt16(parametrs.period_par) * 360].X - 5, mas_speed1[count].Y - 5, 10, 10);
                e.Graphics.FillEllipse(Brushes.Red, mas_speed2[count + Convert.ToInt16(parametrs.period_par) * 360].X - 5, mas_speed2[count].Y - 5, 10, 10);
            }
        }

        private void pictureBox_speed_y_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(0.5f, 0.5f);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.DrawCurve(Pens.Yellow, mas_vy1);
            e.Graphics.DrawCurve(Pens.Violet, mas_vy2);
            
            e.Graphics.DrawLine(Pens.White, 20, 10, 20, pictureBox_speed_y.Height);
            e.Graphics.DrawLine(Pens.White, 20, 10, 15, 25);
            e.Graphics.DrawLine(Pens.White, 20, 10, 25, 25);
            e.Graphics.DrawLine(Pens.White, 0, pictureBox_speed_y.Height - 10, 500, pictureBox_speed_y.Height - 10);
            e.Graphics.DrawLine(Pens.White, 500, 340, 485, 335);
            e.Graphics.DrawLine(Pens.White, 500, 340, 485, 345);

            if (count != 0)
            {
                e.Graphics.FillEllipse(Brushes.Red, mas_vy1[count + Convert.ToInt16(parametrs.period_par) * 360].X - 5, mas_vy1[count].Y - 5, 10, 10);
                e.Graphics.FillEllipse(Brushes.Red, mas_vy2[count + Convert.ToInt16(parametrs.period_par) * 360].X - 5, mas_vy2[count].Y - 5, 10, 10);
            }
        }

        private void pictureBox_lambda_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(0.5f, 0.5f);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.DrawCurve(Pens.Yellow, mas_lambda1);
            e.Graphics.DrawCurve(Pens.Violet, mas_lambda2);

            e.Graphics.DrawLine(Pens.White, 20, 10, 20, pictureBox_lambda.Height);
            e.Graphics.DrawLine(Pens.White, 20, 10, 15, 25);
            e.Graphics.DrawLine(Pens.White, 20, 10, 25, 25);
            e.Graphics.DrawLine(Pens.White, 0, pictureBox_lambda.Height - 10, 500, pictureBox_lambda.Height - 10);
            e.Graphics.DrawLine(Pens.White, 500, 340, 485, 335);
            e.Graphics.DrawLine(Pens.White, 500, 340, 485, 345);

            if (count != 0)
            {
                e.Graphics.FillEllipse(Brushes.Red, mas_lambda1[count + Convert.ToInt16(parametrs.period_par)*360].X - 5, mas_lambda1[count].Y - 5, 10, 10);
                e.Graphics.FillEllipse(Brushes.Red, mas_lambda2[count+Convert.ToInt16(parametrs.period_par)*360].X -5, mas_lambda2[count].Y - 5, 10, 10);
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Моделирование двойных звездных систем и их спектрограмм \n\nВерсия: 0.2\n\n Автор: Частов Антон","О программе",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

    }
}
