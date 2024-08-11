namespace WWSD
{
    partial class menue
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sellingproduct1 = new WWSD.sellingproduct();
            this.product1 = new WWSD.product();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(19, 219);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 101);
            this.button1.TabIndex = 0;
            this.button1.Text = "Buying Item ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(19, 396);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(202, 101);
            this.button2.TabIndex = 1;
            this.button2.Text = "Selling Item ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.product1);
            this.panel1.Controls.Add(this.sellingproduct1);
            this.panel1.Location = new System.Drawing.Point(253, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1320, 980);
            this.panel1.TabIndex = 2;
            // 
            // sellingproduct1
            // 
            this.sellingproduct1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sellingproduct1.Location = new System.Drawing.Point(0, 0);
            this.sellingproduct1.Name = "sellingproduct1";
            this.sellingproduct1.Size = new System.Drawing.Size(1320, 980);
            this.sellingproduct1.TabIndex = 0;
            // 
            // product1
            // 
            this.product1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.product1.Location = new System.Drawing.Point(0, 0);
            this.product1.Name = "product1";
            this.product1.Size = new System.Drawing.Size(1320, 980);
            this.product1.TabIndex = 1;
            // 
            // menue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "menue";
            this.Size = new System.Drawing.Size(1606, 1038);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private product product1;
        private sellingproduct sellingproduct1;
    }
}
