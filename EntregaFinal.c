#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>
#include <stdbool.h>
//#include <my_global.h>
typedef struct
{
	char usuario[50];
	int socket;
}TUsuario;

typedef struct
{
	TUsuario usuarios[100];
	int num;
}
TConectados;
TConectados lista;
// Lista invitados
typedef struct
{
	char nombre[30];
	int notificacion;
	int socket;
}TUsuario_invitado;
typedef struct
{
	TUsuario_invitado usuarios[100];
	int num;
}TInvitados;
TInvitados lista_partida;
int partida_activa = 0;
char creador_partida[20];
int num_jugadores = 0;

//MYSQL//
int err;
int sockets[100];
char consulta[100];//Lo usamos para preparar las conultas
char insertar[100];
MYSQL *conn;//Conector para acceder al servidor de bases de datos
MYSQL_RES *resultado;//variable para comprobar errores
MYSQL_ROW row;
//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

char cuestionador[30];
//SOCKETS//
int sock_conn, sock_listen, ret;
char buff[512];
char buff2[512];


//Da respuesta al resultado del Check_Log


//Devuelve 0 si se loguea bien
//Devuelve 1 si no
int Log_In (char nombre[20], char password[20], int socket_conn)
{
	int comparador;
	
	//	 Ahora recibimos sus credenciales, que dejamos en buff*/
	printf ("Credenciales recibidas\n");
	
	
	//preparamos la consulta en modo SQL*/
	strcpy(consulta,"SELECT pass FROM JUGADOR WHERE nombre = '");
	strcat(consulta,nombre);
	strcat(consulta,"'");
	mysql_query(conn,consulta); //almaceno el resultado de la consulta en modo SQL dentro de "conn"*/
	if (err!=0) {
		
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL) 
	{
		printf("This name isn't in our database");
		write(sock_conn, "1/2",sizeof("1/2"));
		return -1;
	}
	
	
	comparador = strcmp(password, row[0]); //comparamos la password dada con la real
	if (comparador == 0) //Password correcta*/
	{
	
		return 0;
	}
	if (comparador != 0) //Password incorrecta
	{
		
		return 1;
	}
}



