namespace MyStockSystem.SubItems
{
    partial class DownloadImgForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PrgDownload = new System.Windows.Forms.ProgressBar();
            this.ImgDownload = new System.Windows.Forms.PictureBox();
            this.LbImageUrl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgDownload)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PrgDownload);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ImgDownload);
            this.splitContainer1.Size = new System.Drawing.Size(760, 920);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 0;
            // 
            // PrgDownload
            // 
            this.PrgDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrgDownload.Location = new System.Drawing.Point(0, 0);
            this.PrgDownload.Name = "PrgDownload";
            this.PrgDownload.Size = new System.Drawing.Size(760, 25);
            this.PrgDownload.TabIndex = 0;
            // 
            // ImgDownload
            // 
            this.ImgDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImgDownload.Location = new System.Drawing.Point(0, 0);
            this.ImgDownload.Name = "ImgDownload";
            this.ImgDownload.Size = new System.Drawing.Size(760, 891);
            this.ImgDownload.TabIndex = 0;
            this.ImgDownload.TabStop = false;
            // 
            // LbImageUrl
            // 
            this.LbImageUrl.Location = new System.Drawing.Point(236, 33);
            this.LbImageUrl.Name = "LbImageUrl";
            this.LbImageUrl.Size = new System.Drawing.Size(544, 24);
            this.LbImageUrl.TabIndex = 1;
            this.LbImageUrl.Text = "ImageUrl";
            this.LbImageUrl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DownloadImgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 1000);
            this.Controls.Add(this.LbImageUrl);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DownloadImgForm";
            this.Text = "코스 정보";
            this.Load += new System.EventHandler(this.DownloadImgForm_Load);
            this.Shown += new System.EventHandler(this.DownloadImgForm_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImgDownload)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ProgressBar PrgDownload;
        private System.Windows.Forms.PictureBox ImgDownload;
        private System.Windows.Forms.Label LbImageUrl;
    }
}