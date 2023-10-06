using System.ComponentModel;
using System.IO.Enumeration;
using EspacioCadete;
using EspacioCadeteria;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace EspacioAccesoADatos;

public class AccesoADatos{
    private string dataPath = Directory.GetCurrentDirectory() + "/";
    private List<Cadete> listaCadetes;
    private Cadeteria cadeteria;

    public string DataPath { get => dataPath; set => dataPath = value; }
    public List<Cadete> ListaCadetes { get => listaCadetes; set => listaCadetes = value; }
    public Cadeteria Cadeteria { get => cadeteria; set => cadeteria = value; }

    public AccesoADatos() {
        this.ListaCadetes = new List<Cadete>();
        this.Cadeteria  = new Cadeteria();
    }

    public virtual void LeerDatosCadeteria(){
        // Empty
    }

    public virtual void LeerDatosCadetes() {
        // Empty
    }

}

public class AccesoCSV : AccesoADatos {     // La subclase hereda los atributos y métodos de la clase base AccesoADatos

    public AccesoCSV() : base() {     // Definición del constructor de la clase derivada, se indica que es el mismo que el de la clase base (!)
        //Empty
    }

    public override void LeerDatosCadeteria() {

        this.DataPath = DataPath + "cadeteria.csv";

        if(File.Exists(DataPath)) {     // Si el archivo existe, ejecutar lo siguiente

            using (var reader = new StreamReader(DataPath)) {

                while(!reader.EndOfStream) {        // Mientras no acabe la lectura del archivo

                    string line = reader.ReadLine();       // Se lee una línea de archivo

                    if(line != null) {      // Si la línea leída no está vacía, ejecutar lo siguiente

                        var splits = line.Split(',');       // Separa las línea leída en el caracter ','

                        Cadeteria.Nombre = splits[0].Trim();        // El primer split corresponde al nombre (Trim() remueve los espacios en blanco)
                        Cadeteria.Telefono = int.Parse(splits[1].Trim());       // El segundo split corresponde al teléfono, haciendo la conversión a entero


                    }
                }
            }

            Console.WriteLine("\n Datos de la cadeteria leídos correctamente");
        }
        else {
            Console.WriteLine("\n\n (!) No ha podido encontrarse el archivo de datos (cadeteria.csv)");
        }
    }

    public override void LeerDatosCadetes() {
        
        this.DataPath = DataPath + "cadetes.csv";

        if(File.Exists(DataPath)) {

            using (var reader = new StreamReader(DataPath)) {

                while(!reader.EndOfStream) {

                    string line = reader.ReadLine();

                    if(line != null) {

                        var splits = line.Split(',');

                        Cadete cadete = new Cadete();       // Nueva instancia para crear un cadete

                        cadete.IdCadete = int.Parse(splits[0].Trim());
                        cadete.Nombre = splits[1].Trim();
                        cadete.Direccion = splits[2].Trim();
                        cadete.Telefono = long.Parse(splits[3].Trim());

                       ListaCadetes.Add(cadete);        // Se añade el nuevo cadete registrado a la lista

                    }

                }

            }

            Console.WriteLine("\n Datos de los cadetes leídos correctamente");

        }
        else {
            Console.WriteLine("\n\n (!) No ha podido encontrarse el archivo de datos (cadetes.csv)");
        }

    }

}

public class AccesoJSON : AccesoADatos {

    public AccesoJSON() : base() {       // Constructor (!)
        // Empty
    }

    public override void LeerDatosCadeteria() {

        this.DataPath = DataPath + "cadeteria.json";
        
        if(File.Exists(DataPath)) {

            string JSON = File.ReadAllText(DataPath);

            string[]? array = JsonSerializer.Deserialize<string[]>(JSON);       // Se añaden los datos leídos del archivo JSON a un arreglo

            Cadeteria.Nombre = array[0];
            Cadeteria.Telefono = int.Parse(array[1]);
        }
        else {
            Console.WriteLine("\n\n (!) No ha podido encontrarse el archivo de datos (cadeteria.json)");
        }
        
    }

    public override void LeerDatosCadetes() {
        
        this.DataPath = DataPath + "cadetes.json";

        if(File.Exists(DataPath)) {

            string JSON = File.ReadAllText(DataPath);

            string[]? array = JsonSerializer.Deserialize<string[]>(JSON);

            Cadete cadete = new Cadete();

            cadete.IdCadete = int.Parse(array[0]);
            cadete.Nombre = array[1];
            cadete.Direccion = array[2];
            cadete.Telefono = int.Parse(array[3]); 
        }
        else {
            Console.WriteLine("\n\n (!) No ha podido encontrarse el archivo de datos (cadetes.json)");
        }

    }


}
