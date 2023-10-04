using EspacioCadete;
using EspacioPedido;
using EspacioCliente;
using EspacioAccesoADatos;

namespace EspacioCadeteria {

    public class Cadeteria {

        private string nombre;
        private int telefono;
        private List<Cadete> listadoCadetes;
        private List<Pedido> listadoTotalPedidos;
        private List<Pedido> listaPedidosPendientes;
        private List<Pedido> listaPedidosEntregados;
        private List<Pedido> listaPedidosCancelados;


        public string Nombre { get => nombre; set => nombre = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
        public List<Pedido> ListadoTotalPedidos { get => listadoTotalPedidos; set => listadoTotalPedidos = value; }
        public List<Pedido> ListaPedidosPendientes { get => listaPedidosPendientes; set => listaPedidosPendientes = value; }
        public List<Pedido> ListaPedidosEntregados { get => listaPedidosEntregados; set => listaPedidosEntregados = value; }
        public List<Pedido> ListaPedidosCancelados { get => listaPedidosCancelados; set => listaPedidosCancelados = value; }

        // Métodos

        public Cadeteria() {    // Constructor por defecto, inicializa una lista de cadetes para evitar errores a posteriori
            this.ListadoCadetes = new List<Cadete>();
            this.ListadoTotalPedidos = new List<Pedido>();
            this.ListaPedidosEntregados = new List<Pedido>();
            this.ListaPedidosPendientes = new List<Pedido>();
            this.ListaPedidosCancelados = new List<Pedido>();
        }

        /* public Cadeteria(string nombre, int telefono) {
            this.Nombre = nombre;
            this.Telefono = telefono;
        } */

        public void CargarDatos(int option) {

            switch(option) {

                case 1:
                    
                    AccesoCSV csv = new AccesoCSV();

                    Cadeteria aux = csv.LeerDatosCadeteria("cadeteria.csv");

                    Nombre = aux.Nombre;
                    Telefono = aux.Telefono;
                
                    listadoCadetes = csv.LeerDatosCadetes("cadetes.csv");

                break;

                case 2:

                    AccesoJSON json = new AccesoJSON();

                    aux = json.LeerDatosCadeteria("cadeteria.json");

                    Nombre = aux.Nombre;
                    Telefono = aux.Telefono;

                    ListadoCadetes = json.LeerDatosCadetes("cadetes.json");

                break;

            }

        }

        public void CrearPedido(int numeroPedido) {

            Pedido nuevoPedido = new Pedido();      // Nueva instancia para el pedido

            Console.WriteLine("\n - NUEVO PEDIDO -");

            Console.Write("\n - Descripción del pedido: ");
            var observacion = Console.ReadLine();

            while(string.IsNullOrEmpty(observacion)) {
                Console.Write("\n - (!) Ingrese una descripción válida: ");
                observacion = Console.ReadLine();
            }

            Console.Write("\n - Nombre del cliente: ");
            var nombreCliente = Console.ReadLine();

            while(string.IsNullOrEmpty(nombreCliente)) {
                Console.Write("\n (!) Ingrese un nombre válido: ");
                nombreCliente = Console.ReadLine();
            }

            Console.Write("\n - Dirección del cliente: ");
            var direccionCliente = Console.ReadLine();

            while(direccionCliente == null) {
                Console.Write("\n (!) Ingrese una dirección válida: ");
                direccionCliente = Console.ReadLine();
            }

            Console.Write("\n - Número de teléfono del cliente: ");
            var input = Console.ReadLine();
            long telefonoCliente;

            while(!long.TryParse(input, out telefonoCliente)) {
                Console.Write("\n (!) Ingrese un número válido: ");
                input = Console.ReadLine();
            }

            Console.Write("\n - Monto del pedido: $");
            input = Console.ReadLine();
            double montoPedido;

            while(!double.TryParse(input, out montoPedido)) {
                Console.Write("\n (!) Ingrese un valor válido: ");
                input = Console.ReadLine();
            }

            nuevoPedido.Numero = numeroPedido;
            nuevoPedido.Observaciones = observacion;
            nuevoPedido.Monto = montoPedido;
            
            Cliente nuevoCliente = new Cliente();       // Nueva intancia para cliente

            nuevoCliente.Nombre = nombreCliente;
            nuevoCliente.Direccion = direccionCliente;
            nuevoCliente.Telefono = telefonoCliente;

            nuevoPedido.Cliente = nuevoCliente;

            this.listadoTotalPedidos.Add(nuevoPedido);
            this.ListaPedidosPendientes.Add(nuevoPedido);

            Console.WriteLine("\n\n >> El pedido ha sido creado exitosamente. \n");
        }

        public void AsignarPedido() {

            Console.WriteLine("\n - ASIGNACIÓN DE PEDIDOS - ");
            
            if(this.ListadoTotalPedidos.Count() > 0) {     // Si existen pedidos enlistados, se procede con lo siguiente

                // Se muestran todos los pedidos registrados. Se habilita la selección de un pedido para asignar a un cadete

                Console.WriteLine("\n - LISTADO DE TODOS LOS PEDIDOS REGISTRADOS: ");

                this.ListarTodosLosPedidos();

                Console.WriteLine("\n\n - ELECCIÓN DEL PEDIDO: ");

                Pedido pedidoSeleccionado = this.SeleccionarPedidoPorNumero();

                Console.Clear();

                Console.WriteLine("\n\n >> Pedido seleccionado para asignar: ");
                pedidoSeleccionado.VerDatosPedido();

                Console.ReadLine();
                Console.Clear();

                // Se muestran los cadetes. Se habilita la selección de un cadete para asignar el pedido

                Console.WriteLine("\n\n - LISTADO DE TODOS LOS CADETES: ");
                this.ListarTodosLosCadetes();

                Console.WriteLine("\n\n - ELECCIÓN DEL CADETE: ");

                Cadete cadeteSeleccionado = this.SeleccionarCadetePorID();

                Console.Clear();

                // Una vez seleccionado el pedido y el cadete, se procede con la asignación

                pedidoSeleccionado.Asignado = true;
                cadeteSeleccionado.ListadoPedidos.Add(pedidoSeleccionado);      // Se añade el pedido elegido a la lista de pedidos del cadete elegido

                Console.WriteLine("\n >> Asignación completada: ");
                cadeteSeleccionado.VerDatosCadete();

            }
            else {
                Console.WriteLine("\n (!) No hay pedidos registrados para ser asignados");
            }

        }

        public void ListarTodosLosPedidos() {
            foreach(Pedido P in this.ListadoTotalPedidos) {
                P.VerDatosPedido();
            }
        }

        public void ListarTodosLosCadetes() {
            foreach(Cadete C in this.ListadoCadetes) {
                C.VerDatosCadete();
            }
        }

        public void CambiarEstadoPedido() {

            Console.WriteLine("\n - MODIFICACIÓN DEL ESTADO DE PEDIDOS - ");

            if(this.ListadoTotalPedidos.Count() > 0) {

                Console.WriteLine("\n\n - LISTADO DE TODOS LOS PEDIDOS REGISTRADOS: ");
                this.ListarTodosLosPedidos();

                Console.WriteLine("\n - ELECCIÓN DEL PEDIDO A MODIFICAR:  ");

                Pedido pedidoAModificar = this.SeleccionarPedidoPorNumero();

                Console.Write("\n ¿Cuál es el nuevo estado del pedido? \n [1] - Pendiente  \n [2] - En preparación \n [3] - Asignado a un cadete \n [4] - En camino \n [5] - Entregado \n [6] - Cancelado \n\n > Su respuesta: ");

                string input = Console.ReadLine();
                int option;

                while(!int.TryParse(input, out option) || option < 1 || option > 6) {
                    Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
                    input = Console.ReadLine();
                }

                this.ListaPedidosEntregados.RemoveAll(pedido => pedido == pedidoAModificar);
                this.ListaPedidosPendientes.RemoveAll(pedido => pedido == pedidoAModificar);
                this.ListaPedidosCancelados.RemoveAll(pedido => pedido == pedidoAModificar);


                switch(option) {

                    case 1:
                        pedidoAModificar.Estado = EstadoPedido.Pendiente;
                        this.ListaPedidosPendientes.Add(pedidoAModificar);
                    break;

                    case 2:
                        pedidoAModificar.Estado = EstadoPedido.EnPreparacion;
                    break;

                    case 3:
                        pedidoAModificar.Estado = EstadoPedido.AsignadoACadete;
                    break;

                    case 4:
                        pedidoAModificar.Estado = EstadoPedido.EnCamino;
                    break;
                    
                    case 5:
                        pedidoAModificar.Estado = EstadoPedido.Entregado;
                        this.ListaPedidosEntregados.Add(pedidoAModificar);
                    break;

                    case 6:
                        pedidoAModificar.Estado = EstadoPedido.Cancelado;
                        this.ListaPedidosCancelados.Add(pedidoAModificar);
                    break;

                }

                Console.Clear();
                Console.WriteLine("\n >> El estado del pedido se ha modificado exitosamente");

                pedidoAModificar.VerDatosPedido();

            }  
            else {
                Console.WriteLine("\n (!) No hay pedidos registrados para ser asignados");
            }

        }

        public Pedido SeleccionarPedidoPorNumero() {

            string input;
            int numeroPedidoBuscado;

            Console.Write("\n > Ingrese el número del pedido que desea seleccionar: ");

            input = Console.ReadLine();

            while(!int.TryParse(input, out numeroPedidoBuscado)) {
                Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
                input = Console.ReadLine();
            }

            Pedido pedidoSeleccionado = this.ListadoTotalPedidos.Find(pedido => pedido.Numero == numeroPedidoBuscado);

            while(pedidoSeleccionado == null) {

                Console.Write($"\n\n (!) No se ha encontrado el pedido número {numeroPedidoBuscado}.\n > Ingrese un nuevo valor: ");
                
                input = Console.ReadLine();

                while(!int.TryParse(input, out numeroPedidoBuscado)) {
                    Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
                    input = Console.ReadLine();
                }

                pedidoSeleccionado = this.ListadoTotalPedidos.Find(pedido => pedido.Numero == numeroPedidoBuscado);
            }

            return pedidoSeleccionado;

        }

        public Cadete SeleccionarCadetePorID() {

            int idCadeteBuscado;

            Console.Write("\n > Ingrese el ID del cadete que desea seleccionar: ");

            string input = Console.ReadLine();

            while(!int.TryParse(input, out idCadeteBuscado)) {
                Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
                input = Console.ReadLine();
            }

            Cadete cadeteSeleccionado = this.ListadoCadetes.Find(cadete => cadete.IdCadete == idCadeteBuscado);

            while(cadeteSeleccionado == null) {

                Console.Write($"\n\n (!) No se ha encontrado el cadete con ID {idCadeteBuscado}.\n > Ingrese un nuevo valor: ");
                
                input = Console.ReadLine();

                while(!int.TryParse(input, out idCadeteBuscado)) {
                    Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
                    input = Console.ReadLine();
                }

                cadeteSeleccionado = this.ListadoCadetes.Find(cadete => cadete.IdCadete == idCadeteBuscado);

            }

            return cadeteSeleccionado;
        }

        public void ReasignarPedido() {

            Console.WriteLine("\n - REASIGNACIÓN DE PEDIDO - ");

            if(this.ListadoTotalPedidos.Count() > 0) {

                Console.WriteLine("\n - ELECCIÓN DEL PEDIDO A REASIGNAR:  ");        // Se muestran los pedidos que no tienen un cadete asignado

                foreach(Pedido P in this.ListadoTotalPedidos) {
                    if(P.Asignado == true) {
                        P.VerDatosPedido();
                    }
                }

                Pedido pedidoAReasignar = this.SeleccionarPedidoAsignado();     // Se elige el pedido a reasignar de una lista de pedidos con asignacion de cadete afirmativa

                Console.Clear();

                foreach(Cadete C in this.ListadoCadetes) {

                    Pedido pedidoAEliminar = C.ListadoPedidos.Find(pedido => pedido == pedidoAReasignar);

                    if(pedidoAEliminar != null) {       // Si el pedido a reasignar se encuentra en alguna de las listas de pedidos de algun cadete, se elimina
                        C.EliminarPedido(pedidoAEliminar);
                    }

                }

                Console.WriteLine("\n - ELECCIÓN DEL CADETE AL QUE DESEA REASIGNAR EL PEDIDO: ");

                this.ListarTodosLosCadetes();

                Cadete cadeteSeleccionado = this.SeleccionarCadetePorID();

                cadeteSeleccionado.ListadoPedidos.Add(pedidoAReasignar);

                Console.Clear();

                Console.WriteLine("\n >> Pedido reasignado exitosamente");

                cadeteSeleccionado.VerDatosCadete();

            }
            else {
                Console.WriteLine("\n (!) No hay pedidos registrados para ser reasignados");
            }
            
        }

        public Pedido SeleccionarPedidoAsignado() {

            string input;
            int numeroPedidoBuscado;

            Console.Write("\n > Ingrese el número del pedido que desea seleccionar: ");

            input = Console.ReadLine();

            while(!int.TryParse(input, out numeroPedidoBuscado)) {
                Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
                input = Console.ReadLine();
            }

            Pedido pedidoSeleccionado = this.ListadoTotalPedidos.Find(pedido => pedido.Numero == numeroPedidoBuscado && pedido.Asignado == true);

            while(pedidoSeleccionado == null) {

                Console.Write($"\n\n (!) No se ha encontrado el pedido número {numeroPedidoBuscado}.\n > Ingrese un nuevo valor: ");
                
                input = Console.ReadLine();

                while(!int.TryParse(input, out numeroPedidoBuscado)) {
                    Console.Write("\n\n (!) Ha ingresado una opción inválida.\n > Ingrese nuevamente: ");
                    input = Console.ReadLine();
                }

                pedidoSeleccionado = this.ListadoTotalPedidos.Find(pedido => pedido.Numero == numeroPedidoBuscado);
            }

            return pedidoSeleccionado;

        }

        public int CantidadDeCadetes() {
            return this.ListadoCadetes.Count();
        }

        public int CantidadDePedidos() {
            return this.ListadoTotalPedidos.Count();
        }

        public double RecaudacionTotal() {

            double recaudacion = 0;

            foreach(Pedido P in this.ListadoTotalPedidos) {
                recaudacion += P.Monto;
            }

            return recaudacion;
        }

        public void VerMontosCadaPedido() {
            foreach(Pedido P in this.ListadoTotalPedidos) {
                Console.WriteLine($"\n PEDIDO NÚMERO: {P.Numero}");
                Console.WriteLine($" - Monto: ${P.Monto}");
            }
        }

        

        public void GenerarInforme() {

            //var pedidosEntregados = from pedido in this.ListadoTotalPedidos where pedido.Estado == EstadoPedido.Entregado select pedido;    // Sentencia linq (old)

            var pedidosEntregados1 = listadoTotalPedidos.Where(L => L.Estado == EstadoPedido.Entregado); //New

            Console.WriteLine("\n------ INFORME ------\n\n");
            Console.WriteLine($" Pedidos pendientes: {this.ListaPedidosPendientes.Count()} ");
            Console.WriteLine($" Pedidos entregados: {pedidosEntregados1.Count()}"); 
            Console.WriteLine($" Pedidos totales: {this.ListadoTotalPedidos.Count()}");
            Console.WriteLine($" ------------------\n Recaudación total: ${this.RecaudacionTotal()}");

        }

    }
    
}