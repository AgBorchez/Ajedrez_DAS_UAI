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
using BL_business_logic_layer;
using BE_BusinessEntities;


namespace Presentacion
{
    public partial class UCSquare : UserControl
    {
        public event DelSeleccionarCasilla SeleccionarCasilla;

        public Piece SquarePiece;
        public readonly BE_Position pos;
        public UCSquare()
        {
            InitializeComponent();
            this.button1.Dock = DockStyle.Fill;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.FlatAppearance.BorderSize = 0;
        }
        public UCSquare(Color color, Square c) : this()
        {
            this.button1.BackColor = color;
            this.pos = c.Position;
            this.SquarePiece = c.SquarePiece;
        }

        public void ActualizarImagen()
        {
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            if (SquarePiece == null) 
            {
                button1.Image = null;
                return;
            }
            bool color = (SquarePiece.color == BE_GameColor.White);
            Image imgSeleccionada = null;

            switch (SquarePiece)
            {
                case Pawn p: imgSeleccionada = color ? Resources.wp : Resources.bp; break;
                case Knight c: imgSeleccionada = color ? Resources.wn : Resources.bn; break;
                case Bishop a: imgSeleccionada = color ? Resources.wb : Resources.bb; break;
                case Rook t: imgSeleccionada = color ? Resources.wr : Resources.br; break;
                case King k: imgSeleccionada = color ? Resources.wk : Resources.bk; break;
                case Queen q: imgSeleccionada = color ? Resources.wq : Resources.bq; break;
            }

            if (imgSeleccionada != null)
            {
                // Escala al tamaño real del botón con un pequeño margen interno
                button1.Image = Escalar(imgSeleccionada, button1.Width, button1.Height);
                button1.ImageAlign = ContentAlignment.MiddleCenter;
            }

        }
        public void ActualizarColorFondo()
        {
            this.button1.UseVisualStyleBackColor = false;
            this.button1.BackColor = ((pos.Y + pos.X) % 2 != 0) ? Color.Beige : Color.BurlyWood;
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
            return (SquarePiece != null);
        }
    }
}
