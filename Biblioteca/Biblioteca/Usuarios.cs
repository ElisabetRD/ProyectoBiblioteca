using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class Usuarios
    {

        int  id_usuario;
        string nombre, contraseña, conPassword;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public string ConPassword { get => conPassword; set => conPassword = value; }
        public int Id_usuario { get => id_usuario; set => id_usuario = value; }
    }
}
