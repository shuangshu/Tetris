using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Tetris
{
    public class Block
    {
        private BlockData blockData;
        public BlockData BlockData
        {
            get { return blockData; }
        }

        private Setting blockSetting = null;

        public Block(Setting setting)
        {
            blockSetting = setting;
        }

        public void GenerateBlockData()
        {
            Random random = new Random();
            blockData = blockSetting.Lists[random.Next(0, blockSetting.Lists.Count)];//产生随机的方块
        }

        public Point this[int index]
        {
            get { return blockData.Points[index]; }
        }

        public int Length
        {
            get
            {
                return blockData.Points.Length;
            }
        }
        //水平偏移
        private int x = 0;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        //垂直偏移
        private int y = 0;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Rectangle PointToRectangle(Point p)
        {
            return new Rectangle((X + p.X) * blockSetting.Pixels + 1, (Y - p.Y) * blockSetting.Pixels + 1, blockSetting.Pixels - 2, blockSetting.Pixels - 2);
        }

        public Rectangle PointToRectangle(Point p, int pixels)
        {
            return new Rectangle((X + p.X) * pixels + 1, (Y - p.Y) * pixels + 1, pixels - 2, pixels - 2);
        }

        public void DeasilRotate()
        {
            int value = 0;
            for (int i = 0; i < blockData.Points.Length; i++)
            {
                value = blockData.Points[i].X;
                blockData.Points[i].X = blockData.Points[i].Y;
                blockData.Points[i].Y = -value;
            }
        }

        public void ContraRotate()
        {
            int value = 0;
            for (int i = 0; i < blockData.Points.Length; i++)
            {
                value = blockData.Points[i].X;
                blockData.Points[i].X = -blockData.Points[i].Y;
                blockData.Points[i].Y = value;
            }
        }

        public Block Clone()
        {
            Block block = new Block(this.blockSetting);
            block.blockData = blockData.Clone();
            return block;
        }
    }
}
