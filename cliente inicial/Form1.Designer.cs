namespace Proyecto_V1
{
    partial class Form1
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
            this.ConectarBt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.B_Usuario = new System.Windows.Forms.TextBox();
            this.B_Password = new System.Windows.Forms.TextBox();
            this.LoginBt = new System.Windows.Forms.Button();
            this.C_1 = new System.Windows.Forms.RadioButton();
            this.C_3 = new System.Windows.Forms.RadioButton();
            this.C_2 = new System.Windows.Forms.RadioButton();
            this.cuestionador = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.B_cuestionar = new System.Windows.Forms.Button();
            this.RegisteBt = new System.Windows.Forms.Button();
            this.desconectarBt = new System.Windows.Forms.Button();
            this.Lista_Conectados = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Lista_Conectados)).BeginInit();
            this.SuspendLayout();
            // 
            // ConectarBt
            // 
            this.ConectarBt.Location = new System.Drawing.Point(43, 352);
            this.ConectarBt.Name = "ConectarBt";
            this.ConectarBt.Size = new System.Drawing.Size(189, 35);
            this.ConectarBt.TabIndex = 0;
            this.ConectarBt.Text = "Conectar";
            this.ConectarBt.UseVisualStyleBackColor = true;
            this.ConectarBt.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Usuario :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña :";
            // 
            // B_Usuario
            // 
            this.B_Usuario.Location = new System.Drawing.Point(43, 53);
            this.B_Usuario.Name = "B_Usuario";
            this.B_Usuario.Size = new System.Drawing.Size(100, 20);
            this.B_Usuario.TabIndex = 3;
            // 
            // B_Password
            // 
            this.B_Password.Location = new System.Drawing.Point(43, 115);
            this.B_Password.Name = "B_Password";
            this.B_Password.Size = new System.Drawing.Size(100, 20);
            this.B_Password.TabIndex = 4;
            // 
            // LoginBt
            // 
            this.LoginBt.Location = new System.Drawing.Point(45, 169);
            this.LoginBt.Name = "LoginBt";
            this.LoginBt.Size = new System.Drawing.Size(75, 31);
            this.LoginBt.TabIndex = 5;
            this.LoginBt.Text = "Login";
            this.LoginBt.UseVisualStyleBackColor = true;
            this.LoginBt.Click += new System.EventHandler(this.button2_Click);
            // 
            // C_1
            // 
            this.C_1.AutoSize = true;
            this.C_1.Location = new System.Drawing.Point(282, 23);
            this.C_1.Name = "C_1";
            this.C_1.Size = new System.Drawing.Size(219, 17);
            this.C_1.TabIndex = 7;
            this.C_1.TabStop = true;
            this.C_1.Text = "Dame el jugador que sea mayor de edad:";
            this.C_1.UseVisualStyleBackColor = true;
            // 
            // C_3
            // 
            this.C_3.AutoSize = true;
            this.C_3.Location = new System.Drawing.Point(282, 94);
            this.C_3.Name = "C_3";
            this.C_3.Size = new System.Drawing.Size(201, 17);
            this.C_3.TabIndex = 8;
            this.C_3.TabStop = true;
            this.C_3.Text = "Dame la clasificacion de este jugador";
            this.C_3.UseVisualStyleBackColor = true;
            // 
            // C_2
            // 
            this.C_2.AutoSize = true;
            this.C_2.Location = new System.Drawing.Point(282, 57);
            this.C_2.Name = "C_2";
            this.C_2.Size = new System.Drawing.Size(267, 17);
            this.C_2.TabIndex = 9;
            this.C_2.TabStop = true;
            this.C_2.Text = "Dame el id del jugador que tenga el mayor puntaje :";
            this.C_2.UseVisualStyleBackColor = true;
            // 
            // cuestionador
            // 
            this.cuestionador.Location = new System.Drawing.Point(282, 151);
            this.cuestionador.Name = "cuestionador";
            this.cuestionador.Size = new System.Drawing.Size(135, 20);
            this.cuestionador.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(279, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Completa tu pregunta:";
            // 
            // B_cuestionar
            // 
            this.B_cuestionar.Location = new System.Drawing.Point(300, 208);
            this.B_cuestionar.Name = "B_cuestionar";
            this.B_cuestionar.Size = new System.Drawing.Size(106, 33);
            this.B_cuestionar.TabIndex = 12;
            this.B_cuestionar.Text = "Cuestionar";
            this.B_cuestionar.UseVisualStyleBackColor = true;
            this.B_cuestionar.Click += new System.EventHandler(this.B_cuestionar_Click);
            // 
            // RegisteBt
            // 
            this.RegisteBt.Location = new System.Drawing.Point(45, 239);
            this.RegisteBt.Name = "RegisteBt";
            this.RegisteBt.Size = new System.Drawing.Size(112, 41);
            this.RegisteBt.TabIndex = 13;
            this.RegisteBt.Text = "Register";
            this.RegisteBt.UseVisualStyleBackColor = true;
            // 
            // desconectarBt
            // 
            this.desconectarBt.Location = new System.Drawing.Point(340, 352);
            this.desconectarBt.Name = "desconectarBt";
            this.desconectarBt.Size = new System.Drawing.Size(209, 35);
            this.desconectarBt.TabIndex = 14;
            this.desconectarBt.Text = "Desconectar";
            this.desconectarBt.UseVisualStyleBackColor = true;
            this.desconectarBt.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Lista_Conectados
            // 
            this.Lista_Conectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Lista_Conectados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.Lista_Conectados.Location = new System.Drawing.Point(611, 27);
            this.Lista_Conectados.Name = "Lista_Conectados";
            this.Lista_Conectados.Size = new System.Drawing.Size(144, 222);
            this.Lista_Conectados.TabIndex = 16;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(889, 463);
            this.Controls.Add(this.Lista_Conectados);
            this.Controls.Add(this.desconectarBt);
            this.Controls.Add(this.RegisteBt);
            this.Controls.Add(this.B_cuestionar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cuestionador);
            this.Controls.Add(this.C_2);
            this.Controls.Add(this.C_3);
            this.Controls.Add(this.C_1);
            this.Controls.Add(this.LoginBt);
            this.Controls.Add(this.B_Password);
            this.Controls.Add(this.B_Usuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConectarBt);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Lista_Conectados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConectarBt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox B_Usuario;
        private System.Windows.Forms.TextBox B_Password;
        private System.Windows.Forms.Button LoginBt;
        private System.Windows.Forms.RadioButton C_1;
        private System.Windows.Forms.RadioButton C_3;
        private System.Windows.Forms.RadioButton C_2;
        private System.Windows.Forms.TextBox cuestionador;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button B_cuestionar;
        private System.Windows.Forms.Button RegisteBt;
        private System.Windows.Forms.Button desconectarBt;
        private System.Windows.Forms.DataGridView Lista_Conectados;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}

