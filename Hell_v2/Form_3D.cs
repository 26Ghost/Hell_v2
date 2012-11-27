using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;

namespace Hell_v2
{
    public partial class Form_3D : Form
    {
        private static Glu.GLUquadric quadratic;
        private static int[] texture = new int[3];
        bool rotate = false;
        float xl=0, yl=0,xr=0,yr=0;
        float lastx=0, lasty = 0;
        public Form_3D()
        {
            InitializeComponent();
            AnT.InitializeContexts();
            
        }

        private void AnT_Load(object sender, EventArgs e)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Gl.glClearColor(0, 0, 0, 1);
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 5000);
            Gl.glTranslated(0, 0, -6);


            Gl.glEnable(Gl.GL_TEXTURE_2D);                                      // Enable Texture Mapping ( NEW )
            Gl.glShadeModel(Gl.GL_SMOOTH);                                      // Enable Smooth Shading
            Gl.glClearColor(0, 0, 0, 0.5f);                                     // Black Background
            Gl.glClearDepth(1);                                                 // Depth Buffer Setup
            Gl.glEnable(Gl.GL_DEPTH_TEST);                                      // Enables Depth Testing
            Gl.glDepthFunc(Gl.GL_LEQUAL);                                       // The Type Of Depth Testing To Do
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);
            quadratic = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(quadratic, Glu.GLU_SMOOTH);                   // Create Smooth Normals (NEW)
            Glu.gluQuadricTexture(quadratic, Gl.GL_TRUE);                       // Create Texture Coords (NEW)




            Bitmap[] textureImage = new Bitmap[3];
            textureImage[0] = new Bitmap("22.bmp");
            textureImage[1] = new Bitmap("666.bmp");
            textureImage[2] = new Bitmap("stars-2.jpg");

            Gl.glGenTextures(3, texture);                            // Create The Texture
            textureImage[0].RotateFlip(RotateFlipType.RotateNoneFlipY);     // Flip The Bitmap Along The Y-Axis
            textureImage[1].RotateFlip(RotateFlipType.RotateNoneFlipY);
            textureImage[2].RotateFlip(RotateFlipType.RotateNoneFlipY);
            //Rectangle For Locking The Bitmap In Memory
            Rectangle rectangle = new Rectangle(0, 0, textureImage[0].Width, textureImage[0].Height);
            Rectangle rectangle1 = new Rectangle(0, 0, textureImage[1].Width, textureImage[1].Height);
            Rectangle rectangle2 = new Rectangle(0, 0, textureImage[2].Width, textureImage[2].Height);
            //Get The Bitmap's Pixel Data From The Locked Bitmap
            System.Drawing.Imaging.BitmapData bitmapData = textureImage[0].LockBits(rectangle, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData bitmapData1 = textureImage[1].LockBits(rectangle1, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData bitmapData2 = textureImage[2].LockBits(rectangle2, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[1]);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB8, textureImage[1].Width, textureImage[1].Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData1.Scan0);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            textureImage[1].UnlockBits(bitmapData1);                     // Unlock The Pixel Data From Memory
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[1]);

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB8, textureImage[0].Width, textureImage[0].Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            textureImage[0].UnlockBits(bitmapData);                     // Unlock The Pixel Data From Memory
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[2]);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB8, textureImage[2].Width, textureImage[2].Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData2.Scan0);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            textureImage[2].UnlockBits(bitmapData2);                     // Unlock The Pixel Data From Memory
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[2]);
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            Gl.glShadeModel(Gl.GL_SMOOTH);
            Gl.glEnable(Gl.GL_LINE_SMOOTH);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_DONT_CARE);
            Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glFlush();
            timer1.Start();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_3D.ActiveForm.Hide();
        }
        private void AnT_Paint(object sender, PaintEventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);        // Clear Screen And Depth Buffer
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 5000);
            Gl.glTranslated(0, 0, -700);
            Gl.glRotated(110 - yr-lasty, 1, 0, 0);
            Gl.glRotated(-xr-lastx, 0, 0, 1);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            textBox1.Text = (-xr).ToString();
            //textBox2.Text = (Math.Cos(xr * Math.PI / 180)).ToString();
            //textBox2.Text = ((Form1)this.Owner).inform.system1_inf[((Form1)this.Owner).count][5].ToString();
            //textBox2.Text = (((Form1)this.Owner).parametrs.ygol * 180 / Math.PI).ToString();
            //textBox2.Text = Math.Tan(Math.Atan(((Form1)this.Owner).inform.system1_inf[((Form1)this.Owner).count][6]) - (-xr) * Math.PI / 180).ToString();
            textBox3.Text = ((Math.Atan(((Form1)this.Owner).inform.system1_inf[((Form1)this.Owner).count][6]) - (-xr) * Math.PI / 180)*180/Math.PI).ToString(); 
            //if (((Math.Atan(((Form1)this.Owner).inform.system1_inf[((Form1)this.Owner).count][6]) - (-xr) * Math.PI / 180))>0)
            //textBox2.Text = (Math.Sin((Math.Atan(((Form1)this.Owner).inform.system1_inf[((Form1)this.Owner).count][6]) - (-xr) * Math.PI / 180))/**180/Math.PI*/).ToString(); 
            //else
            //textBox2.Text = (Math.Sin((-Math.Atan(((Form1)this.Owner).inform.system1_inf[((Form1)this.Owner).count][6]) + (-xr) * Math.PI / 180))/**180/Math.PI*/).ToString(); 
            textBox2.Text = Math.Sin(((Math.Atan(((Form1)this.Owner).inform.system1_inf[((Form1)this.Owner).count][6]) + ((Form1)this.Owner).parametrs.izm_ygol) /** 180 / Math.PI*/)).ToString();

            Gl.glTranslated(0, 0, 0);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[2]);
            Glu.gluSphere(quadratic, 1000.1f, 100, 100);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glColor4f(0, 20, 20, 255);
            Glut.glutWireSphere(10, 20, 20);


            Gl.glColor4f(1, 1, 1, 255);
            Gl.glLineWidth(3);
            Gl.glBegin(Gl.GL_LINE_LOOP);
            for (int i = 0; i < ((Form1)this.Owner).inform.system1_inf.Count; i++)
            {
                Gl.glVertex3f((float)((Form1)this.Owner).inform.system1_inf[i][3] - (float)((Form1)this.Owner).parametrs.c - (float)((Form1)this.Owner).parametrs.a, (float)((Form1)this.Owner).inform.system1_inf[i][4] - (float)((Form1)this.Owner).parametrs.b, 0);
            }
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINE_LOOP);
            for (int i = 0; i < ((Form1)this.Owner).inform.system2_inf.Count; i++)
            {
                Gl.glVertex3f((float)((Form1)this.Owner).inform.system2_inf[i][3] - (float)((Form1)this.Owner).parametrs.c - (float)((Form1)this.Owner).parametrs.a, (float)((Form1)this.Owner).inform.system2_inf[i][4] - (float)((Form1)this.Owner).parametrs.b, 0);
            }
            Gl.glEnd();

            Gl.glColor4f(1, 1, 1, 255);


            Gl.glEnable(Gl.GL_TEXTURE_2D);

            Gl.glLoadIdentity();
            Gl.glTranslated((float)((Form1)this.Owner).inform.system1_inf[(int)((Form1)this.Owner).count][3] - (float)((Form1)this.Owner).parametrs.c - (float)((Form1)this.Owner).parametrs.a, (float)((Form1)this.Owner).inform.system1_inf[(int)((Form1)this.Owner).count][4] - (float)((Form1)this.Owner).parametrs.b, 0);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
            Glu.gluSphere(quadratic, 50.1f, 32, 32);
            Gl.glLoadIdentity();
            Gl.glPushMatrix();

            if (checkBox1.Checked)
            {
                Gl.glLineWidth(2);
                Gl.glLoadIdentity();
                Gl.glTranslated((float)((Form1)this.Owner).inform.system2_inf[(int)((Form1)this.Owner).count][3] - (float)((Form1)this.Owner).parametrs.c - (float)((Form1)this.Owner).parametrs.a, (float)((Form1)this.Owner).inform.system2_inf[(int)((Form1)this.Owner).count][4] - (float)((Form1)this.Owner).parametrs.b, 0);
                Gl.glRotated(((Form1)this.Owner).parametrs.rotate, 0, 0, 1);
                Gl.glDisable(Gl.GL_TEXTURE_2D);
                Gl.glColor4f(1, 1, 0, 255);
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex3f(50, 0, 70);
                Gl.glVertex3f(-50, 0, -70);
                Gl.glEnd();
                Gl.glEnable(Gl.GL_TEXTURE_2D);
                Gl.glColor4f(1, 1, 1, 255);

                textBox2.Text = Math.Cos(((Form1)this.Owner).parametrs.rotate * Math.PI / 180).ToString();
                ((Form1)this.Owner).parametrs.pre_add = Math.Abs(Math.Cos(((Form1)this.Owner).parametrs.rotate * Math.PI / 180));
                textBox1.Text = ((Form1)this.Owner).parametrs.rotate.ToString();
            }
            else
                ((Form1)this.Owner).parametrs.pre_add = 0;
            Gl.glPopMatrix();

            Gl.glTranslated((float)((Form1)this.Owner).inform.system2_inf[(int)((Form1)this.Owner).count][3] - (float)((Form1)this.Owner).parametrs.c - (float)((Form1)this.Owner).parametrs.a, (float)((Form1)this.Owner).inform.system2_inf[(int)((Form1)this.Owner).count][4] - (float)((Form1)this.Owner).parametrs.b, 0);
            if (checkBox1.Checked)
            {
                Gl.glRotated(((Form1)this.Owner).parametrs.rotate, 0, 0, 1);
                Gl.glRotated(30, 0, 1, 0);
                Glu.gluPartialDisk(quadratic, 10, 70, 60, 60, 30, 360);
                //Glut.glutWireTorus(100, 20, 2, 360);
            }
            else
            {
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[1]);
                Glu.gluSphere(quadratic, 25.1f, 32, 32);
            }
            Gl.glLoadIdentity();
            Gl.glPushMatrix();

            if (checkBox_osi2.Checked || checkBox_osi1.Checked || checkBox_centr_osi.Checked)
            {
                if (checkBox_osi1.Checked)
                    koord_draw((float)((Form1)this.Owner).inform.system1_inf[(int)((Form1)this.Owner).count][3] - (float)((Form1)this.Owner).parametrs.c - (float)((Form1)this.Owner).parametrs.a, (float)((Form1)this.Owner).inform.system1_inf[(int)((Form1)this.Owner).count][4] - (float)((Form1)this.Owner).parametrs.b);

                if (checkBox_osi2.Checked)
                    koord_draw((float)((Form1)this.Owner).inform.system2_inf[(int)((Form1)this.Owner).count][3] - (float)((Form1)this.Owner).parametrs.c - (float)((Form1)this.Owner).parametrs.a, (float)((Form1)this.Owner).inform.system2_inf[(int)((Form1)this.Owner).count][4] - (float)((Form1)this.Owner).parametrs.b);

                if (checkBox_centr_osi.Checked)
                    koord_draw(0, 0);

            }


        }
        private void koord_draw(float X,float Y)
        {

            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPushMatrix();
            Gl.glLineWidth(2);
            Gl.glLoadIdentity();
            Gl.glTranslatef(X, Y, 0);
            
            Gl.glColor4f(0, 1, 1, 255);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(0, 0, 0);
            Gl.glVertex3f(0, 0, -100);//z
            Gl.glEnd();

            Gl.glRasterPos3d(-10, -10, -110);
            Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_24, 'Z');


            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(0, 0, 0);
            Gl.glVertex3f(100, 0, 0);//x
            Gl.glEnd();

            Gl.glRasterPos3d(110, 10, 10);
            Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_24, 'X');
            
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(0, 0, 0);
            Gl.glVertex3f(0, 100, 0);//y
            Gl.glEnd();
          
            Gl.glRasterPos3d(10, 110, 10);
            Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_24, 'Y');
            Gl.glPopMatrix();

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glColor4f(1, 1, 1, 255);
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (((Form1)this.Owner).timer1.Enabled)
                ((Form1)this.Owner).parametrs.rotate +=1*trackBar1.Value;
            AnT.Refresh();
            timer1.Interval = ((Form1)this.Owner).timer1.Interval;
            
        }
        private void AnT_MouseDown(object sender, MouseEventArgs e)
        {
            rotate = true;
            xl = e.X;
            yl = e.Y;

        }
        private void AnT_MouseMove(object sender, MouseEventArgs e)
        {
            if (rotate)
            {
                //if (xr != xl - e.X)
                    //new_speed_y();
                xr = (xl - e.X);
                yr = (yl - e.Y);

            }
        }
        private void new_speed_y()
        {
            ((Form1)this.Owner).parametrs.izm_ygol = (-xr) * Math.PI / 180;
            ((Form1)this.Owner).parametrs.izm_ygol2 = (xr) * Math.PI / 180;
            ((Form1)this.Owner).timer1.Stop();
            for (int i = 0; i < ((Form1)this.Owner).inform.system1_inf.Count; i++)
            {
                ((Form1)this.Owner).inform.system1_inf[i][5] = Math.Sin(Math.Atan(((Form1)this.Owner).inform.system1_inf[i][6]) + ((Form1)this.Owner).parametrs.izm_ygol) * ((Form1)this.Owner).inform.system1_inf[i][1];
                ((Form1)this.Owner).inform.system1_inf[i][2] = new_shift(true, i);
                ((Form1)this.Owner).inform.system2_inf[i][5] = Math.Sin(Math.Atan(((Form1)this.Owner).inform.system2_inf[i][6]) + ((Form1)this.Owner).parametrs.izm_ygol2) * ((Form1)this.Owner).inform.system2_inf[i][1];
                ((Form1)this.Owner).inform.system2_inf[i][2] = new_shift(false, i);
                //* Math.Sign(((Form1)this.Owner).inform.system1_inf[i][6])
                //*Math.Sign(Math.Sin(((Form1)this.Owner).parametrs.izm_ygol))
            }
            ((Form1)this.Owner).timer1.Start();
        }
        private double new_shift(bool syst, int count)
        {
            double sp = 0, sp_y = 0, lmb = 0;
            if (syst)
            {
                sp = ((Form1)this.Owner).inform.system1_inf[count][1];
                sp_y = ((Form1)this.Owner).inform.system1_inf[count][5];
                lmb = Math.Sqrt(1 - (sp_y / 300) * (sp_y / 300)) / (1 - (sp / 300));
                lmb = lmb * 550 - 550;
                //lmb *= Math.Sign(((Form1)this.Owner).inform.system1_inf[count][5]);
                //lmb *= -Math.Sign(Math.Sin(((Form1)this.Owner).parametrs.izm_ygol));
                lmb *= Math.Sign(((Form1)this.Owner).inform.system1_inf[count][5]);
            }
            else
            {
                sp = ((Form1)this.Owner).inform.system2_inf[count][1];
                sp_y = ((Form1)this.Owner).inform.system2_inf[count][5];
                lmb = Math.Sqrt(1 - (sp_y / 300) * (sp_y / 300)) / (1 - (sp / 300));
                lmb = lmb * 550 - 550;
                //lmb *= Math.Sign(((Form1)this.Owner).inform.system2_inf[count][5]);
                //lmb *= -Math.Sign(Math.Sin(((Form1)this.Owner).parametrs.izm_ygol2));
                lmb *= Math.Sign(((Form1)this.Owner).inform.system2_inf[count][5]);
            }

            
            return lmb;

        }
        private void AnT_MouseUp(object sender, MouseEventArgs e)
        {
            rotate = false;
            lastx = xr+lastx;
            lasty = yr+lasty;
            xr = 0;
            yr = 0;
        }

        private void Form_3D_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Form_3D.ActiveForm.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                ((Form1)this.Owner).parametrs.rotate = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (((Form1)this.Owner).timer1.Enabled)
            {
                button1.Text = "Продолжить";
                ((Form1)this.Owner).timer1.Stop();
            }
            else
            {
                button1.Text = "Остановить";
                ((Form1)this.Owner).timer1.Start();
            }
        }

        private void Form_3D_Load(object sender, EventArgs e)
        {
            if (((Form1)this.Owner).timer1.Enabled)
                button1.Text = "Остановить";
            else
                button1.Text = "Продолжить";
        }
    }
}
