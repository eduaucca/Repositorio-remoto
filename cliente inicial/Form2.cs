using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
namespace Proyecto_V4
{
    public partial class Form2 : Form
    {
        bool respuesta;
        string jugador;
        string nom_invitacion;
        public Form2()
        {
            InitializeComponent();
        }

        private void Aceptar_Bt_Click(object sender, EventArgs e)
        {
            respuesta = true;
            Close();
        }

        private void Rechazar_Bt_Click(object sender, EventArgs e)
        {
            respuesta = false;
            Close();
        }
        public void setNombre(string jugador)
        {
            this.jugador = jugador;
        }
        public bool getRespuesta()
        {
            return this.respuesta;
        }
        public void setNomInvitacion(string nombre)
        {
            this.nom_invitacion = nombre;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label_mensaje.Text = nom_invitacion + " : te ha invitado a que te unas a una partida . . .";
        }

    }
}
