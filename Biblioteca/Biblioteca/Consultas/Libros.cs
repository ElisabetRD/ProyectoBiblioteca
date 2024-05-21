using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Consultas
{
     class Libros
    {
        public int id_registro { get; set; }
        public DateTime fecha { get; set; }
        public string titulo_libro{ get; set; }
        public string autor { get; set; }
        public string clasificacion { get; set; }
        public string folio { get; set; }
        public string nombre_alumno{ get; set; }
         public string carrera{ get; set; }
        public int numero_control { get; set; }
    }
}
