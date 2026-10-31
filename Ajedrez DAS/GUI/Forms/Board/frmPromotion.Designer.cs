namespace Ajedrez_DAS.GUI.Forms.Board
{
    partial class frmPromotion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxRook = new System.Windows.Forms.PictureBox();
            this.pictureBoxKnight = new System.Windows.Forms.PictureBox();
            this.pictureBoxQueen = new System.Windows.Forms.PictureBox();
            this.pictureBoxBishop = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKnight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQueen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBishop)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxRook
            // 
            this.pictureBoxRook.Location = new System.Drawing.Point(14, 12);
            this.pictureBoxRook.Name = "pictureBoxRook";
            this.pictureBoxRook.Size = new System.Drawing.Size(100, 95);
            this.pictureBoxRook.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxRook.TabIndex = 0;
            this.pictureBoxRook.TabStop = false;
            this.pictureBoxRook.Click += new System.EventHandler(this.pictureBoxRook_Click);
            // 
            // pictureBoxKnight
            // 
            this.pictureBoxKnight.Location = new System.Drawing.Point(120, 12);
            this.pictureBoxKnight.Name = "pictureBoxKnight";
            this.pictureBoxKnight.Size = new System.Drawing.Size(100, 95);
            this.pictureBoxKnight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxKnight.TabIndex = 1;
            this.pictureBoxKnight.TabStop = false;
            this.pictureBoxKnight.Click += new System.EventHandler(this.pictureBoxKnight_Click);
            // 
            // pictureBoxQueen
            // 
            this.pictureBoxQueen.Location = new System.Drawing.Point(332, 12);
            this.pictureBoxQueen.Name = "pictureBoxQueen";
            this.pictureBoxQueen.Size = new System.Drawing.Size(100, 95);
            this.pictureBoxQueen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxQueen.TabIndex = 3;
            this.pictureBoxQueen.TabStop = false;
            this.pictureBoxQueen.Click += new System.EventHandler(this.pictureBoxQueen_Click);
            // 
            // pictureBoxBishop
            // 
            this.pictureBoxBishop.Location = new System.Drawing.Point(226, 12);
            this.pictureBoxBishop.Name = "pictureBoxBishop";
            this.pictureBoxBishop.Size = new System.Drawing.Size(100, 95);
            this.pictureBoxBishop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBishop.TabIndex = 2;
            this.pictureBoxBishop.TabStop = false;
            this.pictureBoxBishop.Click += new System.EventHandler(this.pictureBoxBishop_Click);
            // 
            // frmPromotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 121);
            this.Controls.Add(this.pictureBoxQueen);
            this.Controls.Add(this.pictureBoxBishop);
            this.Controls.Add(this.pictureBoxKnight);
            this.Controls.Add(this.pictureBoxRook);
            this.Name = "frmPromotion";
            this.Text = "Promotion";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKnight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQueen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBishop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxRook;
        private System.Windows.Forms.PictureBox pictureBoxKnight;
        private System.Windows.Forms.PictureBox pictureBoxQueen;
        private System.Windows.Forms.PictureBox pictureBoxBishop;
    }
}