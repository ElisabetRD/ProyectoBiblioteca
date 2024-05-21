using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class Control
    {
        public string ctrlRegistro(Usuarios nombre)
        {
            Modelo modelo = new Modelo();
            string respuesta = "";

            if (string.IsNullOrEmpty(nombre.Nombre) || string.IsNullOrEmpty(nombre.Contraseña) || string.IsNullOrEmpty(nombre.ConPassword))
            {
                respuesta = "Debe llenar todos los campos";
            }
            else
            {
                if (nombre.Contraseña == nombre.ConPassword)
                {
                    if (modelo.existeUsuario(nombre.Nombre))
                    {
                        respuesta = "El usuario ya existe";
                    }
                    else
                    {
                        nombre.Contraseña = generarSHA1(nombre.Contraseña);
                        modelo.registro(nombre);
                    }
                }
                else
                {
                    respuesta = "Las contraseña no coinciden";
                }
            }
            return respuesta;

        }

        public string ctrlLogin(string nombre, string contraseña)
        {
            Modelo modelo = new Modelo();
            string respuesta = "";
            Usuarios datosUsuario = null;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(contraseña))
            {
                respuesta = "Debe llenar todos los campos";
            }
            else
            {
                datosUsuario = modelo.porUsuario(nombre);

                if (datosUsuario == null)
                {
                    respuesta = "El usuario no existe";
                }
                else
                {
                    if (datosUsuario.Contraseña != generarSHA1(contraseña))
                    {
                        respuesta = "El usuario y/o contraseña no coinciden";
                    }
                    else
                    {
                        Session.id_usuario = datosUsuario.Id_usuario;
                        Session.nombre = datosUsuario.Nombre;
                        Session.contraseña = datosUsuario.Contraseña;
                    }
                }
            }
            return respuesta;
        }

        private string generarSHA1(string cadena)
        {
            UTF8Encoding enc = new UTF8Encoding();
            byte[] data = enc.GetBytes(cadena);
            byte[] result;

            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();

            result = sha.ComputeHash(data);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {

                if (result[i] < 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }

            return sb.ToString();
        }
    }
}
