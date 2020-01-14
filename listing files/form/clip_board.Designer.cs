namespace listing_files.form
{
    partial class clip_board
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(clip_board));
            this.start = new System.Windows.Forms.PictureBox();
            this.T = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.T)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Image = ((System.Drawing.Image)(resources.GetObject("start.Image")));
            this.start.Location = new System.Drawing.Point(81, 12);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(40, 40);
            this.start.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.start.TabIndex = 0;
            this.start.TabStop = false;
            this.start.Click += new System.EventHandler(this.Start_Click);
            // 
            // T
            // 
            this.T.Location = new System.Drawing.Point(12, 12);
            this.T.Name = "T";
            this.T.Size = new System.Drawing.Size(40, 40);
            this.T.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.T.TabIndex = 0;
            this.T.TabStop = false;
            // 
            // clip_board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(138, 60);
            this.Controls.Add(this.start);
            this.Controls.Add(this.T);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "clip_board";
            ((System.ComponentModel.ISupportInitialize)(this.start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.T)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox T;
        private System.Windows.Forms.PictureBox start;
    }
}