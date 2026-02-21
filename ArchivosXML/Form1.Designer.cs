namespace ArchivosXML
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnBuscar = new Button();
            bindingSource1 = new BindingSource(components);
            btnCrearArchivo = new Button();
            btnAbrirArchiv = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnAgregar = new Button();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            txtBuscar = new TextBox();
            txtNombre = new TextBox();
            txtCelular = new TextBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(333, 18);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 0;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnCrearArchivo
            // 
            btnCrearArchivo.Location = new Point(456, 18);
            btnCrearArchivo.Name = "btnCrearArchivo";
            btnCrearArchivo.Size = new Size(113, 29);
            btnCrearArchivo.TabIndex = 1;
            btnCrearArchivo.Text = "Crear Archivo";
            btnCrearArchivo.UseVisualStyleBackColor = true;
            btnCrearArchivo.Click += btnCrearArchivo_Click;
            // 
            // btnAbrirArchiv
            // 
            btnAbrirArchiv.Location = new Point(456, 53);
            btnAbrirArchiv.Name = "btnAbrirArchiv";
            btnAbrirArchiv.Size = new Size(113, 29);
            btnAbrirArchiv.TabIndex = 2;
            btnAbrirArchiv.Text = "Abrir Archivo";
            btnAbrirArchiv.UseVisualStyleBackColor = true;
            btnAbrirArchiv.Click += btnAbrirArchiv_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(575, 88);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(108, 50);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Guardar Cambios";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(575, 53);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(108, 29);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar Info";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(575, 21);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(108, 29);
            btnAgregar.TabIndex = 5;
            btnAgregar.Text = "Agregar Info";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 144);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(569, 220);
            dataGridView1.TabIndex = 6;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(0, 370);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(569, 79);
            dataGridView2.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 8;
            label1.Text = "Buscar";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 68);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 9;
            label2.Text = "Nombre";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(81, 18);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(210, 27);
            txtBuscar.TabIndex = 10;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(81, 65);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(210, 27);
            txtNombre.TabIndex = 11;
            // 
            // txtCelular
            // 
            txtCelular.Location = new Point(81, 111);
            txtCelular.Name = "txtCelular";
            txtCelular.Size = new Size(210, 27);
            txtCelular.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 111);
            label3.Name = "label3";
            label3.Size = new Size(55, 20);
            label3.TabIndex = 12;
            label3.Text = "Celular";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtCelular);
            Controls.Add(label3);
            Controls.Add(txtNombre);
            Controls.Add(txtBuscar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Controls.Add(btnAgregar);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnAbrirArchiv);
            Controls.Add(btnCrearArchivo);
            Controls.Add(btnBuscar);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBuscar;
        private BindingSource bindingSource1;
        private Button btnCrearArchivo;
        private Button btnAbrirArchiv;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnAgregar;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Label label1;
        private Label label2;
        private TextBox txtBuscar;
        private TextBox txtNombre;
        private TextBox txtCelular;
        private Label label3;
    }
}
