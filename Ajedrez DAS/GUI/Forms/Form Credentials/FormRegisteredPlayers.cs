using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ajedrez_DAS.GUI.Forms
{
    public partial class FormRegisteredPlayers : Form
    {
        private BL_business_logic_layer.UserService UserService = new BL_business_logic_layer.UserService();
        public FormRegisteredPlayers()
        {
            InitializeComponent();

            dataGridView1.DataSource = UserService.ListPlayers();
            dataGridView1.Columns["Password"].Visible = false;
            dataGridView1.Columns["PlayerColor"].Visible = false;
            DataGridViewButtonColumn botonColumna = new DataGridViewButtonColumn();
            botonColumna.Name = "Games";
            botonColumna.HeaderText = "Partidas";
            botonColumna.Text = "Ver Partidas"; 
            botonColumna.UseColumnTextForButtonValue = true;

            botonColumna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(botonColumna);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Games")
                {
                    DataGridViewRow filaSeleccionada = dataGridView1.Rows[e.RowIndex];

                    int PlayerId = Convert.ToInt32(filaSeleccionada.Cells["Id"].Value);
                   
                    FormGames frm = new FormGames(PlayerId);
                    frm.ShowDialog();

                }
            }
        }

    }
}
