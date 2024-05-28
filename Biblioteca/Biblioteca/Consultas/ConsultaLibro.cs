using Biblioteca.BD;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca.Consultas
{

    internal class ConsultaLibro
    {
        private ConexionMySql mConexion;
        private List<Libros> mLibros;

        public ConsultaLibro()
        {
            mConexion = new ConexionMySql();
            mLibros = new List<Libros>();
        }

        public bool agregarLibro(Libros mlibro)
        {
            string INSERT = "INSERT INTO registrar_prestamos (fecha, titulo_libro, autor, clasificacion, folio, nombre_alumno, carrera, numero_control) " +
                            "VALUES (@fecha, @titulo_libro, @autor, @clasificacion, @folio, @nombre_alumno, @carrera, @numero_control);";

            using (MySqlCommand mCommand = new MySqlCommand(INSERT, mConexion.getConexion()))
            {
                mCommand.Parameters.Add(new MySqlParameter("@fecha", mlibro.fecha));
                mCommand.Parameters.Add(new MySqlParameter("@titulo_libro", mlibro.titulo_libro));
                mCommand.Parameters.Add(new MySqlParameter("@autor", mlibro.autor));
                mCommand.Parameters.Add(new MySqlParameter("@clasificacion", mlibro.clasificacion));
                mCommand.Parameters.Add(new MySqlParameter("@folio", mlibro.folio));
                mCommand.Parameters.Add(new MySqlParameter("@nombre_alumno", mlibro.nombre_alumno));
                mCommand.Parameters.Add(new MySqlParameter("@carrera", mlibro.carrera));
                mCommand.Parameters.Add(new MySqlParameter("@numero_control", mlibro.numero_control));

                return mCommand.ExecuteNonQuery() > 0;
            }
        }

        public bool modificarLibroPorFolio(Libros mlibro)
        {
            string UPDATE = "UPDATE registrar_prestamos " +
                            "SET fecha = @fecha, " +
                            "titulo_libro = @titulo_libro, " +
                            "autor = @autor, " +
                            "clasificacion = @clasificacion, " +
                            "carrera = @carrera, " +
                            "numero_control = @numero_control " +
                            "WHERE folio = @folio;";

            MySqlCommand mCommand = new MySqlCommand(UPDATE, mConexion.getConexion());

            mCommand.Parameters.Add(new MySqlParameter("@fecha", mlibro.fecha));
            mCommand.Parameters.Add(new MySqlParameter("@titulo_libro", mlibro.titulo_libro));
            mCommand.Parameters.Add(new MySqlParameter("@autor", mlibro.autor));
            mCommand.Parameters.Add(new MySqlParameter("@clasificacion", mlibro.clasificacion));
            mCommand.Parameters.Add(new MySqlParameter("@carrera", mlibro.carrera));
            mCommand.Parameters.Add(new MySqlParameter("@numero_control", mlibro.numero_control));
            mCommand.Parameters.Add(new MySqlParameter("@folio", mlibro.folio));

            try
            {
                return mCommand.ExecuteNonQuery() > 0;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al modificar el libro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public bool eliminarLibroPorFolio(string folio)
        {
            string DELETE = "DELETE FROM registrar_prestamos WHERE folio = @folio;";
            MySqlCommand mCommand = new MySqlCommand(DELETE, mConexion.getConexion());

            mCommand.Parameters.Add(new MySqlParameter("@folio", folio));

            try
            {
                return mCommand.ExecuteNonQuery() > 0;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al eliminar el libro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public List<Libros> consultarLibro(string filtro)
        {
            string CONSULTA = "SELECT * FROM registrar_prestamos";

            if (!string.IsNullOrEmpty(filtro))
            {
                CONSULTA += " WHERE " +
                            "id_registro LIKE '%" + filtro + "%' OR " +
                            "fecha LIKE '%" + filtro + "%' OR " +
                            "titulo_libro LIKE '%" + filtro + "%' OR " +
                            "autor LIKE '%" + filtro + "%';";
            }

            using (MySqlCommand mCommand = new MySqlCommand(CONSULTA, mConexion.getConexion()))
            {
                mLibros.Clear(); // Limpiar la lista antes de llenarla nuevamente
                using (MySqlDataReader mReader = mCommand.ExecuteReader())
                {
                    while (mReader.Read())
                    {
                        Libros mlibro = new Libros
                        {
                            id_registro = mReader.GetInt32("id_registro"),
                            fecha = mReader.GetDateTime("fecha"),
                            titulo_libro = mReader.GetString("titulo_libro"),
                            autor = mReader.GetString("autor"),
                            clasificacion = mReader.GetString("clasificacion"),
                            folio = mReader.GetString("folio"),
                            nombre_alumno = mReader.GetString("nombre_alumno"),
                            carrera = mReader.GetString("carrera"),
                            numero_control = mReader.GetInt32("numero_control")
                        };
                        mLibros.Add(mlibro);
                    }
                }
            }

            return mLibros;
        }
    }
}

