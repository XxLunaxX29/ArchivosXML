namespace ArchivosXML
{
    public partial class Form1 : Form
    {
        private List<string[]> csvData;
        private string[] headers;
        private int selectedRowIndex = -1;
        private string rutaArchivo;
        public Form1()
        {
            InitializeComponent();
            ConfiguracionDataGridView();
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }
        private void ConfiguracionDataGridView()
        {
            dataGridView1.VirtualMode = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.CellValueNeeded += DataGridView1_CellValueNeeded;

            dataGridView2.ReadOnly = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.CellSelect;

        }
        private void DataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            // Solo proporcionar datos cuando se necesiten (virtualización)
            if (csvData != null && e.RowIndex < csvData.Count)
            {
                var row = csvData[e.RowIndex];
                if (e.ColumnIndex < row.Length)
                {
                    e.Value = row[e.ColumnIndex];
                }
            }
        }
        private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            if (csvData == null)
            {
                selectedRowIndex = -1;
                return;
            }

            if (selectedRowIndex >= 0 && selectedRowIndex < csvData.Count)
            {
                SaveEditorToCsvData(selectedRowIndex);
            }

            if (dataGridView1.SelectedRows.Count == 0)
            {
                selectedRowIndex = -1;
                return;
            }

            var sel = dataGridView1.SelectedRows[0];
            int rowIndex = sel.Index;
            if (rowIndex < 0 || rowIndex >= csvData.Count)
            {
                selectedRowIndex = -1;
                return;
            }

            selectedRowIndex = rowIndex;
            LoadRowIntoEditor(csvData[rowIndex]);
        }
        private void SaveEditorToCsvData(int rowIndex)
        {
            if (csvData == null || headers == null) return;
            if (rowIndex < 0 || rowIndex >= csvData.Count) return;
            try
            {
                dataGridView2.EndEdit();
                dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch
            {

            }
            var newRow = new string[headers.Length];
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = dataGridView2.Rows[0].Cells[i];
                newRow[i] = cell?.Value?.ToString() ?? string.Empty;
            }
            csvData[rowIndex] = newRow;
            dataGridView1.InvalidateRow(rowIndex);
        }
        private void LoadRowIntoEditor(string[] row)
        {
            int colCount = Math.Min(row.Length, dataGridView2.ColumnCount);

            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {
                dataGridView2.Rows[0].Cells[i].Value = i < colCount ? row[i] : string.Empty;
            }
        }
        private void CargarXML(string rutaArchivo)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string? firstLine = File.ReadLines(rutaArchivo).FirstOrDefault();

                if (string.IsNullOrEmpty(firstLine))
                {
                    MessageBox.Show("El archivo está vacío.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                headers = firstLine.Split(',');

                csvData = File.ReadAllLines(rutaArchivo)
                              .Skip(1)
                              .Select(line => line.Split(','))
                              .ToList();

                dataGridView1.Columns.Clear();

                for (int i = 0; i < headers.Length; i++)
                {
                    dataGridView1.Columns.Add($"Column{i}", headers[i]);
                }

                dataGridView1.RowCount = csvData.Count;

                dataGridView2.Columns.Clear();
                for (int i = 0; i < headers.Length; i++)
                {
                    dataGridView2.Columns.Add($"EditCol{i}", headers[i]);
                }
                dataGridView2.RowCount = 1;

                selectedRowIndex = -1;
                dataGridView1.ClearSelection();

                MessageBox.Show($"Se cargaron {csvData.Count:N0} registros exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el archivo: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos cargados");
                return;
            }

            string textoBuscar = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(textoBuscar))
            {
                MessageBox.Show("Escribe un texto para buscar");
                return;
            }

            textoBuscar = textoBuscar.ToLower();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                foreach (DataGridViewCell celda in fila.Cells)
                {
                    if (celda.Value != null &&
                        celda.Value.ToString().ToLower().Contains(textoBuscar))
                    {
                        fila.Selected = true;

                        dataGridView1.FirstDisplayedScrollingRowIndex = fila.Index;

                        MessageBox.Show($"Texto encontrado en la fila {fila.Index + 1}");
                        return;
                    }
                }
            }

            MessageBox.Show("Texto no encontrado");
        }

        private void btnCrearArchivo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCelular.Text))
            {
                MessageBox.Show("Debes llenar con informacion.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos XML|*.xml|Todos los archivos|*.*";
            sfd.Title = "";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string ruta = sfd.FileName;
                    string encabezado = "Nombre, # Celular";
                    string datos = txtNombre.Text + "," + txtCelular.Text;
                    File.WriteAllLines(ruta, new[] { encabezado, datos });
                    MessageBox.Show("Archivo creado exitosamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNombre.Clear();
                    txtCelular.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al crear el archivo: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAbrirArchiv_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos XML|*.xml|Todos los archivos|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    rutaArchivo = ofd.FileName;
                    CargarXML(ofd.FileName);
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rutaArchivo))
            {
                MessageBox.Show("No hay ningún archivo cargado en el DataGrid.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCelular.Text))
            {
                MessageBox.Show("Debe escribir información en ambos TextBox.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string linea = txtNombre.Text + "," + txtCelular.Text;

                File.AppendAllText(rutaArchivo, linea + Environment.NewLine);

                string[] nuevaFila = linea.Split(',');
                csvData.Add(nuevaFila);

                dataGridView1.RowCount = csvData.Count;
                dataGridView1.Refresh();

                int lastRow = dataGridView1.RowCount - 1;
                if (lastRow >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[lastRow].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = lastRow;
                }

                MessageBox.Show("Datos agregados correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombre.Clear();
                txtCelular.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar datos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("No hay ninguna fila seleccionada.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int rowIndex = dataGridView1.CurrentCell.RowIndex;

            if (csvData == null || rowIndex < 0 || rowIndex >= csvData.Count)
            {
                MessageBox.Show("No hay datos para eliminar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("¿Estás seguro de eliminar este registro?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            selectedRowIndex = -1; // muy importante

            csvData.RemoveAt(rowIndex);

            dataGridView1.RowCount = csvData.Count;

            if (csvData.Count > 0)
            {
                int newIndex = Math.Min(rowIndex, csvData.Count - 1);
                dataGridView1.CurrentCell = dataGridView1.Rows[newIndex].Cells[0];
            }

            dataGridView1.Invalidate();

            MessageBox.Show("Registro eliminado.", "Listo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (csvData == null || csvData.Count == 0)
            {
                MessageBox.Show("No hay datos para guardar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(rutaArchivo))
            {
                MessageBox.Show("No se ha cargado ningún archivo para guardar.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (selectedRowIndex >= 0 && selectedRowIndex < csvData.Count)
            {
                SaveEditorToCsvData(selectedRowIndex);
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var lines = new List<string>
                {
                     string.Join(",", headers)
                };

                lines.AddRange(csvData.Select(r => string.Join(",", r)));

                File.WriteAllLines(rutaArchivo, lines);

                MessageBox.Show("Archivo actualizado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el archivo: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
