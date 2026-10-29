namespace Ajedrez_DAS.GUI.Forms
{
    partial class FormGames
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonTodas = new System.Windows.Forms.RadioButton();
            this.radioButtonPerdidas = new System.Windows.Forms.RadioButton();
            this.radioButtonGanadas = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 92);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(668, 449);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonTodas);
            this.groupBox1.Controls.Add(this.radioButtonPerdidas);
            this.groupBox1.Controls.Add(this.radioButtonGanadas);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(462, 69);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ver Partidas:";
            // 
            // radioButtonTodas
            // 
            this.radioButtonTodas.AutoSize = true;
            this.radioButtonTodas.Checked = true;
            this.radioButtonTodas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonTodas.Location = new System.Drawing.Point(28, 23);
            this.radioButtonTodas.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonTodas.Name = "radioButtonTodas";
            this.radioButtonTodas.Size = new System.Drawing.Size(95, 29);
            this.radioButtonTodas.TabIndex = 2;
            this.radioButtonTodas.TabStop = true;
            this.radioButtonTodas.Text = "Todas";
            this.radioButtonTodas.UseVisualStyleBackColor = true;
            this.radioButtonTodas.CheckedChanged += new System.EventHandler(this.radioButtonTodas_CheckedChanged);
            // 
            // radioButtonPerdidas
            // 
            this.radioButtonPerdidas.AutoSize = true;
            this.radioButtonPerdidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonPerdidas.Location = new System.Drawing.Point(322, 23);
            this.radioButtonPerdidas.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonPerdidas.Name = "radioButtonPerdidas";
            this.radioButtonPerdidas.Size = new System.Drawing.Size(123, 29);
            this.radioButtonPerdidas.TabIndex = 1;
            this.radioButtonPerdidas.Text = "Perdidas";
            this.radioButtonPerdidas.UseVisualStyleBackColor = true;
            this.radioButtonPerdidas.CheckedChanged += new System.EventHandler(this.radioButtonPerdidas_CheckedChanged);
            // 
            // radioButtonGanadas
            // 
            this.radioButtonGanadas.AutoSize = true;
            this.radioButtonGanadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonGanadas.Location = new System.Drawing.Point(162, 23);
            this.radioButtonGanadas.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonGanadas.Name = "radioButtonGanadas";
            this.radioButtonGanadas.Size = new System.Drawing.Size(124, 29);
            this.radioButtonGanadas.TabIndex = 0;
            this.radioButtonGanadas.Text = "Ganadas";
            this.radioButtonGanadas.UseVisualStyleBackColor = true;
            this.radioButtonGanadas.CheckedChanged += new System.EventHandler(this.radioButtonGanadas_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 555);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Promedio Victorias:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 599);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(271, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tiempo Total de Juego:";
            // 
            // FormGames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 639);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormGames";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonTodas;
        private System.Windows.Forms.RadioButton radioButtonPerdidas;
        private System.Windows.Forms.RadioButton radioButtonGanadas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}