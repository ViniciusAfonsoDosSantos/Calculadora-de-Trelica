
namespace Software_Trelisa
{
    public partial class Form1 : Form
    {
        public static List<Barra> listaBarras = new List<Barra>();
        public static List<Ponto> listaPontos = new List<Ponto>();
        List<PictureBox> listaPictureBox = new List<PictureBox>();
        List<Ponto> listaDeletar = new List<Ponto>();
        bool querDeletar = false;

        public  Form1()
        {
            InitializeComponent();
            AdicionaPrimeiroPonto();
       
        }

        public void novoPonto_DoubleClick(object sender, EventArgs e, Ponto ponto)
        {
            if (querDeletar == true)
            {
                QuerDeletar(ponto);
            }
            else
            {
                AdicionaBarras f = new AdicionaBarras(ponto);
                f.ShowDialog();

                foreach (PictureBox imagem in listaPictureBox)
                {
                    imagem.Enabled = false;
                    imagem.Visible = false;
                }
                lbMensagem.Visible = false;
            }
            DesenhaBarras();
        }

        private void AdicionaPrimeiroPonto()
        {
            Ponto ponto = new Ponto(75, 500);
            listaPontos.Add(ponto);
            CriaPontoImagem(ponto);
        }

        public void CriaPontoImagem(Ponto ponto)
        {
            System.Windows.Forms.PictureBox novoPontoImagem = new System.Windows.Forms.PictureBox();
            novoPontoImagem.Image = Properties.Resources.PontoSelecao;
            novoPontoImagem.Name = "Ponto" + ponto.valorX.ToString() + ponto.valorY.ToString();
            novoPontoImagem.SizeMode = PictureBoxSizeMode.StretchImage;
            novoPontoImagem.BackColor = Color.White;
            novoPontoImagem.Width = 20;
            novoPontoImagem.Height = 20;
            novoPontoImagem.Location = new Point(ponto.valorX - 10, ponto.valorY - 10);
            novoPontoImagem.Click += new EventHandler((sender, e) => novoPonto_DoubleClick(sender, e, ponto));
            novoPontoImagem.Enabled = false;
            novoPontoImagem.Visible = false;
            panel1.Controls.Add(novoPontoImagem);
            listaPictureBox.Add(novoPontoImagem);

        }

