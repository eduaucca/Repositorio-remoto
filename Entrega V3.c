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
typedef struct
{
	char usuario[50];
	int socket;
}TUsuario;

typedef struct
{
	TUsuario usuarios[100];
	int num;
}TConectados;
TConectados lista;

//MYSQL//
int err;
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
int Log_In (char nombre[20], char password[20])
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
		printf("Correctly Logged\n");
		write(sock_conn,"1/0", sizeof("1/0"));
		return 0;
	}
	if (comparador != 0) //Password incorrecta
	{
		printf("Password error\n");
		write(sock_conn,"1/1", sizeof("1/1"));
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
	sprintf (conectados, "4/%d/", l-> num);
	
	for (i; i < l->num; i++)
	{
		strcat(conectados, l-> usuarios[i].usuario);
		strcat(conectados, "/");
	}
	printf ("comprobacion del proceso\n");
	for ( i = 0; i < l->num; i++)
	{
		write(l->usuarios[i].socket, conectados, sizeof(conectados));
		printf("%d\n", l->num);
	}
	    printf("%s\n", conectados);
}


int Add_User (TConectados *l, int socket, char usuario[50]) //1 -> A\ufff1adido Correctamente || 0 -> Lista llena
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
void *AtenderCliente(void *socket)
{
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;
	
	int select; //selecciona que esta haciendo el usuario
	char password[20];
	char nombre[20];
	char conectado[40];
	int cuestion;
	char respuesta[512];
	int terminar = 0;
	
	while(terminar == 0)
	{	
		
		ret=read(sock_conn,buff, sizeof(buff));
		buff[ret] = '\0';
		
		char *p = strtok(buff, "/");
		
		select = atoi(p);
		
		if(select == 1) //Log In
		{
			
			int logueado = -2; //0 -> correcta || 1 -> contrase?a incorrecta
			char usuario; //
			
			p = strtok (NULL, "/");
			strcpy(nombre, p); //almacenamos el nombre escrito por el ususario
			p = strtok (NULL, "/");
			strcpy (password, p); //almacenamos la password dada por el usuario
			
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora
			// Ahora recibimos sus credenciales, que dejamos en buff
			logueado = Log_In (nombre, password);
		    
			if (logueado == 0) // se loguea correctamente
			{
				//entonces se a?ade el usuario 
				
				int add = Add_User(&lista, sock_conn, nombre);
				Update_Conectados(&lista);
				
				if (add == 0) 
				{
					printf ("Se ha a?adido al usuario correctamente\n");
					// se a?ade correctamente
				}
				else if (add == -1)
				{
					printf ("No se ha podido a?adir al usuario\n"); // la lista esta llena, no se puede a?adir
				}
				
				
			}
			
			if(logueado == 1)
			{
				printf("contrase?a incorrecta");
				
			}
			else if(logueado == -1)
			{
				printf("el nombre no se encuentra en la DB"); // el nombre no esta en la DB
			}
			
			select = 100;
			pthread_mutex_unlock( &mutex); // ahora puede interrumpirme
			
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
		
		if (select == 3) //Dar el nombre del jugador mayor de edad
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
			    select = 100;
			}
			if(cuestion == 2) // Da la id del jugador con mayor puntaje
			{
				char identificador[40];
				char respuesta[100] = "3/";
				printf("Este es la segunda pregunta\n");
				DameID_MayorPuntaje(respuesta);
				strcat(respuesta,identificador);
				write(sock_conn, respuesta, sizeof(respuesta));
				select = 100;
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
				select = 100;
			}
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
void DameConectados (TConectados *lista, char conectados[300])
{ 
	
	
	sprintf (conectados, "%d", lista->num);
	
	int i;
	for (i=0; i< lista->num; i++)
	{
		sprintf (conectados, "%s/%s", conectados, lista->usuarios[i].usuario);
	}
}



int main(int argc, char *argv[])
{
	//MYSQL//
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
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
	err=mysql_query (conn, "use juego");
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
	serv_adr.sin_port = htons(9570);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	
	int i = 0;
	//int sockets[100];
	TConectados lista;
	pthread_t thread[100];
	
	for(;;)
	{
		
		printf ("Escuchando\n");
		//sock_conn es el socket que usaremos para este cliente
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("Conexion establecida\n");
		
		lista.usuarios[i].socket = sock_conn;
		pthread_create (&thread, NULL, AtenderCliente, &lista.usuarios[i].socket);
		i= i+1;
		
	} 
	
	return 0;
}

