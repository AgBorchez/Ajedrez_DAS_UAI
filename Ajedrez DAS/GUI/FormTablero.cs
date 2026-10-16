using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Ajedrez_DAS.BLL;

namespace Ajedrez_DAS.Presentacion
{
    public partial class FormTablero : System.Windows.Forms.Form
    {
        public FormTablero()
        {
            InitializeComponent();
            int width = this.Width;
            int height = this.Height;
            tablero1.PointToScreen(new Point(width/8, height/10));
        }

        private void FormTablero_Resize(object sender, EventArgs e)
        {
            tablero1.Tablero_Resize(this.ClientSize);
        }
    }
}