        private void DesenhaBarras()
        {
            Graphics g = panel1.CreateGraphics();
            Pen myPen = new Pen(Color.Green, 8);
            g.Clear(panel1.BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (Barra barras in listaBarras)
            {
                g.DrawLine(myPen, barras.pontoInicialX, barras.pontoInicialY, barras.pontoFinalX, barras.pontoFinalY);
                
            }
            g.Dispose();
            myPen.Dispose();
        }

        private void QuerDeletar(Ponto ponto)
        {
            listaDeletar.Add(ponto);

            if(listaDeletar.Count == 2)
            {
                foreach (PictureBox imagem in listaPictureBox)
                {
                    imagem.Enabled = false;
                    imagem.Visible = false;
                }

                if(listaBarras.Find(x => x.pontoInicialX == listaDeletar[0].valorX &
                x.pontoInicialY == listaDeletar[0].valorY & x.pontoFinalX == listaDeletar[1].valorX &
                x.pontoFinalY == listaDeletar[1].valorY) != null)
                {
                    Barra barraDeletar = listaBarras.Find(x => x.pontoInicialX == listaDeletar[0].valorX &
                    x.pontoInicialY == listaDeletar[0].valorY & x.pontoFinalX == listaDeletar[1].valorX &
                    x.pontoFinalY == listaDeletar[1].valorY);

                    listaBarras.Remove(barraDeletar);

                    foreach (Ponto pontoAcha in listaPontos)
                    {
                        if (pontoAcha.barrasPonto.Find(x => x == barraDeletar) != null)
                        {
                            pontoAcha.barrasPonto.Remove(barraDeletar);
                        }
                    }

                    foreach (Ponto pontoImagem in listaPontos)
                    {
                        if (pontoImagem.barrasPonto.Count == 0)
                        {
                            PictureBox pictureBoxRemove = listaPictureBox.Find(x => x.Name == "Ponto" +
                            pontoImagem.valorX.ToString() + pontoImagem.valorY.ToString());
                            panel1.Controls.Remove(pictureBoxRemove);
                            listaPictureBox.Remove(pictureBoxRemove);
                        }
                    }

                    listaPontos.RemoveAll(x => x.barrasPonto.Count == 0);
                }
                else if(listaBarras.Find(x => x.pontoInicialX == listaDeletar[1].valorX &
                x.pontoInicialY == listaDeletar[1].valorY & x.pontoFinalX == listaDeletar[0].valorX &
                x.pontoFinalY == listaDeletar[0].valorY) != null)
                {
                    Barra barraDeletar = listaBarras.Find(x => x.pontoInicialX == listaDeletar[1].valorX &
                    x.pontoInicialY == listaDeletar[1].valorY & x.pontoFinalX == listaDeletar[0].valorX &
                    x.pontoFinalY == listaDeletar[0].valorY);

                    listaBarras.Remove(barraDeletar);

                    foreach (Ponto pontoAcha in listaPontos)
                    {
                        if (pontoAcha.barrasPonto.Find(x => x == barraDeletar) != null)
                        {
                            pontoAcha.barrasPonto.Remove(barraDeletar);
                        }
                    }

                    foreach (Ponto pontoImagem in listaPontos)
                    {
                        if (pontoImagem.barrasPonto.Count == 0)
                        {
                            PictureBox pictureBoxRemove = listaPictureBox.Find(x => x.Name == "Ponto" +
                            pontoImagem.valorX.ToString() + pontoImagem.valorY.ToString());
                            panel1.Controls.Remove(pictureBoxRemove);
                            listaPictureBox.Remove(pictureBoxRemove);
                        }
                    }

                    listaPontos.RemoveAll(x => x.barrasPonto.Count == 0);
                }
                else if (listaDeletar[0] == listaDeletar[1])
                {
                    MessageBox.Show("Selecionou o mesmo ponto duas vezes");
                }
                else
                {
                    MessageBox.Show("Os pontos selecionados não correspondem aos ponto de alguma barra já criada");
                }

                listaDeletar.RemoveAt(1);
                listaDeletar.RemoveAt(0);
                lbDeletar.Visible = false;
                querDeletar = false;

            }

            if (listaPontos.Count == 0)
            {
                AdicionaPrimeiroPonto();
            }
        }
            
        private void btnCriar_Click(object sender, EventArgs e)
        {
            lbDeletar.Visible = false;
            lbMensagem.Visible = true;
            foreach (PictureBox imagem in listaPictureBox)
            {
                imagem.Enabled = true;
                imagem.Visible = true;
            }
        }

        private void btnDeletarBarra_Click(object sender, EventArgs e)
        {
            if(listaBarras.Count == 0)
            {
                MessageBox.Show("Não há barras para deletar");
                return;
            }

            lbMensagem.Visible = false;
            lbDeletar.Visible = true;
            foreach (PictureBox imagem in listaPictureBox)
            {
                imagem.Enabled = true;
                imagem.Visible = true;
            }
            querDeletar = true;
        }

        private void rbBarras_CheckedChanged(object sender, EventArgs e)
        {
            if(rbCriar.Checked == true && rbBarras.Checked == true)
            {
                btnCriarBarra.Enabled = true;
                btnCriarBarra.Visible = true;
                querDeletar = false;
            }
            else if(rbDeletar.Checked == true && rbBarras.Checked == true)
            {
                btnDeletarBarra.Enabled = true;
                btnDeletarBarra.Visible = true;
                querDeletar = false;
            }
            else
            {
                btnDeletarBarra.Enabled = false;
                btnDeletarBarra.Visible = false;
                btnCriarBarra.Enabled = false;
                btnCriarBarra.Visible = false;
                querDeletar = false;
            }
        }

        private void rbForcas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCriar.Checked == true && rbForcas.Checked == true)
            {
                btnCriarForcas.Enabled = true;
                btnCriarForcas.Visible = true;
                querDeletar = false;
            }
            else
            {
                btnCriarForcas.Enabled = false;
                btnCriarForcas.Visible = false;
            }
        }

