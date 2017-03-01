using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;           // Paso 1
using System.Net.Sockets;   // Paso 1

namespace ServidorConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            // Paso 2: crear un socket
            Socket socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Paso 3: definir la dirección y puerto
            IPEndPoint direccion1 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

            // Cadena para recibir datos del cliente
            byte[] byARecibir = new byte[255];
            try
            {
                // Paso 4: asociar el socket creado a la dirección con Bind
                socket1.Bind(direccion1);
                // Paso 5: se pone el socket a escuchar con Listen
                socket1.Listen(1); // Solo se acepta una conexión
                Console.WriteLine("Servidor escuchando...");
                // Páso 6: se queda esperando una conexión con Accept (y devolverá un socket cuando llegue)
                Socket socket2 = socket1.Accept();

                Console.WriteLine("Servidor conectado con éxito");

                int longitud = socket2.Receive(byARecibir);
                Array.Resize(ref byARecibir, longitud);
                Console.WriteLine(Encoding.Default.GetString(byARecibir));

                //Vamos a probar a enviar algo
                byte[] txtAEnviar;
                txtAEnviar = Encoding.Default.GetBytes("Mensaje desde el servidor");
                socket2.Send(txtAEnviar);
                Console.WriteLine("Texto de prueba enviado al cliente");

                // Paso 7: cerrar el socket
                socket1.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString());
            }
            Console.WriteLine("Fin del programa. Presione cualquier tecla...");
            Console.ReadLine();
        }
    }
}
