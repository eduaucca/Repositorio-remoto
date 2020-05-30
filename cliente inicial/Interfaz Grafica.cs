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
    public partial class Interfaz_Grafica : Form
    {
        Socket server;
        delegate void guardarNombre();
        string minombre;
        int numero_personaje;
        private void ponerNombre()
        {
            lbl_nombre.Text = this.minombre;
        }

       
        public Interfaz_Grafica(int personaje, Socket server, string nombre)
        {
            InitializeComponent();
            this.minombre = nombre;
            lbl_nombre.Text = this.minombre;
            this.numero_personaje = personaje;
            this.server = server;
        }

       
        private void button1_Click(object sender, EventArgs e) // boton para enviar mensaje
        {
            string mensaje = "7/" + box_msj.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        public void mostrarFrase(string frase)
        {
            lbl_mensaje.Text += "\n"+ frase + "\n";
        }
        delegate void PonerFrase(string frase);
        public void guardarFrase(string frase)
        {
            PonerFrase delegadofrase = new PonerFrase(mostrarFrase);
            lbl_mensaje.Invoke(delegadofrase, new object[] { frase });
        }
    }
}
