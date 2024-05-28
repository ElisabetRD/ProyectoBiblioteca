using Biblioteca.Consultas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Biblioteca
{
    public partial class CRUD : Form
    {
        private ConsultaLibro mConsultaLibro;
        private Libros mLibro;
        private List<Libros> mLibros;

        public CRUD()
        {
            InitializeComponent();
            dataGridView2.CellClick += dataGridView2_CellClick;

            mConsultaLibro = new ConsultaLibro();
            mLibro = new Libros();
            mLibros = new List<Libros>();

            CargarLibro();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {

        }


        private bool datosCorrectos()
        {
            // Validar la fecha
            if (dtpFecha.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese la fecha de préstamo");
                return false;
            }

            // Validar el título
            if (txtTitulo.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el título del libro");
                return false;
            }

            // Validar el autor
            if (txtAutor.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el autor del libro");
                return false;
            }

            // Validar clasificación
            if (cbClasificacion.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese la clasificación");
                return false;
            }

            // Validar el folio
            if (txtFolio.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el folio");
                return false;
            }

            // Validar la carrera
            if (cbCarrera.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese la carrera");
                return false;
            }

            // Validar el nombre del alumno
            if (txtNombreAlumno.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el nombre del alumno");
                return false;
            }

            // Validar el número de control
            if (txtNumeroControl.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el número de control");
                return false;
            }

            if (!int.TryParse(txtNumeroControl.Text.Trim(), out _))
            {
                MessageBox.Show("Ingrese un número de control correcto");
                return false;
            }

            return true;
        }

        private void CargarDatosLibro()
        {

            try
            {
                mLibro.fecha = DateTime.Parse(dtpFecha.Text);
                mLibro.titulo_libro = txtTitulo.Text.Trim();
                mLibro.autor = txtAutor.Text.Trim();
                mLibro.clasificacion = cbClasificacion.Text.Trim();
                mLibro.folio = txtFolio.Text.Trim();
                mLibro.nombre_alumno = txtNombreAlumno.Text.Trim();
                mLibro.carrera = cbCarrera.Text.Trim();
                mLibro.numero_control = int.Parse(txtNumeroControl.Text.Trim());
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Error al cargar datos del libro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarDatoslibro()
        {
            dtpFecha.Text = "";
            txtTitulo.Text = "";
            txtAutor.Text = "";
            cbClasificacion.Text = "";
            txtFolio.Text = "";
            cbCarrera.Text = "";
            txtNombreAlumno.Text = "";
            txtNumeroControl.Text = "";
        }

        private void CargarLibro(string filtro = "")
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            mLibros.Clear();
            mLibros = mConsultaLibro.consultarLibro(filtro);

            foreach (var libro in mLibros)
            {
                dataGridView2.Rows.Add(
                   libro.fecha,
                   libro.titulo_libro,
                   libro.autor,
                   libro.clasificacion,
                   libro.folio,
                   libro.carrera,
                   libro.nombre_alumno,
                   libro.numero_control
                );
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow fila = dataGridView2.Rows[e.RowIndex];

                dtpFecha.Text = Convert.ToString(fila.Cells["Fecha"].Value);
                txtTitulo.Text = Convert.ToString(fila.Cells["Titulo"].Value);
                txtAutor.Text = Convert.ToString(fila.Cells["Autor"].Value);
                cbClasificacion.Text = Convert.ToString(fila.Cells["Clasificacion"].Value);
                txtFolio.Text = Convert.ToString(fila.Cells["Folio"].Value);
                cbCarrera.Text = Convert.ToString(fila.Cells["Carrera"].Value);
                txtNombreAlumno.Text = Convert.ToString(fila.Cells["Nombre_alumno"].Value);
                txtNumeroControl.Text = Convert.ToString(fila.Cells["Numero_control"].Value);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inicio regi = new Inicio();
            regi.ShowDialog();
        }

        private void txtBusqueda_TextChanged_1(object sender, EventArgs e)
        {
            CargarLibro(txtBusqueda.Text.Trim());
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            if (!datosCorrectos())
                return;

            CargarDatosLibro();

            if (mConsultaLibro.agregarLibro(mLibro))
            {
                MessageBox.Show("Préstamo agregado");
                CargarLibro();
                LimpiarDatoslibro();
            }
            else
            {
                MessageBox.Show("Error al agregar el préstamo");
            }

        }
        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            // Verificar que los datos ingresados sean correctos
            if (!datosCorrectos())
                return;

            // Cargar los datos del libro desde los campos del formulario
            CargarDatosLibro();

            // Llamar al método de modificar libro
            if (mConsultaLibro.modificarLibroPorFolio(mLibro))
            {
                MessageBox.Show("Préstamo modificado con éxito.");
                // Recargar la lista de libros en el DataGridView
                CargarLibro();
                // Limpiar los campos del formulario
                LimpiarDatoslibro();
            }
            else
            {
                MessageBox.Show("Error al modificar el préstamo.");
            }
        }



        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            LimpiarDatoslibro();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el producto?", "Eliminar producto", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string folio = txtFolio.Text.Trim(); // Obtener el folio del libro seleccionado
                if (!string.IsNullOrEmpty(folio))
                {
                    if (mConsultaLibro.eliminarLibroPorFolio(folio))
                    {
                        MessageBox.Show("Producto eliminado con éxito.");
                        CargarLibro();
                        LimpiarDatoslibro();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el producto.");
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un libro antes de intentar eliminarlo.");
                }
            }
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
