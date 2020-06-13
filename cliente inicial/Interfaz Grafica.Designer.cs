namespace Proyecto_V4
{
    partial class Interfaz_Grafica
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
            this.lbl_nombre = new System.Windows.Forms.Label();
            this.box_msj = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_mensaje = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_nombre
            // 
            this.lbl_nombre.AutoSize = true;
            this.lbl_nombre.Location = new System.Drawing.Point(584, 28);
            this.lbl_nombre.Name = "lbl_nombre";
            this.lbl_nombre.Size = new System.Drawing.Size(42, 13);
            this.lbl_nombre.TabIndex = 0;
            this.lbl_nombre.Text = "nombre";
            // 
            // box_msj
            // 
            this.box_msj.Location = new System.Drawing.Point(38, 39);
            this.box_msj.Name = "box_msj";
            this.box_msj.Size = new System.Drawing.Size(199, 20);
            this.box_msj.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(272, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Enviar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_mensaje
            // 
            this.lbl_mensaje.AutoSize = true;
            this.lbl_mensaje.Location = new System.Drawing.Point(35, 77);
            this.lbl_mensaje.Name = "lbl_mensaje";
            this.lbl_mensaje.Size = new System.Drawing.Size(35, 13);
            this.lbl_mensaje.TabIndex = 7;
            this.lbl_mensaje.Text = "Chat :";
            // 
            // Interfaz_Grafica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 292);
            this.Controls.Add(this.lbl_mensaje);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.box_msj);
            this.Controls.Add(this.lbl_nombre);
            this.Name = "Interfaz_Grafica";
            this.Text = "Interfaz_Grafica";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_nombre;
        private System.Windows.Forms.TextBox box_msj;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_mensaje;

        public System.EventHandler label5_Click { get; set; }
    }
}