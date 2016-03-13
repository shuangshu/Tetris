namespace Tetris
{
    partial class TetrisSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TetrisSetting));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageCommon = new System.Windows.Forms.TabPage();
            this.checkBoxVoice = new System.Windows.Forms.CheckBox();
            this.checkBoxManual = new System.Windows.Forms.CheckBox();
            this.numericUpDownPixels = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownVertical = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHorizontal = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backSelection = new Tetris.ColorSelection();
            this.txtlHotKeys = new System.Windows.Forms.TextBox();
            this.lblHotKeys = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.cName = new System.Windows.Forms.ColumnHeader();
            this.cShortcut = new System.Windows.Forms.ColumnHeader();
            this.tabPageBlock = new System.Windows.Forms.TabPage();
            this.listBox = new Tetris.ListBoxEx();
            this.blockSelection = new Tetris.ColorSelection();
            this.imageButtonEmpty = new Tetris.ImageButton();
            this.imageButtonDelete = new Tetris.ImageButton();
            this.imageButtonAdd = new Tetris.ImageButton();
            this.imageButtonModify = new Tetris.ImageButton();
            this.blockDesign = new Tetris.BlockDesign();
            this.imageButtonSave = new Tetris.ImageButton();
            this.tabControl1.SuspendLayout();
            this.tabPageCommon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPixels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHorizontal)).BeginInit();
            this.tabPageBlock.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageCommon);
            this.tabControl1.Controls.Add(this.tabPageBlock);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(443, 273);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageCommon
            // 
            this.tabPageCommon.Controls.Add(this.checkBoxVoice);
            this.tabPageCommon.Controls.Add(this.checkBoxManual);
            this.tabPageCommon.Controls.Add(this.numericUpDownPixels);
            this.tabPageCommon.Controls.Add(this.numericUpDownVertical);
            this.tabPageCommon.Controls.Add(this.numericUpDownHorizontal);
            this.tabPageCommon.Controls.Add(this.label3);
            this.tabPageCommon.Controls.Add(this.label2);
            this.tabPageCommon.Controls.Add(this.label1);
            this.tabPageCommon.Controls.Add(this.backSelection);
            this.tabPageCommon.Controls.Add(this.txtlHotKeys);
            this.tabPageCommon.Controls.Add(this.lblHotKeys);
            this.tabPageCommon.Controls.Add(this.listView);
            this.tabPageCommon.Location = new System.Drawing.Point(4, 21);
            this.tabPageCommon.Name = "tabPageCommon";
            this.tabPageCommon.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCommon.Size = new System.Drawing.Size(435, 248);
            this.tabPageCommon.TabIndex = 0;
            this.tabPageCommon.Text = "基本设置";
            this.tabPageCommon.UseVisualStyleBackColor = true;
            // 
            // checkBoxVoice
            // 
            this.checkBoxVoice.AutoSize = true;
            this.checkBoxVoice.Location = new System.Drawing.Point(360, 154);
            this.checkBoxVoice.Name = "checkBoxVoice";
            this.checkBoxVoice.Size = new System.Drawing.Size(72, 16);
            this.checkBoxVoice.TabIndex = 17;
            this.checkBoxVoice.Text = "声音效果";
            this.checkBoxVoice.UseVisualStyleBackColor = true;
            this.checkBoxVoice.CheckedChanged += new System.EventHandler(this.checkBoxVoice_CheckedChanged);
            // 
            // checkBoxManual
            // 
            this.checkBoxManual.AutoSize = true;
            this.checkBoxManual.Location = new System.Drawing.Point(273, 154);
            this.checkBoxManual.Name = "checkBoxManual";
            this.checkBoxManual.Size = new System.Drawing.Size(72, 16);
            this.checkBoxManual.TabIndex = 16;
            this.checkBoxManual.Text = "手动操作";
            this.checkBoxManual.UseVisualStyleBackColor = true;
            this.checkBoxManual.CheckedChanged += new System.EventHandler(this.checkBoxManual_CheckedChanged);
            // 
            // numericUpDownPixels
            // 
            this.numericUpDownPixels.Location = new System.Drawing.Point(70, 211);
            this.numericUpDownPixels.Name = "numericUpDownPixels";
            this.numericUpDownPixels.Size = new System.Drawing.Size(133, 21);
            this.numericUpDownPixels.TabIndex = 15;
            // 
            // numericUpDownVertical
            // 
            this.numericUpDownVertical.Location = new System.Drawing.Point(274, 181);
            this.numericUpDownVertical.Name = "numericUpDownVertical";
            this.numericUpDownVertical.Size = new System.Drawing.Size(132, 21);
            this.numericUpDownVertical.TabIndex = 14;
            // 
            // numericUpDownHorizontal
            // 
            this.numericUpDownHorizontal.Location = new System.Drawing.Point(70, 181);
            this.numericUpDownHorizontal.Name = "numericUpDownHorizontal";
            this.numericUpDownHorizontal.Size = new System.Drawing.Size(133, 21);
            this.numericUpDownHorizontal.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "方块像素:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "垂直方块:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "水平方块:";
            // 
            // backSelection
            // 
            this.backSelection.ActiveControl = this;
            this.backSelection.BlockDesign = null;
            this.backSelection.Location = new System.Drawing.Point(273, 208);
            this.backSelection.Name = "backSelection";
            this.backSelection.Selection = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.backSelection.Size = new System.Drawing.Size(133, 24);
            this.backSelection.TabIndex = 6;
            this.backSelection.Text = "背景色";
            this.backSelection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.backSelection.UseVisualStyleBackColor = true;
            // 
            // txtlHotKeys
            // 
            this.txtlHotKeys.BackColor = System.Drawing.SystemColors.Window;
            this.txtlHotKeys.Location = new System.Drawing.Point(70, 152);
            this.txtlHotKeys.Name = "txtlHotKeys";
            this.txtlHotKeys.ReadOnly = true;
            this.txtlHotKeys.Size = new System.Drawing.Size(133, 21);
            this.txtlHotKeys.TabIndex = 2;
            this.txtlHotKeys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtlHotKeys_KeyDown);
            // 
            // lblHotKeys
            // 
            this.lblHotKeys.AutoSize = true;
            this.lblHotKeys.Location = new System.Drawing.Point(8, 155);
            this.lblHotKeys.Name = "lblHotKeys";
            this.lblHotKeys.Size = new System.Drawing.Size(47, 12);
            this.lblHotKeys.TabIndex = 1;
            this.lblHotKeys.Text = "快捷键:";
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cName,
            this.cShortcut});
            this.listView.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView.FullRowSelect = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.Location = new System.Drawing.Point(3, 3);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(429, 137);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // cName
            // 
            this.cName.Text = "命令";
            this.cName.Width = 113;
            // 
            // cShortcut
            // 
            this.cShortcut.Text = "快捷键";
            this.cShortcut.Width = 306;
            // 
            // tabPageBlock
            // 
            this.tabPageBlock.Controls.Add(this.listBox);
            this.tabPageBlock.Controls.Add(this.blockSelection);
            this.tabPageBlock.Controls.Add(this.imageButtonEmpty);
            this.tabPageBlock.Controls.Add(this.imageButtonDelete);
            this.tabPageBlock.Controls.Add(this.imageButtonAdd);
            this.tabPageBlock.Controls.Add(this.imageButtonModify);
            this.tabPageBlock.Controls.Add(this.blockDesign);
            this.tabPageBlock.Location = new System.Drawing.Point(4, 21);
            this.tabPageBlock.Name = "tabPageBlock";
            this.tabPageBlock.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBlock.Size = new System.Drawing.Size(435, 248);
            this.tabPageBlock.TabIndex = 1;
            this.tabPageBlock.Text = "方块设置";
            this.tabPageBlock.UseVisualStyleBackColor = true;
            // 
            // listBox
            // 
            this.listBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 30;
            this.listBox.Location = new System.Drawing.Point(266, 6);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(161, 149);
            this.listBox.TabIndex = 7;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // blockSelection
            // 
            this.blockSelection.ActiveControl = this;
            this.blockSelection.BlockDesign = null;
            this.blockSelection.Location = new System.Drawing.Point(8, 173);
            this.blockSelection.Name = "blockSelection";
            this.blockSelection.Selection = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.blockSelection.Size = new System.Drawing.Size(161, 31);
            this.blockSelection.TabIndex = 6;
            this.blockSelection.Text = "方块色";
            this.blockSelection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.blockSelection.UseVisualStyleBackColor = true;
            // 
            // imageButtonEmpty
            // 
            this.imageButtonEmpty.DialogResult = System.Windows.Forms.DialogResult.None;
            this.imageButtonEmpty.Image = global::Tetris.Properties.Resources.empty;
            this.imageButtonEmpty.ImageMargin = new System.Drawing.Size(6, 5);
            this.imageButtonEmpty.Location = new System.Drawing.Point(200, 122);
            this.imageButtonEmpty.Name = "imageButtonEmpty";
            this.imageButtonEmpty.Size = new System.Drawing.Size(36, 33);
            this.imageButtonEmpty.TabIndex = 5;
            this.imageButtonEmpty.Text = "imageButtonEmpty";
            this.imageButtonEmpty.Click += new System.EventHandler(this.imageButtonEmpty_Click);
            // 
            // imageButtonDelete
            // 
            this.imageButtonDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.imageButtonDelete.Image = global::Tetris.Properties.Resources.delete;
            this.imageButtonDelete.ImageMargin = new System.Drawing.Size(6, 5);
            this.imageButtonDelete.Location = new System.Drawing.Point(200, 83);
            this.imageButtonDelete.Name = "imageButtonDelete";
            this.imageButtonDelete.Size = new System.Drawing.Size(36, 33);
            this.imageButtonDelete.TabIndex = 4;
            this.imageButtonDelete.Text = "imageButtonDelete";
            this.imageButtonDelete.Click += new System.EventHandler(this.imageButtonDelete_Click);
            // 
            // imageButtonAdd
            // 
            this.imageButtonAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.imageButtonAdd.Image = global::Tetris.Properties.Resources.add;
            this.imageButtonAdd.ImageMargin = new System.Drawing.Size(6, 5);
            this.imageButtonAdd.Location = new System.Drawing.Point(200, 6);
            this.imageButtonAdd.Name = "imageButtonAdd";
            this.imageButtonAdd.Size = new System.Drawing.Size(36, 33);
            this.imageButtonAdd.TabIndex = 3;
            this.imageButtonAdd.Text = "imageButtonSave";
            this.imageButtonAdd.Click += new System.EventHandler(this.imageButtonAdd_Click);
            // 
            // imageButtonModify
            // 
            this.imageButtonModify.DialogResult = System.Windows.Forms.DialogResult.None;
            this.imageButtonModify.Image = global::Tetris.Properties.Resources.modify;
            this.imageButtonModify.ImageMargin = new System.Drawing.Size(6, 5);
            this.imageButtonModify.Location = new System.Drawing.Point(200, 44);
            this.imageButtonModify.Name = "imageButtonModify";
            this.imageButtonModify.Size = new System.Drawing.Size(36, 33);
            this.imageButtonModify.TabIndex = 1;
            this.imageButtonModify.Text = "imageButtonModify";
            this.imageButtonModify.Click += new System.EventHandler(this.imageButtonModify_Click);
            // 
            // blockDesign
            // 
            this.blockDesign.BackColor = System.Drawing.SystemColors.Window;
            this.blockDesign.BlockIndex = -1;
            this.blockDesign.ColorSelection = System.Drawing.Color.Empty;
            this.blockDesign.Location = new System.Drawing.Point(8, 6);
            this.blockDesign.Name = "blockDesign";
            this.blockDesign.Size = new System.Drawing.Size(161, 161);
            this.blockDesign.Style = "0000000000000000000000000";
            this.blockDesign.TabIndex = 0;
            this.blockDesign.Text = "blockDesign1";
            // 
            // imageButtonSave
            // 
            this.imageButtonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.imageButtonSave.Image = global::Tetris.Properties.Resources.save;
            this.imageButtonSave.ImageMargin = new System.Drawing.Size(6, 6);
            this.imageButtonSave.Location = new System.Drawing.Point(396, 276);
            this.imageButtonSave.Name = "imageButtonSave";
            this.imageButtonSave.Size = new System.Drawing.Size(36, 33);
            this.imageButtonSave.TabIndex = 4;
            this.imageButtonSave.Text = "imageButtonSave";
            this.imageButtonSave.Click += new System.EventHandler(this.imageButtonSave_Click);
            // 
            // TetrisSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(443, 312);
            this.Controls.Add(this.imageButtonSave);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TetrisSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris设置";
            this.tabControl1.ResumeLayout(false);
            this.tabPageCommon.ResumeLayout(false);
            this.tabPageCommon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPixels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHorizontal)).EndInit();
            this.tabPageBlock.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageCommon;
        private System.Windows.Forms.TabPage tabPageBlock;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader cName;
        private System.Windows.Forms.ColumnHeader cShortcut;
        private System.Windows.Forms.Label lblHotKeys;
        private System.Windows.Forms.TextBox txtlHotKeys;
        private ColorSelection backSelection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private BlockDesign blockDesign;
        private System.Windows.Forms.NumericUpDown numericUpDownVertical;
        private System.Windows.Forms.NumericUpDown numericUpDownHorizontal;
        private System.Windows.Forms.NumericUpDown numericUpDownPixels;
        private ImageButton imageButtonModify;
        private ImageButton imageButtonEmpty;
        private ImageButton imageButtonDelete;
        private ImageButton imageButtonAdd;
        private ImageButton imageButtonSave;
        private ColorSelection blockSelection;
        private ListBoxEx listBox;
        private System.Windows.Forms.CheckBox checkBoxManual;
        private System.Windows.Forms.CheckBox checkBoxVoice;
    }
}