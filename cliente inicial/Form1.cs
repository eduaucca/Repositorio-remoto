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
using System.IO;
using System.Threading;


namespace Proyecto_V4
{
    public partial class Form1 : Form
    {
        IPAddress direccion;
        IPEndPoint ipep;
        private DataTable tabla;
        string registro;
        string resultado;
        Socket socket;
        byte[] data = new byte[1024];
        Thread atender;
        int cont_invitados = 0;
        string[] usuarios_invitados = new string[4];
        string invitado;
        string mi_nombre;
        bool res_partida;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            direccion = IPAddress.Parse("192.168.56.103");
            ipep = new IPEndPoint(direccion, 9550);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Connect(ipep);
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");
            }
            catch (SocketException ee)
            {
                Console.WriteLine("Unable to connect to server. ");
                Console.WriteLine(ee);
                return;
            }
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
            B_Usuario.Enabled = true;
            B_Password.Enabled = true;
            RegisterBt.Enabled = true;
            LoginBt.Enabled = true;
            desconectarBt.Enabled = true;
            box_invi.Enabled = true;
            Invitar_Bt.Enabled = true;
            ConectarBt.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e) // Boton Logearse
        {
            String registro = "1/" + B_Usuario.Text + "/" + B_Password.Text + "\0";
            socket.Send(Encoding.ASCII.GetBytes(registro));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e) //  Boton Registrarse
        {
            String registro = "2/" + B_Usuario.Text + "/" + B_Password.Text + "\0";
            socket.Send(Encoding.ASCII.GetBytes(registro));
        }

        private void B_cuestionar_Click(object sender, EventArgs e)
        {
            if (C_1.Checked) //Dar el jugador que es  mayor de edad
            {
                registro = "3/1/";
                // Enviamos al servidor el nombre tecleado
                socket.Send(Encoding.ASCII.GetBytes(registro));
            }

            if (C_2.Checked) //Dar el id del jugador con el mayor puntaje de la partida
            {
                registro = "3/2/";
                // Enviamos al servidor el nombre tecleado
                socket.Send(Encoding.ASCII.GetBytes(registro));
            }

            if (C_3.Checked) //Dar la divison del jugador 
            {
                registro = "3/3/" + cuestionador.Text;
                // Enviamos al servidor el nombre tecleado
                socket.Send(Encoding.ASCII.GetBytes(registro));
            }
        }

        private void button2_Click_1(object sender, EventArgs e) // boton desconectar
        {

            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            socket.Send(msg);
            //Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            B_Usuario.Enabled = false;
            B_Password.Enabled = false;
            RegisterBt.Enabled = false;
            LoginBt.Enabled = false;
            desconectarBt.Enabled = false;
            box_invi.Enabled = false;
            ConectarBt.Enabled = true;
            Invitar_Bt.Enabled = false;

            MessageBox.Show("Desconectado");

        }
        public void crearForm(string nombre)
        {
            Form2 f2 = new Form2();
            f2.setNomInvitacion(nombre);
            f2.ShowDialog();
            bool res = f2.getRespuesta();
            if (res == true)
            {
                MessageBox.Show("Has aceptado");
                string mensaje = "6/" + nombre + "/"+ this.mi_nombre + "/true";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                socket.Send(msg);
            }
            else
            {
                MessageBox.Show("Has rechazado");
                string mensaje = "6/" + nombre + "/"+ this.mi_nombre+ "/false";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                socket.Send(msg);
            }
            res = this.res_partida;

        }

        private void AtenderServidor()
        {

            while (true)
            {
                //Recibimos mensaje del servidor
                socket.Receive(data);
                string[] trozos = Encoding.ASCII.GetString(data).TrimEnd('\0').Split('/');
                int codigo = Convert.ToInt32(trozos[0]);

                switch (codigo)
                {
                    case 1: // Respuesta loguearse
                        resultado = trozos[1].Split('\0')[0];
                        if (resultado == "0")
                        {
                            this.mi_nombre = B_Usuario.Text;
                            MessageBox.Show("Correctamente Logueado " + this.mi_nombre);
                        }
                        if (resultado == "1")
                        {
                            MessageBox.Show("Contraseña incorrecta");
                        }
                        if (resultado == "2")
                        {
                            MessageBox.Show("Este correo no se ha registrado ");
                        }

                        break;

                    case 2: // Respuesta Regristrarse

                        if (resultado == "0")
                        {
                            MessageBox.Show("se ha registrado correctamente");
                        }
                        if (resultado == "1")
                        {
                            MessageBox.Show("ya se ha registrado este usuario");
                        }
                        break;

                    case 3: // Respuestas de las 3 consultas
                        resultado = trozos[1].Split('\0')[0];
                        if (C_1.Checked)
                        {
                            MessageBox.Show("El jugador mayor de edad es :" + resultado);
                        }
                        if (C_2.Checked)
                        {
                            MessageBox.Show("El id del jugador con mayor puntaje es:" + resultado);
                        }
                        if (C_3.Checked)
                        {
                            MessageBox.Show("La division del jugador : " + cuestionador.Text + " es:" + resultado);
                        }
                        break;

                    case 4: // notificacion lista conectados
                        int cantidad = Convert.ToInt32(trozos[1]);
                        dataGridView1.Rows.Clear();
                        for (int i = 0; i < cantidad; i++)
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1[0, i].Value = trozos[i + 2];
                        }


                        break;
                    case 5: //Respuesta de la invitacion 
                        resultado = trozos[2].Split('\0')[0];
                        string nombres;
                        if (resultado == "SI")
                        {
                            nombres = trozos[1];
                            
                            MessageBox.Show("Se ha enviado la invitación del jugador : " + trozos[1]);
                            usuarios_invitados[cont_invitados] = this.invitado;
                            cont_invitados++;
                            ThreadStart n = delegate { crearForm(nombres); };
                            Thread t = new Thread(n);
                            t.Start(); 
                            
                           

                        }
                        else
                        {
                            MessageBox.Show("Usuario NO EXISTE.");

                            usuarios_invitados[cont_invitados] = "";
                        }
                       


                        break;
                    
                    case 6:
                        resultado = trozos[1].Split('\0')[0];
                        if (resultado == "SI") // notificacion que ha aceptado
                        {
                          
                            MessageBox.Show("Se juega la partida ");
                        }

                        else if (resultado == "NO") // notificacion que no ha aceptado
                        {
                            MessageBox.Show("Alguien ha rechazado, no se juega");
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                        break;
                }
                codigo = 0;
                trozos = null;

            }


        }

        private void Invitar_Bt_Click(object sender, EventArgs e)
        {
            if (cont_invitados == 3)
            {
                MessageBox.Show("Se ha superado el numero máximo de jugadores");
            }
            else
            {
                string jugador = box_invi.Text;
                invitado = jugador;
                bool encontrado = false;
                int i = 0;
                while (i < this.cont_invitados && encontrado == false)
                {
                    if (usuarios_invitados[i] == jugador)
                    {
                        encontrado = true;
                    }
                    i++;
                }
                if (encontrado == false)
                {
                    //usua_invitados[cont_invi] = jugador;
                    string mensaje = "5/" + jugador;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    socket.Send(msg);

                }
                else
                {
                    MessageBox.Show("Ya has invitado a esta persona");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            B_Usuario.Enabled = false;
            B_Password.Enabled = false;
            RegisterBt.Enabled = false;
            LoginBt.Enabled = false;
            desconectarBt.Enabled = false;
            box_invi.Enabled = false;
            Invitar_Bt.Enabled = false;
            B_Usuario.Text = "Edu";
            B_Password.Text = "manedu";
        }




    }
}

