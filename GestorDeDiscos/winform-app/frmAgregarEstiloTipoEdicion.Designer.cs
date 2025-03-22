namespace winform_app
{
    partial class frmAgregarEstiloTipoEdicion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNuevoEstiloTipoEdicion = new System.Windows.Forms.Label();
            this.txtEstiloTipoEdicion = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNuevoEstiloTipoEdicion
            // 
            this.lblNuevoEstiloTipoEdicion.AutoSize = true;
            this.lblNuevoEstiloTipoEdicion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNuevoEstiloTipoEdicion.Location = new System.Drawing.Point(30, 84);
            this.lblNuevoEstiloTipoEdicion.Name = "lblNuevoEstiloTipoEdicion";
            this.lblNuevoEstiloTipoEdicion.Size = new System.Drawing.Size(42, 15);
            this.lblNuevoEstiloTipoEdicion.TabIndex = 3;
            this.lblNuevoEstiloTipoEdicion.Text = "Nuevo";
            // 
            // txtEstiloTipoEdicion
            // 
            this.txtEstiloTipoEdicion.Location = new System.Drawing.Point(90, 82);
            this.txtEstiloTipoEdicion.Name = "txtEstiloTipoEdicion";
            this.txtEstiloTipoEdicion.Size = new System.Drawing.Size(159, 20);
            this.txtEstiloTipoEdicion.TabIndex = 4;
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(161, 146);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(79, 29);
            this.btnSalir.TabIndex = 9;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(50, 146);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(79, 29);
            this.btnAgregar.TabIndex = 8;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // frmAgregarEstiloTipoEdicion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 208);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtEstiloTipoEdicion);
            this.Controls.Add(this.lblNuevoEstiloTipoEdicion);
            this.Name = "frmAgregarEstiloTipoEdicion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAltaEstiloTipoEdicion";
            this.Load += new System.EventHandler(this.FrmAgregarEstiloTipoEdicion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNuevoEstiloTipoEdicion;
        private System.Windows.Forms.TextBox txtEstiloTipoEdicion;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAgregar;
    }
}