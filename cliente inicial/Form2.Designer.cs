namespace Proyecto_V4
{
    partial class Form2
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
            this.Aceptar_Bt = new System.Windows.Forms.Button();
            this.Rechazar_Bt = new System.Windows.Forms.Button();
            this.label_mensaje = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Aceptar_Bt
            // 
            this.Aceptar_Bt.Location = new System.Drawing.Point(42, 119);
            this.Aceptar_Bt.Name = "Aceptar_Bt";
            this.Aceptar_Bt.Size = new System.Drawing.Size(86, 29);
            this.Aceptar_Bt.TabIndex = 22;
            this.Aceptar_Bt.Text = "Aceptar";
            this.Aceptar_Bt.UseVisualStyleBackColor = true;
            this.Aceptar_Bt.Click += new System.EventHandler(this.Aceptar_Bt_Click);
            // 
            // Rechazar_Bt
            // 
            this.Rechazar_Bt.Location = new System.Drawing.Point(175, 119);
            this.Rechazar_Bt.Name = "Rechazar_Bt";
            this.Rechazar_Bt.Size = new System.Drawing.Size(86, 29);
            this.Rechazar_Bt.TabIndex = 23;
            this.Rechazar_Bt.Text = "Rechazar";
            this.Rechazar_Bt.UseVisualStyleBackColor = true;
            this.Rechazar_Bt.Click += new System.EventHandler(this.Rechazar_Bt_Click);
            // 
            // label_mensaje
            // 
            this.label_mensaje.AutoSize = true;
            this.label_mensaje.Location = new System.Drawing.Point(39, 52);
            this.label_mensaje.Name = "label_mensaje";
            this.label_mensaje.Size = new System.Drawing.Size(74, 13);
            this.label_mensaje.TabIndex = 24;
            this.label_mensaje.Text = "label_mensaje";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 261);
            this.Controls.Add(this.label_mensaje);
            this.Controls.Add(this.Rechazar_Bt);
            this.Controls.Add(this.Aceptar_Bt);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Aceptar_Bt;
        private System.Windows.Forms.Button Rechazar_Bt;
        private System.Windows.Forms.Label label_mensaje;
    }
}