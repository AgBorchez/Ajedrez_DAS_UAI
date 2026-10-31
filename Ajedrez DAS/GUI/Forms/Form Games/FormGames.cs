using BE_BusinessEntities;
using BL_business_logic_layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ajedrez_DAS.GUI.Forms
{
    public partial class FormGames : Form
    {
        private int UserId;
        private BL_business_logic_layer.GameService GameService = new BL_business_logic_layer.GameService();
        float AverageWins = 0;
        TimeSpan TimePlayed = TimeSpan.Zero;

        public FormGames(int userId, string username)
        {
            InitializeComponent();
            this.Text = $"Informacion de {username}";
            this.UserId = userId;
            List<BE_GameRecord> AllGames = GameService.ListAllGamesByUserId(userId);

            AverageWins = GameService.ListWinnedGamesByUserId(UserId).Count();
            AverageWins = AverageWins / AllGames.Count;
            
            foreach(BE_GameRecord game in AllGames)
            {
                TimePlayed += game.Duration;
            }

            labelAverages.Text = $"Promedio Victorias: {(int)(AverageWins*100)}%";
            labelGameTime.Text = $"Tiempo Total de Juego: {TimePlayed}";

            FillDataGrid(AllGames);
        }

        private void FillDataGrid(List<BE_BusinessEntities.BE_GameRecord> games)
        {
            ClearDGV();

            dataGridView1.Columns.Add("Date", "Fecha de inicio");
            dataGridView1.Columns.Add("Ended", "Fecha De finalizacion");
            dataGridView1.Columns.Add("Duration", "Tiempo Jugado");
            dataGridView1.Columns.Add("Draw", "Empatado");


            foreach (BE_GameRecord game in games)
            {
                dataGridView1.Rows.Add(game.StartDate, game.EndDate, game.Duration, game.Draw);
                
            }
        }

        private void FillDataGridNoDraw(List<BE_BusinessEntities.BE_GameRecord> games)
        {
            ClearDGV();

            dataGridView1.Columns.Add("Date", "Fecha de inicio");
            dataGridView1.Columns.Add("Ended", "Fecha De finalizacion");
            dataGridView1.Columns.Add("Duration", "Tiempo Jugado");

            foreach (BE_GameRecord game in games)
            {
                dataGridView1.Rows.Add(game.StartDate, game.EndDate, game.Duration);
            }
        }

        private void ClearDGV()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
        }

        private void radioButtonGanadas_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGridNoDraw(GameService.ListWinnedGamesByUserId(UserId));
        }

        private void radioButtonPerdidas_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGridNoDraw(GameService.ListLostGamesByUserId(UserId));
        }

        private void radioButtonTodas_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGrid(GameService.ListAllGamesByUserId(UserId));
        }
    }
}
