using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace görüntü_işleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static Bitmap bit = new Bitmap(400, 400);
        static Graphics grafik = Graphics.FromImage(bit);
        static Bitmap yenibit = new Bitmap(400, 400);
        static Graphics yenigrafik = Graphics.FromImage(bit);
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                grafik.Clear(Color.White);

                grafik.DrawImage(pictureBox1.Image, 0, 0, 400, 400);
                button1.Enabled = true;
                timer1.Stop();
            }
            catch(Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread a = new Thread(yap); a.Start();
        }
        public void yap()
        {
            int data = 0;
            for (int y = 0; y < 398; y++)
            {
                for (int x = 0; x < 398; x++)
                {
                    Color renk = bit.GetPixel(x, y);
                    Color srenk = bit.GetPixel(x + 1, y);
                    if (hatvarmı(renk, srenk, 800000) == true)
                    {
                        yenibit.SetPixel(x, y, Color.Red);
                        data += 1;
                    }
                    else
                    {
                        yenibit.SetPixel(x, y, Color.Black);
                    }
                }
            }
                yansıt();
            
        }
        public void yansıt() {       
            pictureBox2.Image = yenibit;
           // System.IO.File.WriteAllText("c:\\users\\oyn\\desktop\\ghafıza\\1.txt", bütündata.ToString(), System.Text.Encoding.UTF8);

        }
        public Boolean hatvarmı(Color color1,Color color2,int bakışaçısı)
        {

            if (Math.Abs(color1.ToArgb() - color2.ToArgb()) > bakışaçısı)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void msgbox(String mesaj)
        {
            MessageBox.Show(mesaj);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                button1.Enabled = false;
                timer1.Start();
            }
        }
    }
}
