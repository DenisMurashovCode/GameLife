using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        private Graphics graphics;
        private int scale;
        private bool[,] pole;
        private bool[,] AngryPole;
        private int rows;
        private int cols;
        

        public Form1()
        {
            InitializeComponent();
        }

       

        private void StartGame()
        {

            if (timer1.Enabled)
                return;

            
            nudDensity.Enabled = false;

            scale = 2;

            rows = pictureBox1.Height / scale;
            cols = pictureBox1.Width / scale;
            pole = new bool[cols, rows];
            AngryPole = new bool[cols, rows];


            Random rnd = new Random();
            for (int x=0; x<cols; x++)
            {
                for(int y=0; y<rows; y++)
                {
                    pole[x, y] = rnd.Next((int)nudDensity.Value) == 0;
                    if (pole[x, y] ==false)
                    {
                        pole[x, y] = rnd.Next((int)nudDensity.Value*100) == 0;
                        if (pole[x, y] == true)
                        {
                            AngryPole[x, y] = true;
                        }else if (pole[x, y] == false)
                        {
                            AngryPole[x, y] = false;
                        }
                    }else if (pole[x, y] == true)
                    {
                        AngryPole[x, y] = false;
                    }
                }
            }

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();

           
        }



        private void Showing()
        {
            Random rnd = new Random();
            cell C = new cell();
            CellAngry cellAngry = new CellAngry();
            graphics.Clear(Color.White);

            bool angry;

            var poleUpdate = new bool[cols, rows];
            var AngryPoleUpdate = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {

                    var around = CellsAround(x, y);
                    var AngryAround = AngryCellsAround(x, y);
                    var live = pole[x, y];
                    var AngryLive = AngryPole[x, y];

                    if (!live && !AngryLive  && around == 3 && AngryAround==0)
                    {
                        poleUpdate[x, y] = true;
                    }else if(live && (around<2 || around > 3) && !AngryLive)
                    {
                        poleUpdate[x, y] = false;
                    }else if(live && AngryAround >= 1)
                    {
                        poleUpdate[x, y] = false;
                    }else if (AngryLive)
                    {
                        poleUpdate[x, y] = false;
                        AngryPoleUpdate[x, y] = false;

                        int x1 = rnd.Next(-1, 2);
                        int y1 = rnd.Next(-1, 2);

                        if (x+x1 >= 0 && y+y1 >= 0 && x+x1 < cols && y+y1 < rows)
                        {
                            poleUpdate[x + x1, y + y1] = true;
                            AngryPoleUpdate[x + x1, y + y1] = true;
                        }
                    }
                    else
                    {
                        poleUpdate[x, y] = pole[x, y];
                    }


                    if (live)
                    {
                            C.draw(graphics, scale, x, y);
                    }
                    if (AngryLive)
                    {
                        cellAngry.draw(graphics, scale, x, y);
                    }
                }
            }
            pole = poleUpdate;
            AngryPole = AngryPoleUpdate;
            pictureBox1.Refresh();
        }



        private int CellsAround(int x, int y)
        {
            int count = 0;

            for(int i=-1; i < 2; i++)
            {
                for(int j=-1; j<2; j++)
                {
                    int ver = (x + i + cols) % cols;
                    int gor = (y + j + rows) % rows;

                    var except = ver == x && gor == y;
                    var live = pole[ver, gor];
                    var angryCell = AngryPole[ver, gor];

                    if(live && !except && !angryCell)
                    {
                        count++;
                    }
                }
            }

            return count;
        }




        private int AngryCellsAround(int x, int y)
        {
            int count = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int ver = (x + i + cols) % cols;
                    int gor = (y + j + rows) % rows;

                    var except = ver == x && gor == y;
                    var angryCell = AngryPole[ver, gor];

                    if (!except && angryCell)
                    {
                        count++;
                    }
                }
            }

            return count;
        }





        private void StopGame()
        {
            if (!timer1.Enabled)
                return;
            timer1.Stop();
            nudDensity.Enabled = true;
        }

       
        private void timer1_Tick(object sender, EventArgs e)
        {

            Showing();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            StartGame();
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;

            if (e.Button == MouseButtons.Left)
            {
                int x = e.Location.X / scale;
                int y = e.Location.Y / scale;
                if(x>=0 && y>=0 && x<cols && y < rows)
                {
                    pole[x, y] = true;
                }
               
            }


            if (e.Button == MouseButtons.Right)
            {
                int x = e.Location.X / scale;
                int y = e.Location.Y / scale;
                if (x >= 0 && y >= 0 && x < cols && y < rows)
                {
                    pole[x, y] = false;
                }
            }

        }

    }
}
