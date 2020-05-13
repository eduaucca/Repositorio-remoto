namespace Proyecto_V4
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
            this.RegisterBt = new System.Windows.Forms.Button();
            this.desconectarBt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.box_invi = new System.Windows.Forms.TextBox();
            this.Invitar_Bt = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.C_1.Location = new System.Drawing.Point(242, 23);
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
            this.C_3.Location = new System.Drawing.Point(242, 87);
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
            this.C_2.Location = new System.Drawing.Point(242, 56);
            this.C_2.Name = "C_2";
            this.C_2.Size = new System.Drawing.Size(267, 17);
            this.C_2.TabIndex = 9;
            this.C_2.TabStop = true;
            this.C_2.Text = "Dame el id del jugador que tenga el mayor puntaje :";
            this.C_2.UseVisualStyleBackColor = true;
            // 
            // cuestionador
            // 
            this.cuestionador.Location = new System.Drawing.Point(242, 150);
            this.cuestionador.Name = "cuestionador";
            this.cuestionador.Size = new System.Drawing.Size(135, 20);
            this.cuestionador.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Completa tu pregunta:";
            // 
            // B_cuestionar
            // 
            this.B_cuestionar.Location = new System.Drawing.Point(256, 176);
            this.B_cuestionar.Name = "B_cuestionar";
            this.B_cuestionar.Size = new System.Drawing.Size(106, 33);
            this.B_cuestionar.TabIndex = 12;
            this.B_cuestionar.Text = "Cuestionar";
            this.B_cuestionar.UseVisualStyleBackColor = true;
            this.B_cuestionar.Click += new System.EventHandler(this.B_cuestionar_Click);
            // 
            // RegisterBt
            // 
            this.RegisterBt.Location = new System.Drawing.Point(45, 239);
            this.RegisterBt.Name = "RegisterBt";
            this.RegisterBt.Size = new System.Drawing.Size(112, 41);
            this.RegisterBt.TabIndex = 13;
            this.RegisterBt.Text = "Register";
            this.RegisterBt.UseVisualStyleBackColor = true;
            // 
            // desconectarBt
            // 
            this.desconectarBt.Location = new System.Drawing.Point(43, 393);
            this.desconectarBt.Name = "desconectarBt";
            this.desconectarBt.Size = new System.Drawing.Size(189, 35);
            this.desconectarBt.TabIndex = 14;
            this.desconectarBt.Text = "Desconectar";
            this.desconectarBt.UseVisualStyleBackColor = true;
            this.desconectarBt.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(483, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(241, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Escribe el nombre del jugador que quieres invitar :\r\n";
            // 
            // box_invi
            // 
            this.box_invi.Location = new System.Drawing.Point(547, 328);
            this.box_invi.Name = "box_invi";
            this.box_invi.Size = new System.Drawing.Size(141, 20);
            this.box_invi.TabIndex = 18;
            // 
            // Invitar_Bt
            // 
            this.Invitar_Bt.Location = new System.Drawing.Point(722, 325);
            this.Invitar_Bt.Name = "Invitar_Bt";
            this.Invitar_Bt.Size = new System.Drawing.Size(75, 23);
            this.Invitar_Bt.TabIndex = 23;
            this.Invitar_Bt.Text = "Invitar";
            this.Invitar_Bt.UseVisualStyleBackColor = true;
            this.Invitar_Bt.Click += new System.EventHandler(this.Invitar_Bt_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(557, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(154, 233);
            this.dataGridView1.TabIndex = 24;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(889, 489);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Invitar_Bt);
            this.Controls.Add(this.box_invi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.desconectarBt);
            this.Controls.Add(this.RegisterBt);
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
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.Button RegisterBt;
        private System.Windows.Forms.Button desconectarBt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox box_invi;
        private System.Windows.Forms.Button Invitar_Bt;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}

