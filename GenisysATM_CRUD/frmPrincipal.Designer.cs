namespace GenisysATM_CRUD
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpOpciones = new System.Windows.Forms.GroupBox();
            this.btnCuentas = new System.Windows.Forms.Button();
            this.btnConfiguracion = new System.Windows.Forms.Button();
            this.btnTarjetas = new System.Windows.Forms.Button();
            this.btnServicios = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.lbltitulo = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.grpOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOpciones
            // 
            this.grpOpciones.Controls.Add(this.btnCuentas);
            this.grpOpciones.Controls.Add(this.btnConfiguracion);
            this.grpOpciones.Controls.Add(this.btnTarjetas);
            this.grpOpciones.Controls.Add(this.btnServicios);
            this.grpOpciones.Controls.Add(this.btnClientes);
            this.grpOpciones.Controls.Add(this.lbltitulo);
            this.grpOpciones.Location = new System.Drawing.Point(12, 33);
            this.grpOpciones.Name = "grpOpciones";
            this.grpOpciones.Size = new System.Drawing.Size(487, 202);
            this.grpOpciones.TabIndex = 7;
            this.grpOpciones.TabStop = false;
            this.grpOpciones.Text = "Opciones Disponibles";
            // 
            // btnCuentas
            // 
            this.btnCuentas.Location = new System.Drawing.Point(349, 32);
            this.btnCuentas.Name = "btnCuentas";
            this.btnCuentas.Size = new System.Drawing.Size(102, 58);
            this.btnCuentas.TabIndex = 12;
            this.btnCuentas.Text = "Cuentas";
            this.btnCuentas.UseVisualStyleBackColor = true;
            // 
            // btnConfiguracion
            // 
            this.btnConfiguracion.Location = new System.Drawing.Point(166, 32);
            this.btnConfiguracion.Name = "btnConfiguracion";
            this.btnConfiguracion.Size = new System.Drawing.Size(102, 58);
            this.btnConfiguracion.TabIndex = 11;
            this.btnConfiguracion.Text = "Configuraciones";
            this.btnConfiguracion.UseVisualStyleBackColor = true;
            // 
            // btnTarjetas
            // 
            this.btnTarjetas.Location = new System.Drawing.Point(349, 110);
            this.btnTarjetas.Name = "btnTarjetas";
            this.btnTarjetas.Size = new System.Drawing.Size(102, 58);
            this.btnTarjetas.TabIndex = 10;
            this.btnTarjetas.Text = "Tarjetas de \r\n  Crédito";
            this.btnTarjetas.UseVisualStyleBackColor = true;
            // 
            // btnServicios
            // 
            this.btnServicios.Location = new System.Drawing.Point(20, 96);
            this.btnServicios.Name = "btnServicios";
            this.btnServicios.Size = new System.Drawing.Size(102, 58);
            this.btnServicios.TabIndex = 9;
            this.btnServicios.Text = "Servicios";
            this.btnServicios.UseVisualStyleBackColor = true;
            // 
            // btnClientes
            // 
            this.btnClientes.Location = new System.Drawing.Point(20, 32);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(102, 58);
            this.btnClientes.TabIndex = 8;
            this.btnClientes.Text = "Clientes";
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // lbltitulo
            // 
            this.lbltitulo.AutoSize = true;
            this.lbltitulo.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitulo.Location = new System.Drawing.Point(162, 125);
            this.lbltitulo.Name = "lbltitulo";
            this.lbltitulo.Size = new System.Drawing.Size(127, 24);
            this.lbltitulo.TabIndex = 7;
            this.lbltitulo.Text = "Genisys ATM";
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(361, 258);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(138, 34);
            this.btnSalir.TabIndex = 14;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click_1);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 301);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.grpOpciones);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cajeros Automáticos Genisys ATM";
            this.grpOpciones.ResumeLayout(false);
            this.grpOpciones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOpciones;
        private System.Windows.Forms.Button btnCuentas;
        private System.Windows.Forms.Button btnConfiguracion;
        private System.Windows.Forms.Button btnTarjetas;
        private System.Windows.Forms.Button btnServicios;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Label lbltitulo;
        private System.Windows.Forms.Button btnSalir;
    }
}

