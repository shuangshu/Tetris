namespace Tetris
{
    partial class TetrisMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TetrisMain));
            this.groupBoxPreview = new System.Windows.Forms.GroupBox();
            this.previewPalette = new Tetris.PreviewPalette();
            this.workPalette = new Tetris.WorkPalette();
            this.groupBoxPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Controls.Add(this.previewPalette);
            this.groupBoxPreview.ForeColor = System.Drawing.Color.SkyBlue;
            this.groupBoxPreview.Location = new System.Drawing.Point(535, 48);
            this.groupBoxPreview.Name = "groupBoxPreview";
            this.groupBoxPreview.Size = new System.Drawing.Size(133, 151);
            this.groupBoxPreview.TabIndex = 2;
            this.groupBoxPreview.TabStop = false;
            this.groupBoxPreview.Text = "预览:";
            // 
            // previewPalette
            // 
            this.previewPalette.BackColor = System.Drawing.SystemColors.Window;
            this.previewPalette.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.previewPalette.Location = new System.Drawing.Point(6, 22);
            this.previewPalette.Name = "previewPalette";
            this.previewPalette.PreviewBlock = null;
            this.previewPalette.Size = new System.Drawing.Size(121, 121);
            this.previewPalette.TabIndex = 0;
            // 
            // workPalette
            // 
            this.workPalette.Horizontal = 50;
            this.workPalette.Location = new System.Drawing.Point(12, 48);
            this.workPalette.Name = "workPalette";
            this.workPalette.NextBlock = null;
            this.workPalette.Pixels = 10;
            this.workPalette.Size = new System.Drawing.Size(500, 500);
            this.workPalette.TabIndex = 1;
            this.workPalette.Vertical = 50;
            this.workPalette.WorkBlock = null;
            // 
            // TetrisMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(699, 620);
            this.Controls.Add(this.groupBoxPreview);
            this.Controls.Add(this.workPalette);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TetrisMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TetrisMain";
            this.groupBoxPreview.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PreviewPalette previewPalette;
        private WorkPalette workPalette;
        private System.Windows.Forms.GroupBox groupBoxPreview;






















    }
}

