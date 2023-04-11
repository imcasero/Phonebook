using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    internal class Contacto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public static void AgregarContacto(Contacto nuevoContacto, List<Contacto> listaContactos)
        {
            listaContactos.Add(nuevoContacto);
            Console.WriteLine("Contacto añadido correctamente");
        }
        public static void EliminarContacto(int id, List<Contacto> listaContactos)
        {
            bool borrado = false;
            for (int i = 0; i < listaContactos.Count; i++)
            {
                if (listaContactos[i].Id == id)
                {
                    listaContactos.RemoveAt(i);
                    borrado = true;
                    break; // Salir del bucle una vez se ha eliminado el contacto
                }
            }

            if (!borrado)
            {
                Console.WriteLine("No se ha encontrado el id seleccionado");
            }
            else
            {
                Console.WriteLine("Contacto eliminado correctamente");
            }
        }
        public static Contacto BuscarContacto(int id, List<Contacto> listaContactos)
        {
            Contacto contacto = null;
            foreach (Contacto c in listaContactos)
            {
                if (c.Id == id)
                {
                    contacto = c;
                }
            }
            return contacto;

        }
        public static void EditarContacto( Contacto contacto)
        {
            Console.WriteLine("Inserte los datos que quiera modificar, en caso de no querer modificar ese campo escriba lo mismo a la anterior instancia");
            Console.WriteLine("Inserte el nombre");
            contacto.Nombre = Console.ReadLine();
            Console.WriteLine("Inserte el Apellido");
            contacto.Apellido = Console.ReadLine();
            Console.WriteLine("Inserte la direccion");
            contacto.Direccion = Console.ReadLine();
            Console.WriteLine("Inserte el correo");
            contacto.Correo = Console.ReadLine();
            Console.WriteLine("Inserte el numero de telefono");
            contacto.Telefono = Console.ReadLine();

        }
    }

    

}
