using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Windows;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Windows.SystemControl;
using System.Media;

namespace Tetris
{
    public partial class TetrisMain : Form
    {
        private Rebar rebar = null;
        private RebarBand band = null;
        private ToolBox toolBox = null;
        private ToolBoxButton buttonBegin = null;
        private ToolBoxButton buttonPause = null;
        private ToolBoxButton buttonDown = null;
        private ToolBoxButton buttonL = null;
        private ToolBoxButton buttonR = null;
        private ToolBoxButton buttonContra = null;
        private ToolBoxButton buttonDeasil = null;
        private ToolBoxButton buttonDrop = null;
        private ToolBoxButton buttonSetting = null;
        private TetrisSetting setting = null;
        private ImageList imageList = null;

        private Block currentBlock = null;
        private Block previewBlock = null;

        private bool stopping = false;

        public TetrisMain()
        {
            InitializeComponent();
            this.LoadControls();
        }

        private void LoadControls()
        {
            setting = new TetrisSetting();//设置对话框
            imageList = new ImageList();
            imageList.ImageSize = new Size(32, 32);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.Images.Add(Properties.Resources.start);
            imageList.Images.Add(Properties.Resources.stop);
            imageList.Images.Add(Properties.Resources.down);
            imageList.Images.Add(Properties.Resources.moveL);
            imageList.Images.Add(Properties.Resources.moveR);
            imageList.Images.Add(Properties.Resources.deasil);
            imageList.Images.Add(Properties.Resources.contra);
            imageList.Images.Add(Properties.Resources.drop);
            imageList.Images.Add(Properties.Resources.setting);

            buttonBegin = new ToolBoxButton(0);
            buttonBegin.MenuText = "开始";
            buttonPause = new ToolBoxButton(1);
            buttonPause.MenuText = "暂停";
            buttonDown = new ToolBoxButton(2);
            buttonDown.MenuText = "向下";
            buttonL = new ToolBoxButton(3);
            buttonL.MenuText = "向左";
            buttonR = new ToolBoxButton(4);
            buttonR.MenuText = "向右";
            buttonContra = new ToolBoxButton(5);
            buttonContra.MenuText = "逆时针";
            buttonDeasil = new ToolBoxButton(6);
            buttonDeasil.MenuText = "顺时针";
            buttonDrop = new ToolBoxButton(7);
            buttonDrop.MenuText = "丢下";
            buttonSetting = new ToolBoxButton(8);
            buttonSetting.MenuText = "设置";

            buttonPause.Enable = false;
            buttonDrop.Enable = false;
            buttonDeasil.Enable = false;
            buttonContra.Enable = false;
            buttonR.Enable = false;
            buttonL.Enable = false;
            buttonDown.Enable = false;

            toolBox = new ToolBox();
            toolBox.ImageList = imageList;
            toolBox.ButtonClick += new TToolBarButtonClickEventHandle(toolBox_ButtonClick);
            this.Controls.Add(toolBox);
            toolBox.Buttons.Add(buttonBegin);
            toolBox.Buttons.Add(buttonPause);
            toolBox.Buttons.Add(buttonDown);
            toolBox.Buttons.Add(buttonL);
            toolBox.Buttons.Add(buttonR);
            toolBox.Buttons.Add(buttonContra);
            toolBox.Buttons.Add(buttonDeasil);
            toolBox.Buttons.Add(buttonDrop);
            toolBox.Buttons.Add(buttonSetting);
            toolBox.AcceptMessageWindow = this;

            rebar = new Rebar();
            rebar.Dock = DockStyle.Top;
            rebar.Height = 40;
            rebar.MenuItemClick += new MenuItemEventHandler(rebar_MenuItemClick);
            band = new RebarBand("俄罗斯方块 Version 1.0", toolBox);
            rebar.Bands.Add(band);
            this.Controls.Add(rebar);

            this.workPalette.SizeChanged += new EventHandler(workPalette_SizeChanged);
            this.workPalette.LoadSetting(TetrisSetting.Setting);
            this.workPalette.PreviewPalette = this.previewPalette;
        }

        private void rebar_MenuItemClick(object sender, ToolBoxButton button)
        {
            if (button != null)
            {
                if (button == buttonSetting)
                {
                    this.workPalette.EndLoop();
                    if (setting.ShowDialog() == DialogResult.OK)
                    {
                        this.workPalette.LoadSetting(TetrisSetting.Setting);
                        this.workPalette.CreatePalette();
                        this.workPalette.Invalidate();
                        Bengin();
                    }
                }
            }
        }

