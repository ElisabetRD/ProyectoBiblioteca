using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    internal class Modelo
    {

        public int registro(Usuarios nombre)
        {
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();

            string sql = "INSERT INTO registrar_usuario (nombre, contraseña) VALUES(@nombre, @contraseña)";
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nombre", nombre.Nombre);
            comando.Parameters.AddWithValue("@contraseña", nombre.Contraseña);

            int resultado = comando.ExecuteNonQuery();

            return resultado;
        }

        public bool existeUsuario(string nombre)
        {
            MySqlDataReader reader;
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();

            string sql = "SELECT id_usuario FROM registrar_usuario WHERE nombre LIKE @nombre";
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nombre", nombre);

            reader = comando.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Usuarios porUsuario(string nombre)
        {
            MySqlDataReader reader;
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();

            string sql = "SELECT id_usuario, contraseña, nombre FROM registrar_usuario WHERE nombre LIKE @nombre";
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nombre", nombre);

            reader = comando.ExecuteReader();

            Usuarios usr = null;

            while (reader.Read())
            {
                usr = new Usuarios();
                usr.Id_usuario = int.Parse(reader["id_usuario"].ToString());
                usr.Contraseña = reader["contraseña"].ToString();
                usr.Nombre = reader["nombre"].ToString();
            }
            return usr;
        }
    }
}