int Register (char nombre[20], char password[20])
{
	char insertar[40];
	
	//Si el usuario esta en la BD:
	//Nos genera una tabla con el nombre del usuario
	
	strcpy(consulta,"SELECT JUGADOR.nombre FROM JUGADOR WHERE JUGADOR.nombre =");
	sprintf(consulta, "%s'%s';",consulta,nombre);
	err=mysql_query (conn, consulta);
	printf("La consulta es: %s\n",consulta);
	if (err!=0) 
	{
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row (resultado);
	printf("Buscamos al jugador....\n");
	//Si no encuentra el usuario, la tabla es NULL
	//Cumplimos la condicion para registrarnos
	if(row == NULL) 
	{
		printf("Jugador no encontrado, lo a?adimos \n");
		strcpy(consulta,"INSERT INTO JUGADOR (nombre,pass) VALUES (");
		sprintf (insertar,"%s'%s','%s');",insertar,nombre,password);
		err=mysql_query (conn, insertar);
		if (err!=0) 
		{
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		
		printf("Jugador a?adido con exito!\n");
		write(sock_conn, "2/0", sizeof("2/0"));
		return 0;
	}
	else
	{
		write(sock_conn, "2/1", sizeof("2/1"));
		return 1;
	}
}

void Update_Conectados (TConectados *l)
{
	int i = 0;
	char conectados [200];
	sprintf (conectados, "4/%d/",l->num);
	
	for (i = 0; i < l->num; i++)
	{
		strcat(conectados, l-> usuarios[i].usuario);
		strcat(conectados, "/");
	}
	
	
	printf("Respuesta: %s\n",conectados);
	
	
	// Para informar a los demas usuarios
	for ( i = 0; i < l->num; i++)
	{
		write(l->usuarios[i].socket, conectados, sizeof(conectados));
		printf("numero de la posicion : %d\n", l->num);
	}
	printf("Informacion enviada a los otros usuarios : %s\n", conectados);
}


int Add_User (TConectados *l, int socket, char usuario[50])
{
	if (l->num < 100)
	{
		strcpy(l-> usuarios[l->num].usuario, usuario);
		l-> usuarios[l->num].socket = socket;
		l->num ++;
		//write(socket, "2/0", sizeof("2/0"));
		return 0;
	}
	else
		
		return -1;
	//write(socket, "0", sizeof("0"));
	
	
}

void DameNombre_MayorEdad(char mayores[80])
{
	
	printf("Pregunta recibida\n");
	
	strcpy(consulta, "SELECT JUGADOR.nombre FROM JUGADOR WHERE JUGADOR.edad >=18;");
	mysql_query(conn,consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row (resultado);
	
	printf("Cuestion realizada sin errores\n");
	printf("Nombre: %s\n",row[0]);
	strcpy(mayores,row[0]);
	/*while(row != NULL)
	{
	sprintf (mayores, "%s/",mayores);
	} */
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
}	
void DameID_MayorPuntaje(char id[40])
{
	printf("Pregunta recibida\n");
	
	strcpy(consulta,"SELECT JUGADOR FROM JUGADOR, HISTORIAL WHERE HISTORIAL.puntuacion=");
	//strcat(consulta, cuestionador);
	strcat(consulta,"(SELECT MAX(HISTORIAL.puntuacion) FROM HISTORIAL);");
	
	
	
	///
	//err=mysql_query (conn, "SELECT * FROM HISTORIAL");
	err=mysql_query (conn,consulta);
	if (err!=0)
	{
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
	{
		printf ("el ID del jugador con mayor puntaje es : %s\n",
				row[0]);
		//row = mysql_fetch_row (consulta);
		printf("id: %s\n",row[0]);
		
		strcpy (id,row[0]);
		
	}
	
	
	
}
void DameElo_jugador (char division [40])
{
	char*p;
	p = strtok (NULL, "/");
	strcpy(cuestionador, p); //almacenamos el nombre escrito por el ususario
	printf("Pregunta recibida\n");
	
	strcpy(consulta, "SELECT JUGADOR.elo FROM JUGADOR WHERE JUGADOR.nombre = '");
	strcat(consulta, cuestionador);
	strcat (consulta,"';");
	
	mysql_query(conn,consulta);
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row (resultado);
	
	printf("Cuestion realizada sin errores\n");
	printf("division: %s\n",row[0]);
	strcpy (division,row[0]);
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
}

void enviarMensajeUsuarios(TInvitados *l , char respuesta[10])
{
	char mensaje[80];
	int i ;
	if(strcmp(respuesta, "SI") == 0)
	{
		for(i = 0; i < l->num;i++)
		{
			
			sprintf(mensaje, "6/SI/%d/",i);
			
			printf("Este mensaje el invitado envia al cliente invitador : %s\n",mensaje);
			write(l->usuarios[i].socket, mensaje, strlen(mensaje));
		
		}
	}
	else if (strcmp(respuesta, "NO") == 0)
	{
		for( i = 0; i < l->num; i++)
		{
			sprintf(mensaje, "6/NO/");
			printf("Este mensaje el invitado envia al cliente invitador : %s\n",mensaje);
			write(l->usuarios[i].socket,mensaje,strlen(mensaje));
		}
	}
}
int comprobar_TodosAceptan(TInvitados *l)
{
	for(int i = 0;i < l->num; i++)
	{
		if(l->usuarios[i].notificacion == 0)
		{
			return 1;
		}
		else if (l->usuarios[i].notificacion == -1)
		{
			return 2;
		}
	}
	return 0;
}
void GuardarConfirmacion(TInvitados *l, char n[30], int confirmar)
{
	for(int i = 0;i < l->num;i++)
	{
		if(strcmp(l->usuarios[i].nombre,n)==0)
		{
			l->usuarios[i].notificacion = confirmar;
			break;
		}
	}
}
void Dame_ListaConectados(TConectados *l) // nos mostrara la lista de conectados
{
	for(int i = 0; i< l->num; i++)
	{
		printf("%d -> usuario: %s | socket : %d\n",i,l->usuarios[i].usuario,l->usuarios[i].socket);
	}
}
void Dame_ListaInvitados(TInvitados *l) // nos mostrara la lista de invitados
{   printf("se muestra la lista de invitados\n");
	for (int i = 0; i< l->num;i++)
	{
		printf("%d -> usuario : %s | socket: %d | respuesta: %d\n",i , l->usuarios[i].nombre,l->usuarios[i].socket,l->usuarios[i].notificacion);
	}
}
int Dame_Num (char usuario[20], TConectados *l) //comprueba en que posicion esta el usuario
{
	int Num;
	for (int i = 0; i <= l->num; i++)
	{
		if (strcmp(l->usuarios[i].usuario, usuario) == 0)
		{
			Num = i;
			return Num;
		}
	}
}
 void Invitacion (int socket,char nombreInvitador[20])
{
	//Escribimos el mensaje de invitacion
	char mensaje[200];
	sprintf(mensaje, "5/%s/SI",nombreInvitador);
	printf ("Servidor envia el mensaje de invitacion al cliente invitado : %s\n", mensaje);
	//Mandamos el mensaje
	write(socket, mensaje, sizeof(mensaje));
} 


void Delete_user_conect(TConectados *lista, char usuario[40], int socket )
{
	//Buscamos usuario al que queremos eliminar en el vector "Conectados"
	int i = 0;
	bool encontrado = false;
	
	
	for(i ; i< lista->num ; i++)
	{
		if (strcmp(lista->usuarios[i].usuario,usuario) == 0)
		{
			encontrado = true; 
			int j = i;
			for (j;j < (lista->num-1);j++)
			{
				strcpy(lista->usuarios[j].usuario,lista->usuarios[j+1].usuario);
				lista->usuarios[j].socket=lista->usuarios[j+1].socket; // se elimina el usuario conectado
			}
			
		}
		
	}
	if (encontrado == true)
	{
		printf("DeleteUserConect encontrado, user: %s\n", usuario);
		lista->num--;
	}
	else
	{
		printf("DeleteUserConect NO encontrado, user: %s\n", usuario);
	}
}
/*int DameSocket(TConectados *l,char nombre[40])
{
	//Devuelve el socket
	int i = 0;
	int encontrado = 0;
	while ((i<l->num) && (encontrado ==0))
	{
		if(strcmp(l->usuarios[i].usuario,nombre)==0)
			encontrado = 1;
		if (encontrado==0)
			i = i+1;
	}
	if (encontrado == 1)
		return l->usuarios[i].socket;
	else 
		return -1;
}
*/
int Dame_SocketConectado (TConectados *lista, char nombre[300], char socket[])
{ 
	
	int i;
	for (i=0; i< lista->num; i++)
	{
		if(strcmp(lista->usuarios[i].usuario,nombre)==0)
		
		{
			sprintf (socket, "%d",lista->usuarios[i].socket);
		
		return 0;
		}
	}
	return 1;
}
void enviarFrase(TInvitados *l,char frase[20],char nombre[20])
{
	char mensaje[80];
	sprintf(mensaje, "7/%s - %s/",nombre, frase);
	for(int i = 0; i < l->num; i++)
	{
		write(l->usuarios[i].socket,mensaje,strlen(mensaje));
	}	
}
void *AtenderCliente(void *socket)
{ // Mandamos la lista de conectados nada mas conectarse
	Update_Conectados(&lista);
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;
	
	char password[20];
	char nombre[30];
	
	int cuestion;
	char respuesta[512];
	int terminar = 0;
	
	while(terminar == 0)
	{	
		
		ret=read(sock_conn,buff, sizeof(buff));
		buff[ret] = '\0';
		
		char *p = strtok(buff, "/");
		
		int select = atoi(p);
		printf("seleccion: %d\n", select);
		
		if(select == 1) //Log In 0 -> correcta || 1 -> contrase?a incorrecta
		{
			printf("entramos en la seleccion 1 (login)\n");
			
			char usuario[20]; //
			
			p = strtok (NULL, "/");
			strcpy(nombre, p); //almacenamos el nombre escrito por el ususario
			p = strtok (NULL, "/");
			strcpy (password, p); //almacenamos la password dada por el usuario
			
			
			// Ahora recibimos sus credenciales, que dejamos en buff
			
			int	logueado = Log_In (nombre, password,sock_conn);
			
			
			if (logueado == 0) // se loguea correctamente
			{
				printf("Correctly Logged\n");
				write(sock_conn,"1/0", sizeof("1/0"));
				pthread_mutex_lock(&mutex);
				int add = Add_User(&lista, sock_conn, nombre);
				pthread_mutex_unlock(&mutex);
				Update_Conectados(&lista);
				
				
				if (add == 0) 
				{
					printf ("Se ha añadido al usuario correctamente\n");
					// se añade correctamente
				}
				else if (add == -1)
				{
					printf ("No se ha podido añadir al usuario\n"); // la lista esta llena, no se puede a?adir
				}
				
				
			}
			
			if(logueado == 1)
			{
				printf("contraseña incorrecta");
				write(sock_conn,"1/1", sizeof("1/0"));
			}
			else if(logueado == -1)
			{
				printf("el nombre no se encuentra en la DB"); // el nombre no esta en la DB
			}
			
			
			
			
			// Muestrame los conectados de la lista
			
			Dame_ListaConectados(&lista);
			
			select = 100;
			
		}
		
		if (select == 2) //Registro
		{
			p = strtok (NULL, "/");
			strcpy (password, p); //almacenamos la password dada por el usuario
			p = strtok (NULL, "/");
			strcpy(nombre, p);//almacenamos el nombre dado por el usuario
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora
			int registro = Register(nombre, password);
			
			if (registro == 0)
			{
				printf ("Se ha registrado correctamente");
			}
			else if (registro == 1)
			{
				printf ("No se ha podido registrar");
			}
			pthread_mutex_unlock( &mutex); // ahora puede interrumpirme
			
			select = 100;
		}
		
		/*if (select == 3) //Dar el nombre del jugador mayor de edad
		{ 
			p = strtok (NULL, "/");
			cuestion = atoi(p);
			if(cuestion == 1) //Dar el nombre del jugador mayor de edad
			{
				char nombre[40];
				char respuesta[100] = "3/";
				printf("Este es la primera pregunta\n");
				DameNombre_MayorEdad(nombre);
				strcat(respuesta,nombre);
				write(sock_conn, respuesta, sizeof(respuesta));
				
			}
			if(cuestion == 2) // Da la id del jugador con mayor puntaje
			{
				char identificador[40];
				char respuesta[100] = "3/";
				printf("Este es la segunda pregunta\n");
				DameID_MayorPuntaje(respuesta);
				strcat(respuesta,identificador);
				write(sock_conn, respuesta, sizeof(respuesta));
				
			}
			if(cuestion == 3) // Da la division del jugador
			{
				char division[40];
				char respuesta[100] = "3/";
				
				printf("Este es la tercera pregunta\n");
				DameElo_jugador(division);
				strcat(respuesta,division);
				printf("%s",respuesta);
				write(sock_conn, respuesta, sizeof(respuesta));
				
			}
			select = 100;
			
			
			
		}*/
		if (select == 3) // Peticion al servidor para que el usuario pueda crear la partida
		{   
			p = strtok (NULL, "/");
			int numero_invitados = atoi(p);
			
			if(partida_activa == 1 && strcmp(creador_partida,nombre)!=0)
			{	printf("Existe una partida creada \n");
			}
			 
	       else 
			{
			
			partida_activa = 1;
			strcpy(creador_partida, nombre); // 
			printf("Creador de la partida: %s\n",creador_partida);
			printf("Empieza el mutex\n");
			printf("Numero de invitados: %d",numero_invitados);
			//Verificamos el numero de invitados 
			pthread_mutex_lock(&mutex);
			
			for(int i =0 ; i< numero_invitados+1; i++)
			{
				
				strcpy(lista_partida.usuarios[i].nombre,"");
			lista_partida.usuarios[i].notificacion = -1 ; // el cliente que invita va crear la partida
			lista_partida.usuarios[i].socket = 0;
			} 
			lista_partida.num = numero_invitados + 1;
			
			//Guarda el creador de la partida
			strcpy(lista_partida.usuarios[0].nombre,nombre);
			lista_partida.usuarios[0].notificacion = 1 ; 
			lista_partida.usuarios[0].socket = sock_conn;
			num_jugadores = num_jugadores + 1;
			pthread_mutex_unlock(&mutex);
			printf("Termina el mutex\n");
			Dame_ListaInvitados(&lista_partida);
			
			printf("Se mostro la lista de invitados");
			
			sprintf(respuesta, "3/SI");
			write(sock_conn,respuesta,sizeof(respuesta));
			
			}
		}
		if(select == 5) // Enviar la invitacion
		{
			
			printf("entramos a la seleccion 5\n");
			
		     
			
			if( strcmp(nombre,lista_partida.usuarios[0].nombre )==0)
			{ 
				
				
				// INVITAR
				printf("partida_activa : %d\n",partida_activa);
				Dame_ListaConectados(&lista);
				//Procedemos a la invitacion
				
				p = strtok (NULL, "/"); // Arrancamos el nombre del invitado
				char Invitado [20];
				strcpy(Invitado,p);
				char sock_inv[10];
				int resp = Dame_SocketConectado(&lista,Invitado,sock_inv);
				if(resp == 0)
				{
					printf("Ahora empieza el mutex \n");
					pthread_mutex_lock(&mutex);
					//Verificamos el numero de invitados
					strcpy(lista_partida.usuarios[num_jugadores].nombre, Invitado);
					 // todavia no ha aceptado (no ha respondido el invitado)
					lista_partida.usuarios[num_jugadores].socket = atoi(sock_inv); //Se guarda el socket para luego poder invitar a un usuario a jugar 
					num_jugadores = num_jugadores + 1;
					printf("Ahora termina el mutex\n");
					pthread_mutex_unlock(&mutex);
					printf ("A continuacion vemos la lista de invitados\n");
					
					Dame_ListaInvitados(&lista_partida);
					
					printf ("Fin de la lista invitados\n");
					
					printf("Socket de la persona que vamos a invitar : %s\n",sock_inv);
					Invitacion(atoi(sock_inv),nombre);
					
					
				}
				else
				{
					printf ("NO\n");
					strcpy (respuesta, "5/NO");
					printf("Respuesta al cliente invitado : %s\n",respuesta);
					//enviamos la respuesta 
					write(sock_conn,respuesta,strlen(respuesta));
				}
				
				
			
			select = 100;
			
		    
			}
		}
		if (select == 6) //Responder a la invitacion
		{ 
			printf ("Entramos a la seleccion 6\n");
			
			p= strtok(NULL, "/");
			char creador_partida[40];
			strcpy(creador_partida,p);
			
			p= strtok(NULL, "/");
			char jugador[40];
			strcpy(jugador,p);
			
			p= strtok(NULL, "/");
			char confirmacion[40];
			strcpy(confirmacion,p);
			
			if(strcmp(confirmacion, "true")==0)
			{
				printf("creador: %s | invitado: %s | respuesta: %s\n",creador_partida,jugador,confirmacion);
				GuardarConfirmacion(&lista_partida,jugador,1);
				printf("Se realizo la confirmacion\n");
				//Vemos la lista de invitados
				Dame_ListaInvitados(&lista_partida);
			}
			else if(strcmp(confirmacion, "false")==0)
			{
				printf("creador: %s | invitado: %s | respuesta: %s\n",creador_partida,jugador,confirmacion);
				GuardarConfirmacion(&lista_partida,jugador,0);
				printf("Se realizo la confirmacion\n");
				//Vemos la lista de invitados
				Dame_ListaInvitados(&lista_partida);
			}
			printf("Numero de invitados : %d\n",num_jugadores);
			printf("  Empieza la comprobacion\n");
			
			//if(num_jugadores > 1)
			//{
				printf("Comprobamos\n");
				int comprobar = comprobar_TodosAceptan(&lista_partida);
				if(comprobar == 0 )
				{
					printf(" --- Se juega la partida ---\n");
					enviarMensajeUsuarios(&lista_partida,"SI");
					
				}
				else if (comprobar == 2)
				{
					printf("Esperando respuesta de otros jugadores . . .\n");
				}
				else if (comprobar == 1)
				{
					printf (" No se juega la partida :( \n");
					enviarMensajeUsuarios(&lista_partida, "NO");
				}
			//}
			printf("-----Fin de la comprobacion----\n");
			
			select = 100;
			
		}
		if(select == 7)
		{
			printf("Entra a la seleccion 7\n");
			p = strtok(NULL, "/");
			char msj[20];
			strcpy(msj, p);
			printf("Mensaje de %s: %s\n",nombre,msj);
			enviarFrase(&lista_partida, msj,nombre);
			printf("Termina la seleccion 7\n");
			select = 100;
		}
		
		if(select == 0)
		{
			
			printf("Nombre: %s\n",nombre);
			Delete_user_conect(&lista, nombre, sock_conn);
			Update_Conectados(&lista);
			//write(sock_conn, conectado, sizeof(conectado));
			terminar = 1;
			printf("Usuario desconectado\n");
			
			select = 100;
		}
	}
	close (sock_conn);
}



int main(int argc, char *argv[])
{
	//MYSQL//
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) 
	{
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar la conexion, indicando nuestras claves de acceso
	// al servidor de bases de datos (user,pass)
	conn = mysql_real_connect (conn, "localhost","root", "mysql", NULL, 0, NULL, 0);
	if (conn==NULL)
	{
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//entramos en la base de datos "juego"
	err=mysql_query(conn, "use juego");
	if (err!=0)  //por si la base de datos no existe
	{
		printf("Error al seleccionar la base de datos\n");
		exit (1);
	}
	
	//SOCKETS//
	struct sockaddr_in serv_adr;
	
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// escucharemos en el port 9055 o aprecidos en caso de error de blind
	serv_adr.sin_port = htons(9100);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	
	int i = 0;
	

	pthread_t thread;
	
	for(;;)
	{
		
		printf ("Escuchando\n");
		//sock_conn es el socket que usaremos para este cliente
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("Conexion establecida\n");
		
		sockets[i] = sock_conn;
		pthread_create(&thread, NULL, AtenderCliente,&sockets[i]);
		i= i+1;
		
	} 
	
	return 0;
}



