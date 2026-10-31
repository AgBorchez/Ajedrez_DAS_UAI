using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using BE_BusinessEntities;
using BL_business_logic_layer;
using BL_BusinessLogic;

namespace Ajedrez_DAS.GUI.Forms
{
    public partial class FormGameHistory : Form
    {
        private GameService gameService = new GameService();
        private UserService userService = new UserService();
        private BE_GameResume SelectedGameToResume;
        public FormGameHistory(bool selection)
        {
            InitializeComponent();

            FormatDataGrid(selection);        
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void FormatDataGrid(bool Selection)
        {
            List<BE_GameRecord> games;
            dataGridView1.Columns.Add("IdGame", "Juego Numero");
            dataGridView1.Columns.Add("NamePlayerWhite", "Jugador Blancas");
            dataGridView1.Columns.Add("NamePlayerBlack", "Jugador Negras");
            dataGridView1.Columns.Add("Date", "Fecha de inicio");
            dataGridView1.Columns.Add("Duration", "Tiempo De Juego");

            if (Selection) 
            {
                DataGridViewButtonColumn botonColumna = new DataGridViewButtonColumn();
                botonColumna.Name = "Resume";
                botonColumna.HeaderText = "Acciones";
                botonColumna.Text = "Reanudar";
                botonColumna.UseColumnTextForButtonValue = true;
                botonColumna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns.Add(botonColumna);

                games = gameService.ListUnEndedGames();

                foreach (BE_GameRecord game in games)
                {
                    dataGridView1.Rows.Add(game.IdGame, userService.SearchUsernameById(game.IdPlayerWhite),
                        userService.SearchUsernameById(game.IdPlayerBlack), game.StartDate, game.Duration);
                }
            }
            else
            {
                games = gameService.ListAllGames();

                foreach (BE_GameRecord game in games)
                {
                    dataGridView1.Rows.Add(game.IdGame, userService.SearchUsernameById(game.IdPlayerWhite),
                        userService.SearchUsernameById(game.IdPlayerBlack), game.StartDate, game.Duration);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Resume")
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["IdGame"].Value);
                this.SelectedGameToResume.gr = gameService.SearchGameById(id);
                this.SelectedGameToResume.WhiteP = dataGridView1.Rows[e.RowIndex].Cells["NamePlayerWhite"].Value.ToString();
                this.SelectedGameToResume.BlackP = dataGridView1.Rows[e.RowIndex].Cells["NamePlayerBlack"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public BE_GameResume GetResume()
        {
            return this.SelectedGameToResume;
        }
    }
}
