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
        public FormGames(int userId)
        {
            InitializeComponent();
            this.UserId = userId;

            FillDataGrid(GameService.ListAllGamesByUserId(userId));
        }

        private void FillDataGrid(List<BE_BusinessEntities.BE_GameRecord> games)
        {
            dataGridView1.DataSource = games;
            dataGridView1.Columns["IdGame"].Visible = false;
            dataGridView1.Columns["IdPlayerWhite"].Visible = false;
            dataGridView1.Columns["IdPlayerBlack"].Visible = false;
            dataGridView1.Columns["StartDate"].HeaderText = "Fecha Inicio";
            dataGridView1.Columns["Draw"].HeaderText = "Empate";
            dataGridView1.Columns["Ended"].HeaderText = "Fecha Finalizacion";
        }

        private void radioButtonGanadas_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGrid(GameService.ListWinnedGamesByUserId(UserId));
        }

        private void radioButtonPerdidas_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGrid(GameService.ListLostGamesByUserId(UserId));
        }

        private void radioButtonTodas_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGrid(GameService.ListAllGamesByUserId(UserId));
        }
    }
}
