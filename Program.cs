using EspacioCadeteria;
using EspacioCadete;
using EspacioPedido;
using EspacioCliente;
using System.IO;

Cadeteria cadeteria = new Cadeteria();      // Nueva instancia de Cadeteria

// Carga de los datos iniciales de la cadetería

string input;

Console.Write("\n\n > Seleccione desde qué tipo de archivo desea cargar los datos: \n\n [1] - Archivo CSV \n [2] - Archivo JSON \n\n > Su respuesta: ");

input = Console.ReadLine();

int data = 0;

while(!int.TryParse(input, out data) || data < 1 || data > 2) {
    Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
    input = Console.ReadLine();
}

cadeteria.CargarDatos(data);

Console.ReadLine();

int numeroPedido = 0;

// Inicio de la interfaz de gestión

Console.WriteLine( "\n\n - SISTEMA DE GESTIÓN DE PEDIDOS -");

bool again = true;
int option = 0;

while(again && option != 7) {

    Console.Clear();

    Console.WriteLine("\n ¿Qué operación desea realizar?");

    Console.Write("\n\n [1] - Dar de alta un pedido \n [2] - Asignar un pedido a un cadete \n [3] - Cambiar estado de un pedido \n [4] - Reasignar un pedido a otro cadete \n [5] - Listar todos los cadetes \n [6] - Generar informe \n [7] - Salir \n\n >> Su elección: " );

    input = Console.ReadLine();

    while(!int.TryParse(input, out option) || option < 1 || option > 7) {
        Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
        input = Console.ReadLine();
    }

    switch(option) {

        case 1: 
            Console.Clear();
            numeroPedido++;
            cadeteria.CrearPedido(numeroPedido);
        break;

        case 2: 
            Console.Clear();
            cadeteria.AsignarPedido();
        break;

        case 3:
            Console.Clear();
            cadeteria.CambiarEstadoPedido();
        break;

        case 4:
            Console.Clear();
            cadeteria.ReasignarPedido();
        break;

        case 5:
            Console.Clear();
            cadeteria.ListarTodosLosCadetes();
        break;

        case 6:
            Console.Clear();
            cadeteria.GenerarInforme();
        break;
        
    }

    if(option != 7) {

        Console.ReadLine();
        Console.Clear();

        Console.Write("\n ¿Desea realizar otra operación? \n\n [1] - Si \n [2] - No \n\n >> Su respuesta: ");
        input = Console.ReadLine();

        int aux = 0;

        while(!int.TryParse(input, out aux) || aux < 1 || aux > 2) {
            Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
            input = Console.ReadLine();
        }

        again = true ? aux == 1 : false;

    }

}

Console.Clear();
Console.WriteLine("\n - Programa terminado (!) - ");