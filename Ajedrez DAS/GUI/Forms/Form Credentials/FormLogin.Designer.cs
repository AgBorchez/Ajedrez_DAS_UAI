namespace Ajedrez_DAS
{
    partial class FormLogin
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCambiarColores = new System.Windows.Forms.Button();
            this.buttonJugar = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBoxPlayer2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPlayer1 = new System.Windows.Forms.PictureBox();
            this.BtnRegisteredPlayers = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ucLogin2 = new Ajedrez_DAS.GUI.UserControls.UCLogin();
            this.ucLogin1 = new Ajedrez_DAS.GUI.UserControls.UCLogin();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(149, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 41);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Franklin Gothic Medium", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(648, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 41);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // buttonCambiarColores
            // 
            this.buttonCambiarColores.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCambiarColores.Location = new System.Drawing.Point(389, 355);
            this.buttonCambiarColores.Name = "buttonCambiarColores";
            this.buttonCambiarColores.Size = new System.Drawing.Size(143, 50);
            this.buttonCambiarColores.TabIndex = 7;
            this.buttonCambiarColores.Text = "Cambiar Colores";
            this.buttonCambiarColores.UseVisualStyleBackColor = true;
            this.buttonCambiarColores.Visible = false;
            this.buttonCambiarColores.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonJugar
            // 
            this.buttonJugar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonJugar.Location = new System.Drawing.Point(389, 422);
            this.buttonJugar.Name = "buttonJugar";
            this.buttonJugar.Size = new System.Drawing.Size(143, 50);
            this.buttonJugar.TabIndex = 8;
            this.buttonJugar.Text = "Jugar";
            this.buttonJugar.UseVisualStyleBackColor = true;
            this.buttonJugar.Visible = false;
            this.buttonJugar.Click += new System.EventHandler(this.buttonJugar_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(454, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(10, 515);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // pictureBoxPlayer2
            // 
            this.pictureBoxPlayer2.BackColor = System.Drawing.Color.Black;
            this.pictureBoxPlayer2.Location = new System.Drawing.Point(677, 254);
            this.pictureBoxPlayer2.Name = "pictureBoxPlayer2";
            this.pictureBoxPlayer2.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxPlayer2.TabIndex = 6;
            this.pictureBoxPlayer2.TabStop = false;
            this.pictureBoxPlayer2.Visible = false;
            // 
            // pictureBoxPlayer1
            // 
            this.pictureBoxPlayer1.BackColor = System.Drawing.Color.White;
            this.pictureBoxPlayer1.Location = new System.Drawing.Point(181, 254);
            this.pictureBoxPlayer1.Name = "pictureBoxPlayer1";
            this.pictureBoxPlayer1.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxPlayer1.TabIndex = 5;
            this.pictureBoxPlayer1.TabStop = false;
            this.pictureBoxPlayer1.Visible = false;
            // 
            // BtnRegisteredPlayers
            // 
            this.BtnRegisteredPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRegisteredPlayers.Location = new System.Drawing.Point(389, 25);
            this.BtnRegisteredPlayers.Name = "BtnRegisteredPlayers";
            this.BtnRegisteredPlayers.Size = new System.Drawing.Size(143, 50);
            this.BtnRegisteredPlayers.TabIndex = 9;
            this.BtnRegisteredPlayers.Text = "Jugadores Registrados";
            this.BtnRegisteredPlayers.UseVisualStyleBackColor = true;
            this.BtnRegisteredPlayers.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(777, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 50);
            this.button1.TabIndex = 11;
            this.button1.Text = "Reanudar Partida";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 50);
            this.button2.TabIndex = 12;
            this.button2.Text = "Historico Partidas";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ucLogin2
            // 
            this.ucLogin2.Location = new System.Drawing.Point(513, 117);
            this.ucLogin2.Name = "ucLogin2";
            this.ucLogin2.Size = new System.Drawing.Size(407, 165);
            this.ucLogin2.TabIndex = 2;
            // 
            // ucLogin1
            // 
            this.ucLogin1.Location = new System.Drawing.Point(12, 117);
            this.ucLogin1.Name = "ucLogin1";
            this.ucLogin1.Size = new System.Drawing.Size(407, 165);
            this.ucLogin1.TabIndex = 1;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 510);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnRegisteredPlayers);
            this.Controls.Add(this.buttonJugar);
            this.Controls.Add(this.buttonCambiarColores);
            this.Controls.Add(this.pictureBoxPlayer2);
            this.Controls.Add(this.pictureBoxPlayer1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucLogin2);
            this.Controls.Add(this.ucLogin1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "FormLogin";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GUI.UserControls.UCLogin ucLogin1;
        private GUI.UserControls.UCLogin ucLogin2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxPlayer1;
        private System.Windows.Forms.PictureBox pictureBoxPlayer2;
        private System.Windows.Forms.Button buttonCambiarColores;
        private System.Windows.Forms.Button buttonJugar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button BtnRegisteredPlayers;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}