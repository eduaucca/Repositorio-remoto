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

namespace Proyecto_V1
{
    public partial class Form1 : Form
    {
        private DataTable tabla;
        string registro;
        string resultado;
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("192.168.56.102"), 9570);
        byte[] data = new byte[1024];
        Thread atender;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                socket.Connect(remoteEP);
                this.BackColor = Color.Green;
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
        }
        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                socket.Receive(data);
                string[] trozos = Encoding.ASCII.GetString(data).TrimEnd('\0').Split('/');
                int codigo = Convert.ToInt32(trozos[0]);

       
                switch (codigo)
                {
                    case 1: // Respuesta loguearse
                        resultado = trozos[1].Split('\0')[0];
                        if (resultado == "0")
                        {
                            MessageBox.Show("Correctamente Logueado");
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
                        resultado = trozos[1].Split('\0')[0];
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

                    case 4 :
                        int cantidad = Convert.ToInt32(trozos[1]);
                            Lista_Conectados.Rows.Clear();
                        for (int i = 0; i < cantidad; i++)
                        {
                            Lista_Conectados.Rows.Add();
                            Lista_Conectados[0, i].Value = trozos[i + 2];
                        }
                        
                        break;

                  
                }
            }


        }

        /*  private void Lista_ConBt_Click(object sender, EventArgs e)
          {
            
              string[] mensaje2;
              tabla = new DataTable();
              //crear columna y fila
              DataColumn column;
              DataRow row;
              //Crear la columna Usuario
              column = new DataColumn();
              column.DataType = System.Type.GetType("System.String");
              column.ColumnName = "Usuario";
              column.ReadOnly = true;
              column.Unique = true;
              //añadir a la tabla
              this.tabla.Columns.Add(column);

              //Limpiamos info de la tabla
              tabla.Rows.Clear();

              //Asignamos el numero 6 pedir lista conectados
              string mensaje = "6/";
              byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
              socket.Send(msg);
              //Recibimos la respuesta del servidor
              byte[] msg2 = new byte[80];
              socket.Receive(msg2);
              mensaje2 = Encoding.ASCII.GetString(msg2).TrimEnd('\0').Split('/');
            
              //Separamos los conectados
              string[] ListaSeparada = mensaje2;
              ListaSeparada[ListaSeparada.Length - 1] = ListaSeparada[ListaSeparada.Length - 1].Split('\0')[0];
              int i;
              //ListaSeparada = conectados.Split('/');

              //tabla.Columns.Clear();
              //Colocamos info en la tabla
              for (i = 0; i < ListaSeparada.Length; i++)
              {
                  row = tabla.NewRow();
                  row["Usuario"] = ListaSeparada[i];
                  tabla.Rows.Add(row);
                  // ListaSeparada = conectados.Split('/');
              }
              //añadimos la tabla al grid
              dataGridView1.DataSource = tabla;
            
          } */

    }
}

