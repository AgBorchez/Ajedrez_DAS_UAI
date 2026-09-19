using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ajedrez_DAS.Properties;

namespace Ajedrez_DAS
{
    public partial class UCcasilla : UserControl
    {
        public event DelSeleccionarCasilla SeleccionarCasilla;

        private Casilla casilla;

        public Casilla CasillaLogica { get { return casilla; } }
        public Pieza PiezaCasilla { get { return casilla.PiezaEnCasilla; } }
        public UCcasilla()
        {
            InitializeComponent();
            this.button1.Dock = DockStyle.Fill;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.FlatAppearance.BorderSize = 0;
        }
        public UCcasilla(Color color, Casilla c) : this()
        {
            this.button1.BackColor = color;
            this.casilla = c;
        }

        public void ActualizarImagen()
        {
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            if (casilla.PiezaEnCasilla == null) 
            {
                button1.Image = null;
                return;
            }
            bool color = (casilla.PiezaEnCasilla.color == ColorAjedrez.Blanco);
            Image imgSeleccionada = null;

            switch (casilla.PiezaEnCasilla)
            {
                case Peon p: imgSeleccionada = color ? Resources.wp : Resources.bp; break;
                case Caballo c: imgSeleccionada = color ? Resources.wn : Resources.bn; break;
                case Alfil a: imgSeleccionada = color ? Resources.wb : Resources.bb; break;
                case Torre t: imgSeleccionada = color ? Resources.wr : Resources.br; break;
                case Rey k: imgSeleccionada = color ? Resources.wk : Resources.bk; break;
                case Reina q: imgSeleccionada = color ? Resources.wq : Resources.bq; break;
            }

            if (imgSeleccionada != null)
            {
                // Escala al tamaño real del botón con un pequeño margen interno
                button1.Image = Escalar(imgSeleccionada, button1.Width - 8, button1.Height - 8);
                button1.ImageAlign = ContentAlignment.MiddleCenter;
            }

        }
        public void ActualizarColorFondo()
        {
            this.button1.UseVisualStyleBackColor = false;
            this.button1.BackColor = ((casilla.y + casilla.x) % 2 != 0) ? Color.Beige : Color.BurlyWood;
            this.button1.Invalidate();
            this.button1.Update();
        }
        public void ActualizarColorFondo(Color c)
        {
            this.button1.UseVisualStyleBackColor = false;
            this.button1.BackColor = c;
            this.button1.Invalidate();
            this.button1.Update();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
                SeleccionarCasilla?.Invoke(this);
            
        }

        private Image Escalar(Image img, int ancho, int alto)
        {
            if (ancho <= 0 || alto <= 0) return img;
            Bitmap bmp = new Bitmap(ancho, alto);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, ancho, alto);
            }
            return bmp;
        }

        public bool CasillaOcupada()
        {
            return (casilla.PiezaEnCasilla != null);
        }
    }
}
