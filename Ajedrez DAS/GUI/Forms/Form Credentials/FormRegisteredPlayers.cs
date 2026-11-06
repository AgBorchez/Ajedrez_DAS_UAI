using BE_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ajedrez_DAS.GUI.Forms
{
    public partial class FormRegisteredPlayers : Form
    {
        private BL_business_logic_layer.UserService UserService = new BL_business_logic_layer.UserService();
        public FormRegisteredPlayers()
        {
            InitializeComponent();

            dataGridView1.Columns.Add("UserId", "ID");
            dataGridView1.Columns.Add("Name", "Nombre");

            List<BE_User> Users = UserService.ListPlayers();
            
            foreach(BE_User user in Users)
            {
                dataGridView1.Rows.Add(user.Id, user.Username);
            }

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

                    int PlayerId = Convert.ToInt32(filaSeleccionada.Cells["UserId"].Value);
                    string PlayerName = filaSeleccionada.Cells["Name"].Value.ToString();
                   
                    FormGames frm = new FormGames(PlayerId, PlayerName);
                    frm.ShowDialog();

                }
            }
        }

    }
}
