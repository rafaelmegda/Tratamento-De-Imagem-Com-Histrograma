using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DIPLi;

namespace Aula_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public double[] calcularHistograma(DIPLi.Imagem IMG)
        {
            double[] valores = new double[256];
            int niveldecinza;

            for (int i = 0; i < IMG.Altura; i++)
            {
                for (int j = 0; j < IMG.Largura; j++)
                {
                    niveldecinza = (int)IMG[i, j];
                    valores[niveldecinza] = valores[niveldecinza] + 1;
                }
            }
            return valores;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //colocar o endereço que esta armazenado sua imagem no PC
            Imagem I = new Imagem("E:\\LennaCinza2.jpg");
            I = I.ToGrayscale();
            pictureBox1.Image = I.ToBitmap();

            double[] y = new double[256];
            y = calcularHistograma(I);

            string[] x = new string[256];

            for (int i = 0; i < 256; i++)
            {
                x[i] = i.ToString();
            }

            chart1.Series["Series1"].Points.DataBindXY(x, y);

            //y = a * X + b;

            double Xmin = 255;
            double Xmax = 0;
            for (int i = 0; i < I.Largura; i++)
            {
                for (int j = 0; j < I.Altura; j++)
                {
                    try
                    {
                        if (I[i, j] < Xmin)
                        {
                            Xmin = I[i, j];
                        }

                        if (I[i, j] > Xmax)
                        {
                            Xmax = I[i, j];
                        }
                    }
                    catch
                    {

                    }

                }
            }


            double a = 255 / (Xmax - Xmin);
            double b = -a * Xmin;

            for (int i = 0; i < I.Largura; i++)
            {
                for (int j = 0; j < I.Altura; j++)
                {
                    try
                    {
                        I[i, j] = a * I[i, j] + b;
                    }
                    catch
                    {

                    }
                }

            }

            y = new double[256];
            y = calcularHistograma(I);

            x = new string[256];

            for (int i = 0; i < 256; i++)
            {
                x[i] = i.ToString();
            }

            chart2.Series["Series1"].Points.DataBindXY(x, y);
            pictureBox2.Image = I.ToBitmap();

            //colocar o endereço que esta armazenado sua imagem no PC
            I = new Imagem("E:\\LennaCinza2.jpg");
            I = I.ToGrayscale();
            double[] hist = new double[256];
            hist = calcularHistograma(I);

            for (int i = 0; i < 256; i++)
            {
                hist[i] = hist[i] / (I.Largura * I.Altura);
            }

            for (int i = 1; i < 256; i++)
            {
                hist[i] = hist[i] + hist[i - 1];
            }

            for (int i = 1; i < 256; i++)
            {
                hist[i] = hist[i] * 255;
            }

            for (int i = 0; i < I.Altura; i++)
            {
                for (int j = 0; j < I.Largura; j++)
                {
                    double valorantes = I[i, j];
                    double valordepois = hist[(int)valorantes];
                    I[i, j] = valordepois;
                }
            }
            y = new double[256];
            y = calcularHistograma(I);
            pictureBox3.Image = I.ToBitmap();
            chart3.Series["Series1"].Points.DataBindXY(x, y);
        }
    }


}
