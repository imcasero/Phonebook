using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CsvHelper;
using System.IO;
using System.Diagnostics.Contracts;
//Crear una aplicación de gestión de contactos que permita almacenar, 
//buscar, editar y eliminar información de contacto como nombre, apellido, 
//dirección, correo electrónico y número de teléfono. La aplicación debe 
//permitir la creación de múltiples agendas y la posibilidad de guardar y 
//cargar la información de contacto en un archivo.
namespace Agenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ruta = @".\Agenda\datos\contactos.csv";
            bool respuesta = Comprobar(ruta);
            List<Contacto> listaContactos = LeerContactosDesdeCSV(ruta);
            if (respuesta)
            {
                bool continuar = true;

                while (continuar)
                {
                    Console.WriteLine("Bienvenido a la Agenda");
                    Console.WriteLine("Opciones:");
                    Console.WriteLine("1. Ver contactos");
                    Console.WriteLine("2. Añadir contacto");
                    Console.WriteLine("3. Editar contacto");
                    Console.WriteLine("4. Eliminar contacto");
                    Console.WriteLine("5. Buscar contacto");
                    Console.WriteLine("6. Salir");
                    Console.Write("Seleccione una opción: ");
                    int opcion = int.Parse(Console.ReadLine());

                    switch (opcion)
                    {
                        case 1:
                            foreach (Contacto c in listaContactos)
                            {
                                Console.WriteLine($"ID: {c.Id} , Nombre: {c.Nombre} , Apellido: {c.Apellido}");
                            }
                            break;
                        case 2:
                            Contacto nuevo = CrearContacto(listaContactos);
                            Contacto.AgregarContacto(nuevo, listaContactos);
                            EscribirContactosEnCSV(listaContactos , ruta);
                            break;
                        case 3:
                            Console.WriteLine("Introduzca el id del contacto que desea editar");
                            int idEdit = int.Parse(Console.ReadLine());
                            Contacto contactoCambio = Contacto.BuscarContacto(idEdit, listaContactos);
                            if (contactoCambio == null)
                            {
                                Console.WriteLine("No se encontro el contacto");
                            }
                            else
                            {
                                Contacto.EditarContacto(contactoCambio);
                                EscribirContactosEnCSV(listaContactos, ruta);
                            }
                            break;
                        case 4:
                            Console.WriteLine("Introduzca el id que desea eliminar :");
                            int idDelete = int.Parse(Console.ReadLine());
                            Contacto.EliminarContacto(idDelete, listaContactos);
                            EscribirContactosEnCSV(listaContactos, ruta);
                            break;
                        case 5:
                            Console.WriteLine("Introduzca el id del contacto");
                            int idSearch = int.Parse(Console.ReadLine());
                            Contacto resul = Contacto.BuscarContacto(idSearch, listaContactos);
                            if (resul == null)
                            {
                                Console.WriteLine("No se encontro el contacto");
                            }else
                            {
                                Console.WriteLine("Id: " + resul.Id);
                                Console.WriteLine("Nombre: " + resul.Nombre);
                                Console.WriteLine("Apellido: " + resul.Apellido);
                                Console.WriteLine("Direccion: " + resul.Direccion);
                                Console.WriteLine("Correo: " + resul.Correo);
                                Console.WriteLine("Telefono: " + resul.Telefono);
                            }
                            break;
                        case 6:
                            continuar = false;
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Por favor, seleccione una opción del menú.");
                            break;
                    }
                }
            }
            else
            { 
                Console.WriteLine("No existen contactos, empeice a añadir contactos");
            }
        }
        public static void EscribirContactosEnCSV(List<Contacto> contactos, string ruta)
        {
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                foreach (Contacto c in contactos)
                {
                    string linea = $"{c.Id},{c.Nombre},{c.Apellido},{c.Direccion},{c.Correo},{c.Telefono}";
                    sw.WriteLine(linea);
                }
            }
        }
        public static List<Contacto> LeerContactosDesdeCSV(string rutaArchivo)
        {
            List<Contacto> contactos = new List<Contacto>();

            using (var reader = new StreamReader(rutaArchivo))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Contacto contacto = new Contacto();
                    contacto.Id = int.Parse(values[0]);
                    contacto.Nombre = values[1];
                    contacto.Apellido = values[2];
                    contacto.Direccion = values[3];
                    contacto.Correo = values[4];
                    contacto.Telefono = values[5];

                    contactos.Add(contacto);
                }
            }

            return contactos;
        }

        public static Contacto CrearContacto(List<Contacto> listaContactos) 
        {
            int nuevoId = 1;
            if (listaContactos.Count > 0)
            {
                nuevoId = listaContactos.Last().Id + 1;
            }
            Console.WriteLine("Introduzca el nombre");
            string nombre = Console.ReadLine();
            Console.WriteLine("Introduzca el apellido");
            string apellido = Console.ReadLine();
            Console.WriteLine("Introduzca la direccion");
            string direccion = Console.ReadLine();
            Console.WriteLine("Introduzca el correo");
            string correo = Console.ReadLine();
            Console.WriteLine("Introduzca el numero de telefono");
            string telefono = Console.ReadLine();
            Contacto contacto = new Contacto() 
            { 
                Id = nuevoId,
                Nombre = nombre,
                Apellido = apellido,
                Direccion = direccion,
                Correo = correo,
                Telefono = telefono,
            };
            return contacto;
        }
        public static bool Comprobar(string path)
        { 
            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
}