        private void rbApoios_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCriar.Checked == true && rbApoios.Checked == true)
            {
                btnCriarApoios.Enabled = true;
                btnCriarApoios.Visible = true;
                querDeletar = false;
            }
            else
            {
                btnCriarApoios.Enabled = false;
                btnCriarApoios.Visible = false;
                querDeletar = false;
            }
        }

        private void rbDeletar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDeletar.Checked == true && rbBarras.Checked == true)
            {
                btnDeletarBarra.Enabled = true;
                btnDeletarBarra.Visible = true;
                querDeletar = false;
            }
            else
            {
                btnDeletarBarra.Enabled = false;
                btnDeletarBarra.Visible = false;
                querDeletar = false;
            }
        }

        private void rbCriar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCriar.Checked == true && rbBarras.Checked == true)
            {
                btnCriarBarra.Enabled = true;
                btnCriarBarra.Visible = true;
                querDeletar = false;
            }
            else if(rbCriar.Checked == true && rbForcas.Checked == true)
            {
                btnCriarForcas.Enabled = true;
                btnCriarForcas.Visible = true;
                querDeletar = false;
            }
            else if(rbCriar.Checked == true && rbApoios.Checked == true)
            {
                btnCriarApoios.Enabled = true;
                btnCriarApoios.Visible = true;
                querDeletar = false;
            }
            else
            {
                btnCriarApoios.Enabled = false;
                btnCriarApoios.Visible = false;
                btnCriarForcas.Enabled = false;
                btnCriarForcas.Visible = false;
                btnDeletarBarra.Enabled = false;
                btnDeletarBarra.Visible = false;
                querDeletar = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Barra barra1 = new Barra(75, 500, 125, 230, 79.509, 274.59060);
            Barra barra2 = new Barra(75, 500, 575, 500, 0, 500);
            Barra barra3 = new Barra(575, 500, 575, 150, 90, 350);
            Barra barra4 = new Barra(125, 230, 575, 500, 30.96376, 524.78);
            Barra barra5 = new Barra(125, 230, 575, 150, 10.0806, 457.06);
            Barra barra6 = new Barra(575, 150, 825, 500, 54.46232, 430.12);
            Barra barra7 = new Barra(825, 500, 575, 500, 0, 250);

            listaPontos[0].barrasPonto.Add(barra1);
            listaPontos[0].barrasPonto.Add(barra2);
            Ponto ponto2 = new Ponto(125, 230);
            ponto2.barrasPonto.Add(barra1);
            ponto2.barrasPonto.Add(barra4);
            ponto2.barrasPonto.Add(barra5);
            Ponto ponto3 = new Ponto(575, 500);
            ponto3.barrasPonto.Add(barra2);
            ponto3.barrasPonto.Add(barra3);
            ponto3.barrasPonto.Add(barra4);
            ponto3.barrasPonto.Add(barra7);
            Ponto ponto4 = new Ponto(575, 150);
            ponto4.barrasPonto.Add(barra3);
            ponto4.barrasPonto.Add(barra5);
            ponto4.barrasPonto.Add(barra6);
            Ponto ponto5 = new Ponto(825, 500);
            ponto5.barrasPonto.Add(barra6);
            ponto5.barrasPonto.Add(barra7);

            listaPontos.Add(ponto2);
            listaPontos.Add(ponto3);
            listaPontos.Add(ponto4);
            listaPontos.Add(ponto5);

            CriaPontoImagem(ponto2);
            CriaPontoImagem(ponto3);
            CriaPontoImagem(ponto4);
            CriaPontoImagem(ponto5);


            listaBarras.Add(barra1);
            listaBarras.Add(barra2);
            listaBarras.Add(barra3);
            listaBarras.Add(barra4);
            listaBarras.Add(barra5);
            listaBarras.Add(barra6);
            listaBarras.Add(barra7);

            DesenhaBarras();

            button2.Enabled = false;

        }
    }
}