        private void workPalette_SizeChanged(object sender, EventArgs e)
        {
            Point p = workPalette.Location;
            this.groupBoxPreview.Location = new System.Drawing.Point(p.X + workPalette.Size.Width + 25, p.Y);
            this.Width = workPalette.Size.Width + this.groupBoxPreview.Width + 60;
            this.Height = rebar.Height + workPalette.Height + 50;
        }

        private void toolBox_ButtonClick(object sender, ToolBoxButtonClickEventArgs e)
        {
            if (e.Button == buttonSetting)
            {
                if (setting.ShowDialog() == DialogResult.OK)
                {
                    this.workPalette.EndLoop();
                    this.workPalette.LoadSetting(TetrisSetting.Setting);
                    this.workPalette.CreatePalette();
                    this.workPalette.Invalidate();
                    Bengin();
                }
            }
            else if (e.Button == buttonPause)
            {
                if (!stopping)
                {
                    stopping = true;
                    this.workPalette.EndLoop();
                }
                else
                {
                    stopping = false;
                    this.workPalette.BeginLoop();
                }
            }
            else if (e.Button == buttonBegin)
            {
                Bengin();
                e.Button.Enable = false;
            }
            else if (e.Button == buttonDown)
            {
                workPalette.MoveDown();
            }
            else if (e.Button == buttonL)
            {
                workPalette.MoveL();
            }
            else if (e.Button == buttonR)
            {
                workPalette.MoveR();
            }
            else if (e.Button == buttonDeasil)
            {
                workPalette.MoveDeasilRotate();
            }
            else if (e.Button == buttonContra)
            {
                workPalette.MoveContraRotate();
            }
            else if (e.Button == buttonDrop)
            {
                this.workPalette.EndLoop();
                while (workPalette.MoveDown()) ;
                this.workPalette.BeginLoop();
            }
        }

        private void Bengin()
        {
            if (TetrisSetting.Setting.ManualOperation)
            {
                buttonPause.Enable = true;
                buttonDrop.Enable = true;
                buttonDeasil.Enable = true;
                buttonContra.Enable = true;
                buttonR.Enable = true;
                buttonL.Enable = true;
                buttonDown.Enable = true;
            }
            else
            {
                buttonPause.Enable = true;
                buttonDrop.Enable = false;
                buttonDeasil.Enable = false;
                buttonContra.Enable = false;
                buttonR.Enable = false;
                buttonL.Enable = false;
                buttonDown.Enable = false;
            }

            previewBlock = new Block(TetrisSetting.Setting);
            previewBlock.GenerateBlockData();
            previewBlock.X = 2;
            previewBlock.Y = 2;
            previewPalette.Clean();
            previewPalette.PreviewBlock = previewBlock;
            previewPalette.Draw(previewBlock.BlockData.Color, 24);

            currentBlock = previewBlock.Clone();
            workPalette.WorkBlock = currentBlock;
            workPalette.WorkBlock.X = TetrisSetting.Setting.Horizontal / 2;
            int Y = -2;
            for (int i = 0; i < workPalette.WorkBlock.Length; i++)
            {
                if (workPalette.WorkBlock[i].Y > Y)
                {
                    Y = workPalette.WorkBlock[i].Y;
                }
            }
            workPalette.WorkBlock.Y = Y;
            workPalette.Clean(TetrisSetting.Setting.BackColor);
            workPalette.Draw(currentBlock.BlockData.Color, workPalette.ClientRectangle, false);
            this.workPalette.BeginLoop();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyValue == 32)//屏蔽回车键
            {
                e.Handled = true;
            }
            else if (e.KeyValue == (int)TetrisSetting.Setting.Down)
            {
                workPalette.MoveDown();
            }
            else if (e.KeyValue == (int)TetrisSetting.Setting.Drop)
            {
                this.workPalette.EndLoop();
                while (workPalette.MoveDown()) ;
                this.workPalette.BeginLoop();
            }
            else if (e.KeyValue == (int)TetrisSetting.Setting.MoveL)
            {
                workPalette.MoveL();
            }
            else if (e.KeyValue == (int)TetrisSetting.Setting.MoveR)
            {
                workPalette.MoveR();
            }
            else if (e.KeyValue == (int)TetrisSetting.Setting.Deasilrotate)
            {
                workPalette.MoveDeasilRotate();
            }
            else if (e.KeyValue == (int)TetrisSetting.Setting.Contrarotate)
            {
                workPalette.MoveContraRotate();
            }
            base.OnKeyDown(e);
        }
    }
}
