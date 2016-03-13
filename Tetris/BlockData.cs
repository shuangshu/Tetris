using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Diagnostics;

namespace Tetris
{
    public class BlockData
    {
        private string style = string.Empty;
        private Color color = Color.Empty;
        private Point[] points = null;

        public BlockData()
        {
        }

        public BlockData(string style)
        {
            this.style = style;
        }

        public BlockData(string style, Color color)
            : this(style)
        {
            this.color = color;
        }

        public string Style
        {
            get { return style; }
            set { style = value; }
        }
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        public Point[] Points
        {
            get
            {
                if (points == null)
                {
                    this.points = this.ToPoints;
                }
                return points;
            }
        }

        private Point[] ToPoints
        {
            get
            {
                if (string.IsNullOrEmpty(style)) return null;
                int oneNumber = 0;
                for (int a = 0; a < style.Length; a++)
                {
                    if (style[a] == '1') oneNumber++;
                }
                Point[] points = new Point[oneNumber];
                oneNumber = 0;
                int[,] array = new int[5, 5];
                for (int b = 0; b < 5; b++)
                {
                    for (int a = 0; a < 5; a++)
                    {
                        if (style[a + 5 * b] == '1')
                        {
                            points[oneNumber].X = a - 2;//左移2个坐标
                            points[oneNumber].Y = 2 - b;//右移2个坐标
                            oneNumber++;
                        }
                    }
                }
                return points;
            }
        }

        public BlockData Clone()
        {
            BlockData data = new BlockData();
            data.style = style;
            data.points = data.ToPoints;
            data.color = color;
            return data;
        }
    }
}
