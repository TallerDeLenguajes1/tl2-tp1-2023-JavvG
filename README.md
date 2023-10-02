# Respuestas del punto 2.a

## Pregunta 1

- La relación entre Pedido y Cliente es una composición, pues si se elimina la primera, la segunda también deja de existir.

- La relación entre Pedido y Cadete es una agregación, pues ambas clases tienen existencia independiente (si se elimina un objeto de la clase Pedido, Cadete sigue existiendo, al igual que si se elimina un objeto del tipo Cadete, Pedido puede ser reasignado).

- La relación entre Cadete y Cadetería es una composición, ya que si no existiese Cadetería, no habría objetos de la clase Cadete.

## Pregunta 2

- Métodos de la clase Cadetería: 
    crearPedido()
    asignarPedido() 
    reasignarPedido()
    cambiarEstadoPedido()
    generarInforme()
    cantidadCadetes()
    cantidadPedidos()

- Métodos de la clase Cadete: 
    jornalACobrar()
    cantidadPedidos()
    verDatosCadete()
    agregarPedido()
    eliminarPedido()


## Pregunta 3

- Teniendo en cuenta los principios de abstracción y ocultamiento, los atributos,propiedades y métodos podrían clasificarse de esta forma:

- Clase Cliente:

    - Nombre: público
    - Direccion: público
    - Telefono: público
    - DatosReferenciaDireccion: público

- Clase Pedido:

    - Nro: privado
    - Obs: público
    - Cliente: público
    - Estado: público
    verDireccionCLiente(): público
    verDatosCliente(): público

- Clase Cadete:

    - Id: privado
    - Nombre: público
    - Direccion: privado
    - Telefono: público
    - ListadoPedidos: privado
    jornalACobrar(): público

- Clase Cadetería:

    - Nombre: privado
    - Telefono: privado
    - ListadoCadetes: privado

## Pregunta 4

- Para construir los constructores de las clases, se procedería de esta forma:

    - Para la clase Cliente: se inicializan todos los atributos.
    - Para la clase Pedido: se inicializan los atributos (incluyendo al Cliente).
    - Para la clase Cadete: se inicializan los atributos, en los que ListadoPedidos es una lista vacía.
    - Para la clase Cadetería: se inicializan todos los atributos.
