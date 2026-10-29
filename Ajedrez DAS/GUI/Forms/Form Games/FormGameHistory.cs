using System.Collections.Generic;
using System.Windows.Forms;
using BE_BusinessEntities;
using BL_business_logic_layer;

namespace Ajedrez_DAS.GUI.Forms
{
    public partial class FormGameHistory : Form
    {
        private GameService gameService = new GameService();
        private UserService userService = new UserService();
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
            dataGridView1.Columns.Add("IdPlayerWhite", "Jugador Blancas");
            dataGridView1.Columns.Add("IdPlayerBlack", "Jugador Negras");
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
    }
}
