using BE_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FormTablero : Form
    {
        private const int ANCHO_PANEL_DERECHO = 220;
        private const int MARGEN = 15;
        private UCBoard tablero1;

        public FormTablero(List<BE_User> Players)
        {
            InitializeComponent();
            tablero1 = new UCBoard(Players);
            tablero1.OnNextMove += NextColor;
            tablero1.Dock = DockStyle.Fill;

            this.MinimumSize = new Size(500, 400);
            this.DoubleBuffered = true;
            panel1.Controls.Add(tablero1);
        }

        private void FormTablero_Resize(object sender, EventArgs e)
        {
            AjustarProporcionTablero();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            AjustarProporcionTablero();
        }

        private void AjustarProporcionTablero()
        {
            if (this.ClientSize.Width <= 0 || this.ClientSize.Height <= 0) return;

            // 1. Espacio horizontal disponible descontando el lateral derecho y márgenes
            int anchoDisponible = this.ClientSize.Width - ANCHO_PANEL_DERECHO - (MARGEN * 2);
            // 2. Espacio vertical disponible
            int altoDisponible = this.ClientSize.Height - (MARGEN * 2);

            // 3. LA CLAVE: El tablero toma la menor dimensión disponible para mantenerse cuadrado (1:1)
            int ladoTablero = Math.Min(anchoDisponible, altoDisponible);

            // Redondeamos hacia abajo al múltiplo de 8 más cercano para que no haya líneas desfasadas
            ladoTablero = (ladoTablero / 8) * 8;

            if (ladoTablero < 80) return;

            // 4. Centramos verticalmente el tablero en caso de que sobre alto en la ventana
            int posY = MARGEN + Math.Max(0, (altoDisponible - ladoTablero) / 2);

            // 5. Ajustamos el UserControl contenedor tablero1
            tablero1.SetBounds(MARGEN, posY, ladoTablero, ladoTablero);

            // 6. Redimensionamos las 64 casillas internas
            tablero1.ResizeSquares(ladoTablero);
        }

        private void NextColor(BE_GameColor gc)
        {
            if (gc == BE_GameColor.White) { pictureBox1.BackColor = Color.White; }
            if (gc == BE_GameColor.Black) { pictureBox1.BackColor = Color.Black; }
        }
    }
}