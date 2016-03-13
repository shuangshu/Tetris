using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Tetris
{
    public partial class TetrisSetting : Form
    {
        private static Setting setting = new Setting();
        public static Setting Setting
        {
            get { return setting; }
        }

        private ListViewItem listViewItem;

        public TetrisSetting()
        {
            InitializeComponent();
            this.LoadSetting();
            this.backSelection.Selection = setting.BackColor;
            this.blockSelection.Selection = SystemColors.Window;
            this.blockSelection.BlockDesign = blockDesign;
            this.listBox.SelectedIndex = 0;
        }
        private void blockSelection_ColorChanged(object sender, Color color)
        {
            this.blockSelection.Selection = color;
        }

        private void LoadSetting()
        {
            setting.Load();

            ListViewItem itemDown = new ListViewItem();
            itemDown.Name = "Down";
            itemDown.Text = "向下";
            itemDown.SubItems.Add(setting.Down.ToString());
            listView.Items.Add(itemDown);

            ListViewItem itemDrop = new ListViewItem();
            itemDrop.Name = "Drop";
            itemDrop.Text = "丢下";
            itemDrop.SubItems.Add(setting.Drop.ToString());
            listView.Items.Add(itemDrop);

            ListViewItem itemMoveL = new ListViewItem();
            itemMoveL.Name = "MoveL";
            itemMoveL.Text = "左移";
            itemMoveL.SubItems.Add(setting.MoveL.ToString());
            listView.Items.Add(itemMoveL);

            ListViewItem itemMoveR = new ListViewItem();
            itemMoveR.Name = "MoveR";
            itemMoveR.Text = "右移";
            itemMoveR.SubItems.Add(setting.MoveR.ToString());
            listView.Items.Add(itemMoveR);

            ListViewItem itemDeasilrotate = new ListViewItem();
            itemDeasilrotate.Name = "Deasilrotate";
            itemDeasilrotate.Text = "顺时针移动";
            itemDeasilrotate.SubItems.Add(setting.Deasilrotate.ToString());
            listView.Items.Add(itemDeasilrotate);

            ListViewItem itemContrarotate = new ListViewItem();
            itemContrarotate.Name = "Contrarotate";
            itemContrarotate.Text = "逆时针移动";
            itemContrarotate.SubItems.Add(setting.Contrarotate.ToString());
            listView.Items.Add(itemContrarotate);

            numericUpDownHorizontal.Value = decimal.Parse(setting.Horizontal.ToString());
            numericUpDownVertical.Value = decimal.Parse(setting.Vertical.ToString());
            numericUpDownPixels.Value = decimal.Parse(setting.Pixels.ToString());

            backSelection.Selection = setting.BackColor;

            checkBoxManual.Checked = setting.ManualOperation;
            checkBoxVoice.Checked = setting.AllowVoice;

            for (int i = 0; i < setting.Lists.Count; i++)
            {
                BlockData data = setting.Lists[i];
                listBox.Items.Add(new ListBoxItem(data.Style, data.Color));
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listView.SelectedItems;
            if (items != null && items.Count > 0)
            {
                listViewItem = items[0];
                txtlHotKeys.Text = listViewItem.SubItems[1].Text;
            }
        }

        private void txtlHotKeys_KeyDown(object sender, KeyEventArgs e)
        {
            listViewItem.SubItems[1].Text = e.KeyCode.ToString();
            txtlHotKeys.Text = listViewItem.SubItems[1].Text;
            if (listViewItem.Name == "Down") setting.Down = e.KeyCode;
            else if (listViewItem.Name == "Drop") setting.Drop = e.KeyCode;
            else if (listViewItem.Name == "MoveL") setting.MoveL = e.KeyCode;
            else if (listViewItem.Name == "MoveR") setting.MoveR = e.KeyCode;
            else if (listViewItem.Name == "Deasilrotate") setting.Deasilrotate = e.KeyCode;
            else if (listViewItem.Name == "Contrarotate") setting.Contrarotate = e.KeyCode;
        }

        private void imageButtonSave_Click(object sender, EventArgs e)
        {
            setting.Lists.Clear();
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                ListBoxItem item = (ListBoxItem)listBox.Items[i];
                setting.Lists.Add(new BlockData(item.Text, item.Color));
            }
            setting.Horizontal = (int)numericUpDownHorizontal.Value;
            setting.Vertical = (int)numericUpDownVertical.Value;
            setting.Pixels = (int)numericUpDownPixels.Value;
            setting.BackColor = backSelection.Selection;
            setting.Save();
        }

        private void imageButtonAdd_Click(object sender, EventArgs e)
        {
            this.listBox.Items.Add(new ListBoxItem(blockDesign.Style, blockDesign.ColorSelection));
        }

        private void imageButtonModify_Click(object sender, EventArgs e)
        {
            if (blockDesign.BlockIndex != -1)
            {
                ListBoxItem item = (ListBoxItem)this.listBox.Items[blockDesign.BlockIndex];
                item.Text = blockDesign.Style;
                item.Color = blockDesign.ColorSelection;
                this.listBox.Invalidate();
            }
        }

        private void imageButtonDelete_Click(object sender, EventArgs e)
        {
            if (blockDesign.BlockIndex != -1)
            {
                this.listBox.Items.RemoveAt(blockDesign.BlockIndex);
                blockDesign.BlockIndex--;
                if (this.listBox.Items.Count > 0) this.listBox.SelectedIndex = 0;
                else this.listBox.SelectedIndex = -1;
            }
            if (this.listBox.SelectedIndex == -1)
            {
                blockDesign.Style = string.Empty;
                blockDesign.ColorSelection = SystemColors.Window;
                blockSelection.Selection = SystemColors.Window;
            }
        }

        private void imageButtonEmpty_Click(object sender, EventArgs e)
        {
            this.listBox.Items.Clear();
            this.listBox.SelectedIndex = -1;
            this.blockDesign.BlockIndex = -1;
            blockDesign.Style = string.Empty;
            blockDesign.ColorSelection = SystemColors.Window;
            blockSelection.Selection = SystemColors.Window;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                ListBoxItem item = (ListBoxItem)listBox.SelectedItem;
                blockDesign.Style = item.Text;
                blockDesign.ColorSelection = item.Color;
                blockDesign.BlockIndex = listBox.SelectedIndex;
                blockSelection.Selection = item.Color;
            }
        }

        private void checkBoxManual_CheckedChanged(object sender, EventArgs e)
        {
            setting.ManualOperation = checkBoxManual.Checked;
        }

        private void checkBoxVoice_CheckedChanged(object sender, EventArgs e)
        {
            setting.AllowVoice = checkBoxVoice.Checked;
        }
    }
}
