using System.Threading.Tasks;
using System.Windows.Forms;
using Ajedrez_DAS.Properties;
using BE_BusinessEntities;

namespace Ajedrez_DAS.GUI.Forms.Board
{
    public partial class frmPromotion : Form
    {
        public BE_MovType? selection = null;
        public frmPromotion(BE_GameColor color)
        {
            InitializeComponent();
            pictureBoxBishop.Image = (color == BE_GameColor.White) ? Resources.wb : Resources.bb;
            pictureBoxKnight.Image = (color == BE_GameColor.White) ? Resources.wn : Resources.bn;
            pictureBoxQueen.Image = (color == BE_GameColor.White) ? Resources.wq : Resources.bq;
            pictureBoxRook.Image = (color == BE_GameColor.White) ? Resources.wr : Resources.br;
        }

        private void pictureBoxRook_Click(object sender, System.EventArgs e)
        {
            selection = BE_MovType.PromoteToRook;
            ClosePromotion();
        }

        private void pictureBoxKnight_Click(object sender, System.EventArgs e)
        {
            selection = BE_MovType.PromoteToKnight;
            ClosePromotion();
        }

        private void pictureBoxBishop_Click(object sender, System.EventArgs e)
        {
            selection = BE_MovType.PromoteToBishop;
            ClosePromotion();
        }

        private void pictureBoxQueen_Click(object sender, System.EventArgs e)
        {
            selection = BE_MovType.PromoteToQueen;
            ClosePromotion();
        }

        private void ClosePromotion()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
