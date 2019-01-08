using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace HK
{
    public partial class Form1 : Form
    {
        public Graphics g;
        public Bitmap bmp;
        public int count = 0;
        public string getweight;
        public int old_x=-1;
        public int old_y=-1;
        public int old_p = -1;
        InputBox value = new InputBox();
        public int[] x = new int[100];
        public int[] y = new int[100];
        public int[] first = new int[100]; 
        public int[] second = new int[100];
        public int[] w = new int[120];
        public int countweight = 0; //邊的個數
        public bool drawpoint = false;
        public bool drawline = false;
        public int[] p = new int[100];
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
        }
        private void picture_box1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {                                   
            if (e.Button == MouseButtons.Left && drawpoint == true)
            {
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.X,e.Y, 40, 40);               
                x[count] = e.X;
                y[count] = e.Y;
                g.DrawEllipse(System.Drawing.Pens.Red, e.X, e.Y, 40, 40);
                //g.DrawRectangle(System.Drawing.Pens.Black, rectangle);
                //g.FillRectangle(new SolidBrush(Color.Black),rectangle);
                g.FillEllipse(new SolidBrush(Color.Red),e.X,e.Y,40,40);
                g.DrawString(Convert.ToString(count+1),new Font("Arial",10),new SolidBrush(Color.White),e.X+15, e.Y+15);           
                pictureBox1.Image = bmp;
                count++;              
            }
            if (e.Button == MouseButtons.Left && drawline == true)
            {
               
                //System.Console.WriteLine("("+e.X+","+e.Y+")");
                if (old_x > -1 && old_y > -1)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (e.X <= x[i] + 40 && e.X >= x[i] && e.Y <= y[i] + 40 && e.Y >= y[i])
                        {                         
                           
                                                      
                         //   System.Console.WriteLine("i'm drawing");
                                if (value.ShowDialog() == DialogResult.OK)
                               {
                                  getweight = value.GetMsg();
                              }
                          
                          
                                g.DrawLine(new Pen(Color.Red, 1), old_x + 20, old_y + 20, x[i] + 20, y[i] + 20);
                                int vax = (old_x + x[i]) / 2;
                                int vay = (old_y + y[i]) / 2;
                                g.DrawString(Convert.ToString(getweight), new Font("Arial", 10), new SolidBrush(Color.Green), vax, vay);
                                first[countweight] = old_p;
                                second[countweight] = i;
                                w[countweight] = Convert.ToInt16(getweight);
                                countweight++;
                                pictureBox1.Image = bmp;
                                old_x = -1;
                                old_y = -1;
                                old_p = -1;
                                break;
                           
                            // System.Console.WriteLine("yes");                                                                                            
                        }
                    }
                }
               else if (old_x == -1 && old_y == -1)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (e.X <= x[i] + 40 && e.X >= x[i] && e.Y <= y[i] + 40 && e.Y >= y[i])
                        {
                            old_x = x[i];
                            old_y = y[i];
                            old_p = i;
                            // System.Console.WriteLine("yes");        
                            break;                                                                                                                
                        }
                    }
                }
               // System.Console.WriteLine("(" + old_x + "," + old_y + ")");             
              }
          }
     
        private void button1_Click(object sender, EventArgs e)
        {
            //count = 1;           
            //System.Console.WriteLine(countweight);
             for(int i=0;i<countweight;i++)
            {
                System.Console.WriteLine(w[i]+" ");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            drawpoint = false;
            drawline = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            drawpoint = true;
        }
        public void init()
        {
            for (int i = 0; i < count; ++i)
                p[i] = i;
        }
        int find(int x2)
        {
            return x2 == p[x2] ? x2 : (p[x2] = find(p[x2]));
        }
        void union(int x, int y)
        {
            p[find(x)] = find(y);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();  //定義畫圖的地方
            Pen black = new Pen(Color.Blue, 1);        //定義筆
          //  Pen blue = new Pen(Color.Blue, 1);
            SolidBrush black1 = new SolidBrush(Color.Black); //定義刷子
            Font word = new Font("Arial", 14);
            int a, b, c, d, x1, y1, r1, r2, temp;
          
            c = count; //點的數量(count)       
            d = countweight; // 線的數量(weightcount)
            int[] check = new int[count];
            init();          
            for (b = 0; b < d - 1; b++)
            {
                for (a = b + 1; a < d; a++)
                {

                    r1 = w[a]; //陣列前面的權位
                    r2 = w[b]; //陣列後面得權位
                    if (r1 <= r2)
                    {   //權位做排序
                        temp = first[b];
                        first[b] = first[a];  //----------X座標交換
                        first[a] = temp;

                        temp = second[b];
                        second[b] = second[a];  //  ----------Y座標交換
                        second[a] = temp;

                        temp = w[b];
                        w[b] = w[a];  //----------權位交換
                        w[a] = temp;
                    }
                }
            }
            for(int i=0,j=0;i<count-1 && j<countweight;++j)
            {
                if(find(first[j])==find(second[j]))
                {
                    continue;
                }
                x1 = first[j];
                y1 = second[j];
                union(first[j], second[j]);
                g.DrawLine(black, x[x1] + 20, y[x1] + 20, x[y1] + 20, y[y1] + 20);
                Thread.Sleep(1000); //Delay 1秒
                i++;
            }

        }
    }       
}

