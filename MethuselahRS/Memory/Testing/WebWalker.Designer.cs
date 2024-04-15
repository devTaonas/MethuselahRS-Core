namespace MethuselahRS.Memory.Testing
{
    partial class WebWalker
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_CreatePath = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_sx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_PixelColor = new System.Windows.Forms.Label();
            this.PixelColor = new System.Windows.Forms.Button();
            this.labelWorldCoords = new System.Windows.Forms.Label();
            this.labelScreenCoords = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tb_sy = new System.Windows.Forms.TextBox();
            this.tb_ey = new System.Windows.Forms.TextBox();
            this.tb_ex = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tb_ey);
            this.panel1.Controls.Add(this.tb_ex);
            this.panel1.Controls.Add(this.tb_sy);
            this.panel1.Controls.Add(this.btn_CreatePath);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tb_sx);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl_PixelColor);
            this.panel1.Controls.Add(this.PixelColor);
            this.panel1.Controls.Add(this.labelWorldCoords);
            this.panel1.Controls.Add(this.labelScreenCoords);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 794);
            this.panel1.TabIndex = 0;
            // 
            // btn_CreatePath
            // 
            this.btn_CreatePath.Location = new System.Drawing.Point(34, 148);
            this.btn_CreatePath.Name = "btn_CreatePath";
            this.btn_CreatePath.Size = new System.Drawing.Size(112, 35);
            this.btn_CreatePath.TabIndex = 9;
            this.btn_CreatePath.Text = "CREATE PATH";
            this.btn_CreatePath.UseVisualStyleBackColor = true;
            this.btn_CreatePath.Click += new System.EventHandler(this.btn_CreatePath_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ending Coordinate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Starting Coordinate";
            // 
            // tb_sx
            // 
            this.tb_sx.Location = new System.Drawing.Point(41, 61);
            this.tb_sx.Name = "tb_sx";
            this.tb_sx.Size = new System.Drawing.Size(45, 23);
            this.tb_sx.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(17, 450);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // lbl_PixelColor
            // 
            this.lbl_PixelColor.AutoSize = true;
            this.lbl_PixelColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.lbl_PixelColor.Location = new System.Drawing.Point(17, 392);
            this.lbl_PixelColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_PixelColor.Name = "lbl_PixelColor";
            this.lbl_PixelColor.Size = new System.Drawing.Size(46, 17);
            this.lbl_PixelColor.TabIndex = 3;
            this.lbl_PixelColor.Text = "label1";
            // 
            // PixelColor
            // 
            this.PixelColor.Location = new System.Drawing.Point(29, 358);
            this.PixelColor.Name = "PixelColor";
            this.PixelColor.Size = new System.Drawing.Size(29, 31);
            this.PixelColor.TabIndex = 2;
            this.PixelColor.Text = " ";
            this.PixelColor.UseVisualStyleBackColor = true;
            // 
            // labelWorldCoords
            // 
            this.labelWorldCoords.AutoSize = true;
            this.labelWorldCoords.Location = new System.Drawing.Point(26, 302);
            this.labelWorldCoords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWorldCoords.Name = "labelWorldCoords";
            this.labelWorldCoords.Size = new System.Drawing.Size(46, 17);
            this.labelWorldCoords.TabIndex = 1;
            this.labelWorldCoords.Text = "label1";
            // 
            // labelScreenCoords
            // 
            this.labelScreenCoords.AutoSize = true;
            this.labelScreenCoords.Location = new System.Drawing.Point(26, 253);
            this.labelScreenCoords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelScreenCoords.Name = "labelScreenCoords";
            this.labelScreenCoords.Size = new System.Drawing.Size(46, 17);
            this.labelScreenCoords.TabIndex = 0;
            this.labelScreenCoords.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(319, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(966, 794);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MethuselahRS.Properties.Resources.RuneScape_Worldmap;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(3829, 3200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tb_sy
            // 
            this.tb_sy.Location = new System.Drawing.Point(120, 61);
            this.tb_sy.Name = "tb_sy";
            this.tb_sy.Size = new System.Drawing.Size(45, 23);
            this.tb_sy.TabIndex = 10;
            // 
            // tb_ey
            // 
            this.tb_ey.Location = new System.Drawing.Point(120, 119);
            this.tb_ey.Name = "tb_ey";
            this.tb_ey.Size = new System.Drawing.Size(45, 23);
            this.tb_ey.TabIndex = 12;
            // 
            // tb_ex
            // 
            this.tb_ex.Location = new System.Drawing.Point(41, 119);
            this.tb_ex.Name = "tb_ex";
            this.tb_ex.Size = new System.Drawing.Size(45, 23);
            this.tb_ex.TabIndex = 11;
            // 
            // WebWalker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1285, 794);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WebWalker";
            this.Text = "WebWalker";
            this.Load += new System.EventHandler(this.WebWalker_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelScreenCoords;
        private System.Windows.Forms.Label labelWorldCoords;
        private System.Windows.Forms.Button PixelColor;
        private System.Windows.Forms.Label lbl_PixelColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_sx;
        private System.Windows.Forms.Button btn_CreatePath;
        private System.Windows.Forms.TextBox tb_ey;
        private System.Windows.Forms.TextBox tb_ex;
        private System.Windows.Forms.TextBox tb_sy;
    }
}