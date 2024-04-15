namespace MethuselahRS.UserControls
{
    partial class HeaderPanel
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
            this.launchClient = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // launchClient
            // 
            this.launchClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.launchClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.launchClient.FlatAppearance.BorderSize = 0;
            this.launchClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.launchClient.ForeColor = System.Drawing.Color.Gainsboro;
            this.launchClient.Location = new System.Drawing.Point(62, 11);
            this.launchClient.Margin = new System.Windows.Forms.Padding(2);
            this.launchClient.Name = "launchClient";
            this.launchClient.Size = new System.Drawing.Size(96, 23);
            this.launchClient.TabIndex = 10;
            this.launchClient.Text = "Client Options";
            this.launchClient.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pictureBox1.Image = global::MethuselahRS.Properties.Resources.logout;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 40);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // HeaderPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.launchClient);
            this.Name = "HeaderPanel";
            this.Size = new System.Drawing.Size(966, 46);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button launchClient;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
