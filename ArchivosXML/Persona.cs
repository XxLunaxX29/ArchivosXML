using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchivosXML
{
    public class Persona
    {
        private string nombre;
        private int celular;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public int Celular
        {
            get { return celular; }
            set { celular = value; }
        }
    }
}
