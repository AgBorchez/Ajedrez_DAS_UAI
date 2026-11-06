using BE_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FormTablero : Form
    {
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

        public FormTablero(List<BE_User> Players, BE_GameRecord record)
        {
            InitializeComponent();
            tablero1 = new UCBoard(Players, record);
            tablero1.OnNextMove += NextColor;
            tablero1.Dock = DockStyle.Fill;

            this.MinimumSize = new Size(500, 400);
            this.DoubleBuffered = true;
            panel1.Controls.Add(tablero1);
        }

        private void FormTablero_Resize(object sender, EventArgs e)
        {
        }     

        private void NextColor(BE_GameColor gc)
        {
            if (gc == BE_GameColor.White) { pictureBox1.BackColor = Color.White; }
            if (gc == BE_GameColor.Black) { pictureBox1.BackColor = Color.Black; }
        }
    }
}