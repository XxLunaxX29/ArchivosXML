using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ArchivosXML
{
    public partial class Form1 : Form
    {
        private List<Persona> listaPersonas = new List<Persona>();
        private string rutaArchivo = "";

        public Form1()
        {
            InitializeComponent();
            ConfigurarDataGrid();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void ConfigurarDataGrid()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnCrearArchivo_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos XML|*.xml";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = sfd.FileName;
                listaPersonas = new List<Persona>();
                GuardarXML();
                MessageBox.Show("Archivo XML creado correctamente.");
            }
        }

        private void btnAbrirArchiv_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos XML|*.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = ofd.FileName;
                CargarXML(rutaArchivo);
            }
        }

        private void CargarXML(string ruta)
        {
            try
            {
                if (!File.Exists(ruta)) return;

                XmlSerializer serializer = new XmlSerializer(typeof(List<Persona>));

                using (FileStream fs = new FileStream(ruta, FileMode.Open))
                {
                    listaPersonas = (List<Persona>)serializer.Deserialize(fs);
                }

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listaPersonas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer XML: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rutaArchivo))
            {
                MessageBox.Show("No hay archivo abierto.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCelular.Text))
            {
                MessageBox.Show("Llena ambos campos.");
                return;
            }

            listaPersonas.Add(new Persona
            {
                Nombre = txtNombre.Text,
                Celular = int.Parse(txtCelular.Text)
            });

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaPersonas;

            txtNombre.Clear();
            txtCelular.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rutaArchivo))
            {
                MessageBox.Show("No hay archivo abierto.");
                return;
            }
            if (dataGridView1.CurrentRow == null) return;

            int index = dataGridView1.CurrentRow.Index;

            var confirm = MessageBox.Show("¿Eliminar este registro?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                listaPersonas.RemoveAt(index);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listaPersonas;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rutaArchivo))
            {
                MessageBox.Show("No hay archivo abierto.");
                return;
            }
            string texto = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(texto))
            {
                MessageBox.Show("Escribe un texto para buscar.");
                return;
            }

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                fila.Selected = false;

                string nombre = fila.Cells[0].Value?.ToString()?.Trim().ToLower() ?? "";
                string celular = fila.Cells[1].Value?.ToString()?.Trim().ToLower() ?? "";

                if (nombre == texto || celular == texto)
                {
                    fila.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = fila.Index;
                    MessageBox.Show("Encontrado en la fila " + (fila.Index + 1));
                    return;
                }
            }

            MessageBox.Show("No encontrado.");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rutaArchivo))
            {
                MessageBox.Show("No hay archivo abierto.");
                return;
            }

            GuardarXML();
            MessageBox.Show("Archivo XML guardado correctamente.");
        }

        private void GuardarXML()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Persona>));

                using (FileStream fs = new FileStream(rutaArchivo, FileMode.Create))
                {
                    serializer.Serialize(fs, listaPersonas);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar XML: " + ex.Message);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            if (dataGridView1.CurrentRow.Index < 0) return;
            if (dataGridView1.CurrentRow.IsNewRow) return;
            int index = dataGridView1.CurrentRow.Index;
            txtNombre.Text = dataGridView1.CurrentRow.Cells[0].Value?.ToString() ?? "";
            txtCelular.Text = dataGridView1.CurrentRow.Cells[1].Value?.ToString() ?? "";
        }
    }
}