using EspacioCadeteria;
using EspacioCadete;
using EspacioPedido;
using EspacioCliente;
using System.IO;

Cadeteria cadeteria = new Cadeteria();      // Nueva instancia de Cadeteria

// Carga de los datos iniciales de la cadetería

string input;

Console.Clear();

Console.Write("\n\n > Seleccione desde qué tipo de archivo desea cargar los datos: \n\n [1] - Archivo CSV \n [2] - Archivo JSON \n\n > Su respuesta: ");

input = Console.ReadLine();

int data = 0;

while(!int.TryParse(input, out data) || data < 1 || data > 2) {
    Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
    input = Console.ReadLine();
}

cadeteria = cadeteria.CargarDatos(data);

Console.ReadLine();

Console.Clear();

int numeroPedido = 0;

// Inicio de la interfaz de gestión

Console.WriteLine($" - Cadetería: {cadeteria.Nombre}");

Console.WriteLine( "\n\n - SISTEMA DE GESTIÓN DE PEDIDOS -");

bool again = true;
int option = 0;

while(again && option != 8) {

    Console.WriteLine("\n ¿Qué operación desea realizar?");

    Console.Write("\n\n [1] - Dar de alta un pedido \n [2] - Asignar un pedido a un cadete \n [3] - Cambiar estado de un pedido \n [4] - Reasignar un pedido a otro cadete \n [5] - Listar todos los cadetes \n [6] - Ver montos de cada pedido \n [7] - Generar informe \n [8] - Salir \n\n >> Su elección: " );

    input = Console.ReadLine();

    while(!int.TryParse(input, out option) || option < 1 || option > 8) {
        Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
        input = Console.ReadLine();
    }

    switch(option) {

        case 1: 
            Console.Clear();
            numeroPedido++;
            Console.WriteLine(cadeteria.CrearPedido(numeroPedido));
        break;

        case 2: 
            Console.Clear();
            Console.WriteLine(cadeteria.AsignarPedido());
        break;

        case 3:
            Console.Clear();
            Console.WriteLine(cadeteria.CambiarEstadoPedido());
        break;

        case 4:
            Console.Clear();
            Console.WriteLine(cadeteria.ReasignarPedido());
        break;

        case 5:
            Console.Clear();
            Console.WriteLine("\n\n - LISTADO DE TODOS LOS CADETES: ");
            foreach(Cadete C in cadeteria.ListadoCadetes) {
                C.VerDatosCadete();
            }
        break;

        case 6:
            Console.Clear();
            foreach(Pedido P in cadeteria.ListadoTotalPedidos) {
                Console.WriteLine($"\n PEDIDO NÚMERO: {P.Numero}");
                Console.WriteLine($" - Monto: ${P.Monto}");
            }
        break;

        case 7:
            Console.Clear();

            var pedidosPendientes = cadeteria.ListadoTotalPedidos.Where(pedido => pedido.Estado == EstadoPedido.Pendiente);
            var pedidosEntregados = cadeteria.ListadoTotalPedidos.Where(pedido => pedido.Estado == EstadoPedido.Entregado);
            var pedidosCancelados = cadeteria.ListadoTotalPedidos.Where(pedido => pedido.Estado == EstadoPedido.Cancelado);

            Console.WriteLine("\n------ INFORME ------\n\n");
            Console.WriteLine($" Pedidos pendientes: {pedidosPendientes.Count()} ");
            Console.WriteLine($" Pedidos entregados: {pedidosEntregados.Count()}"); 
            Console.WriteLine($" Pedidos cancelados: {pedidosCancelados.Count()}");
            Console.WriteLine($" Pedidos totales: {cadeteria.ListadoTotalPedidos.Count()}");
            Console.WriteLine(" Cantidad de pedidos asignados a cada cadete: ");

            foreach(Cadete C in cadeteria.ListadoCadetes) {
                Console.WriteLine($"\t - Cadete ID {C.IdCadete} ({C.Nombre}): {C.ListadoPedidos.Count()}");
            }

            Console.WriteLine(" Cantidad de pedidos entregados por cada cadete: ");

            foreach(Cadete C in cadeteria.ListadoCadetes) {
                var pedidosEntregadosCadete = C.ListadoPedidos.Where(pedido => pedido.Estado == EstadoPedido.Entregado);
                Console.WriteLine($"\t - Cadete ID {C.IdCadete} ({C.Nombre}): {pedidosEntregadosCadete.Count()}");
            }

            Console.WriteLine($" ------------------\n Recaudación total: ${cadeteria.RecaudacionTotal()}");
        break;
        
    }

    if(option != 8) {

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

        Console.Clear();

    }

}

Console.Clear();
Console.WriteLine("\n - Programa terminado (!) - ");