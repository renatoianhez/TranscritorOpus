namespace TranscritorWhatsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lstArquivos = new ListBox();
            btnAdicionar = new Button();
            btnIniciar = new Button();
            chkLimpar = new CheckBox();
            grpSaida = new GroupBox();
            btnSelecionarDestino = new Button();
            txtCaminhoDestino = new TextBox();
            rbOutraPasta = new RadioButton();
            rbMesmaPasta = new RadioButton();
            label1 = new Label();
            cmbModelo = new ComboBox();
            numThreads = new NumericUpDown();
            label2 = new Label();
            lblStatus = new Label();
            progressBar = new ProgressBar();
            btnRemover = new Button();
            grpSaida.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numThreads).BeginInit();
            SuspendLayout();
            // 
            // lstArquivos
            // 
            lstArquivos.FormattingEnabled = true;
            lstArquivos.ItemHeight = 17;
            lstArquivos.Location = new Point(13, 46);
            lstArquivos.Name = "lstArquivos";
            lstArquivos.SelectionMode = SelectionMode.MultiExtended;
            lstArquivos.Size = new Size(558, 191);
            lstArquivos.TabIndex = 0;
            // 
            // btnAdicionar
            // 
            btnAdicionar.AutoSize = true;
            btnAdicionar.Location = new Point(12, 14);
            btnAdicionar.Name = "btnAdicionar";
            btnAdicionar.Size = new Size(187, 27);
            btnAdicionar.TabIndex = 1;
            btnAdicionar.Text = "Adicionar arquivos de áudio";
            btnAdicionar.UseVisualStyleBackColor = true;
            // 
            // btnIniciar
            // 
            btnIniciar.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIniciar.Location = new Point(485, 570);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(111, 41);
            btnIniciar.TabIndex = 2;
            btnIniciar.Text = "Transcrever";
            btnIniciar.UseVisualStyleBackColor = true;
            // 
            // chkLimpar
            // 
            chkLimpar.AutoSize = true;
            chkLimpar.Location = new Point(13, 265);
            chkLimpar.Name = "chkLimpar";
            chkLimpar.Size = new Size(207, 21);
            chkLimpar.TabIndex = 3;
            chkLimpar.Text = "Limpar a lista após transcrever";
            chkLimpar.UseVisualStyleBackColor = true;
            // 
            // grpSaida
            // 
            grpSaida.Controls.Add(btnSelecionarDestino);
            grpSaida.Controls.Add(txtCaminhoDestino);
            grpSaida.Controls.Add(rbOutraPasta);
            grpSaida.Controls.Add(rbMesmaPasta);
            grpSaida.Location = new Point(16, 386);
            grpSaida.Name = "grpSaida";
            grpSaida.Size = new Size(580, 168);
            grpSaida.TabIndex = 4;
            grpSaida.TabStop = false;
            grpSaida.Text = "Opções de Salvamento";
            // 
            // btnSelecionarDestino
            // 
            btnSelecionarDestino.Enabled = false;
            btnSelecionarDestino.Location = new Point(410, 116);
            btnSelecionarDestino.Name = "btnSelecionarDestino";
            btnSelecionarDestino.Size = new Size(145, 26);
            btnSelecionarDestino.TabIndex = 3;
            btnSelecionarDestino.Text = "Pasta de salvamento";
            btnSelecionarDestino.UseVisualStyleBackColor = true;
            // 
            // txtCaminhoDestino
            // 
            txtCaminhoDestino.Location = new Point(24, 116);
            txtCaminhoDestino.Name = "txtCaminhoDestino";
            txtCaminhoDestino.ReadOnly = true;
            txtCaminhoDestino.Size = new Size(362, 25);
            txtCaminhoDestino.TabIndex = 2;
            // 
            // rbOutraPasta
            // 
            rbOutraPasta.AutoSize = true;
            rbOutraPasta.Location = new Point(24, 77);
            rbOutraPasta.Name = "rbOutraPasta";
            rbOutraPasta.Size = new Size(266, 21);
            rbOutraPasta.TabIndex = 1;
            rbOutraPasta.TabStop = true;
            rbOutraPasta.Text = "Salvar o arquivo de texto em outra pasta";
            rbOutraPasta.UseVisualStyleBackColor = true;
            // 
            // rbMesmaPasta
            // 
            rbMesmaPasta.AutoSize = true;
            rbMesmaPasta.Location = new Point(24, 37);
            rbMesmaPasta.Name = "rbMesmaPasta";
            rbMesmaPasta.Size = new Size(330, 21);
            rbMesmaPasta.TabIndex = 0;
            rbMesmaPasta.TabStop = true;
            rbMesmaPasta.Text = "Salvar o arquivo de texto na mesma pasta do áudio";
            rbMesmaPasta.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 311);
            label1.Name = "label1";
            label1.Size = new Size(130, 17);
            label1.TabIndex = 5;
            label1.Text = "Modelo a ser usado:";
            // 
            // cmbModelo
            // 
            cmbModelo.FormattingEnabled = true;
            cmbModelo.Location = new Point(154, 307);
            cmbModelo.Name = "cmbModelo";
            cmbModelo.Size = new Size(121, 25);
            cmbModelo.TabIndex = 6;
            // 
            // numThreads
            // 
            numThreads.Location = new Point(465, 308);
            numThreads.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            numThreads.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numThreads.Name = "numThreads";
            numThreads.Size = new Size(70, 25);
            numThreads.TabIndex = 7;
            numThreads.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(330, 311);
            label2.Name = "label2";
            label2.Size = new Size(129, 17);
            label2.TabIndex = 8;
            label2.Text = "Número de Threads:";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(16, 558);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 17);
            lblStatus.TabIndex = 9;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(16, 589);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(462, 15);
            progressBar.TabIndex = 10;
            // 
            // btnRemover
            // 
            btnRemover.Location = new Point(440, 14);
            btnRemover.Name = "btnRemover";
            btnRemover.Size = new Size(131, 27);
            btnRemover.TabIndex = 11;
            btnRemover.Text = "Excluir arquivos";
            btnRemover.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 624);
            Controls.Add(btnRemover);
            Controls.Add(progressBar);
            Controls.Add(lblStatus);
            Controls.Add(label2);
            Controls.Add(numThreads);
            Controls.Add(cmbModelo);
            Controls.Add(label1);
            Controls.Add(grpSaida);
            Controls.Add(chkLimpar);
            Controls.Add(btnIniciar);
            Controls.Add(btnAdicionar);
            Controls.Add(lstArquivos);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Transcritor Opus";
            grpSaida.ResumeLayout(false);
            grpSaida.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numThreads).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstArquivos;
        private Button btnAdicionar;
        private Button btnIniciar;
        private CheckBox chkLimpar;
        private GroupBox grpSaida;
        private RadioButton rbMesmaPasta;
        private RadioButton rbOutraPasta;
        private Label label1;
        private ComboBox cmbModelo;
        private NumericUpDown numThreads;
        private Label label2;
        private Label lblStatus;
        private ProgressBar progressBar;
        private Button btnSelecionarDestino;
        private TextBox txtCaminhoDestino;
        private Button btnRemover;
    }
}
