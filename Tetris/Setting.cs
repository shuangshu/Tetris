using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Collections;

namespace Tetris
{
    public class Setting
    {
        private Keys downKey;
        private Keys lKey;
        private Keys rKey;
        private Keys dropKey;
        private Keys deasilrotateKey;
        private Keys contrarotateKey;

        private int horizontal;
        private int vertical;
        private int pixels;
        private Color backColor;
        private List<BlockData> lists;
        private bool manualOperation = false;
        private bool allowVoice = false;

        public Keys Down
        {
            get
            {
                return downKey;
            }
            set
            {
                downKey = value;
            }
        }
        public Keys Drop
        {
            get
            {
                return dropKey;
            }
            set
            {
                dropKey = value;
            }
        }
        public Keys MoveL
        {
            get
            {
                return lKey;
            }
            set
            {
                lKey = value;
            }
        }
        public Keys MoveR
        {
            get
            {
                return rKey;
            }
            set
            {
                rKey = value;
            }
        }
        public Keys Deasilrotate
        {
            get
            {
                return deasilrotateKey;
            }
            set
            {
                deasilrotateKey = value;
            }
        }
        public Keys Contrarotate
        {
            get
            {
                return contrarotateKey;
            }
            set
            {
                contrarotateKey = value;
            }
        }
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }
        public int Horizontal
        {
            get { return horizontal; }
            set { horizontal = value; }
        }
        public int Vertical
        {
            get { return vertical; }
            set { vertical = value; }
        }
        public int Pixels
        {
            get { return pixels; }
            set { pixels = value; }
        }
        public List<BlockData> Lists
        {
            get { return lists; }
        }

        public bool ManualOperation
        {
            get { return manualOperation; }
            set { manualOperation = value; }
        }

        public bool AllowVoice
        {
            get { return allowVoice; }
            set { allowVoice = value; }
        }

        public void Load()
        {
            string element = string.Empty;
            lists = new List<BlockData>();
            XmlTextReader reader = null;
            if (File.Exists("Tetris.xml")) reader = new XmlTextReader("Tetris.xml");
            BlockData blockData = null;
            try
            {
                while (reader.Read())
                {
                    XmlNodeType nodeType = reader.NodeType;
                    if (nodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "Block":
                                blockData = new BlockData();
                                lists.Add(blockData);
                                break;
                            case "Style":
                                {
                                    element = reader.ReadElementString().Trim();
                                    blockData.Style = element;
                                }
                                break;
                            case "Color":
                                {
                                    element = reader.ReadElementString().Trim();
                                    string[] colors = element.Split(',');
                                    blockData.Color = Color.FromArgb(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]));
                                }
                                break;
                            case "DownKey":
                                element = reader.ReadElementString().Trim();
                                Down = (Keys)int.Parse(element);
                                break;
                            case "DropKey":
                                element = reader.ReadElementString().Trim();
                                Drop = (Keys)int.Parse(element);
                                break;
                            case "MoveLKey":
                                element = reader.ReadElementString().Trim();
                                MoveL = (Keys)int.Parse(element);
                                break;
                            case "MoveRKey":
                                element = reader.ReadElementString().Trim();
                                MoveR = (Keys)int.Parse(element);
                                break;
                            case "DeasilrotateKey":
                                element = reader.ReadElementString().Trim();
                                Deasilrotate = (Keys)int.Parse(element);
                                break;
                            case "ContrarotateKey":
                                element = reader.ReadElementString().Trim();
                                Contrarotate = (Keys)int.Parse(element);
                                break;
                            case "Horizontal":
                                element = reader.ReadElementString().Trim();
                                Horizontal = int.Parse(element);
                                break;
                            case "Vertical":
                                element = reader.ReadElementString().Trim();
                                Vertical = int.Parse(element);
                                break;
                            case "Pixels":
                                element = reader.ReadElementString().Trim();
                                pixels = int.Parse(element);
                                break;
                            case "BackColor":
                                {
                                    element = reader.ReadElementString().Trim();
                                    string[] colors = element.Split(',');
                                    backColor = Color.FromArgb(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]));
                                }
                                break;
                            case "ManualOperation":
                                manualOperation = ((element = reader.ReadElementString().Trim()) == "1") ? true : false;
                                break;
                            case "AllowVoice":
                                allowVoice = ((element = reader.ReadElementString().Trim()) == "1") ? true : false;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        public void Save()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Tetris.xml");
            XmlNode root = doc.SelectSingleNode("Setting");
            root.RemoveAll();
            for (int i = 0; i < lists.Count; i++)
            {
                XmlElement xelType = doc.CreateElement("Block");
                XmlElement xelId = doc.CreateElement("Style");
                xelId.InnerText = lists[i].Style;
                xelType.AppendChild(xelId);
                XmlElement xelColor = doc.CreateElement("Color");
                xelColor.InnerText = lists[i].Color.R.ToString() + "," + lists[i].Color.G.ToString() + "," + lists[i].Color.B.ToString();
                xelType.AppendChild(xelColor);
                root.AppendChild(xelType);
            }
            XmlElement xelKey = doc.CreateElement("Key");

            XmlElement xelDownKey = doc.CreateElement("DownKey");
            xelDownKey.InnerText = ((int)Down).ToString();
            xelKey.AppendChild(xelDownKey);

            XmlElement xelDropKey = doc.CreateElement("DropKey");
            xelDropKey.InnerText = ((int)Drop).ToString();
            xelKey.AppendChild(xelDropKey);

            XmlElement xelMoveLeftKey = doc.CreateElement("MoveLKey");
            xelMoveLeftKey.InnerText = ((int)MoveL).ToString();
            xelKey.AppendChild(xelMoveLeftKey);

            XmlElement xelMoveRightKey = doc.CreateElement("MoveRKey");
            xelMoveRightKey.InnerText = ((int)MoveR).ToString();
            xelKey.AppendChild(xelMoveRightKey);

            XmlElement xelDeasilRotateKey = doc.CreateElement("DeasilrotateKey");
            xelDeasilRotateKey.InnerText = ((int)Deasilrotate).ToString();
            xelKey.AppendChild(xelDeasilRotateKey);

            XmlElement xelContraRotateKey = doc.CreateElement("ContrarotateKey");
            xelContraRotateKey.InnerText = ((int)Contrarotate).ToString();
            xelKey.AppendChild(xelContraRotateKey);
            root.AppendChild(xelKey);

            XmlElement xelGraphics = doc.CreateElement("Graphics");
            XmlElement xelHorizontal = doc.CreateElement("Horizontal");
            xelHorizontal.InnerText = Horizontal.ToString();
            xelGraphics.AppendChild(xelHorizontal);

            XmlElement xelVertical = doc.CreateElement("Vertical");
            xelVertical.InnerText = Vertical.ToString();
            xelGraphics.AppendChild(xelVertical);

            XmlElement xelPixels = doc.CreateElement("Pixels");
            xelPixels.InnerText = Pixels.ToString();
            xelGraphics.AppendChild(xelPixels);

            XmlElement xelBackColor = doc.CreateElement("BackColor");
            xelBackColor.InnerText = BackColor.R.ToString() + "," + BackColor.G.ToString() + "," + BackColor.B.ToString();
            xelGraphics.AppendChild(xelBackColor);
            root.AppendChild(xelGraphics);

            XmlElement xelManualOperation = doc.CreateElement("ManualOperation");
            xelManualOperation.InnerText = manualOperation ? "1" : "0";
            root.AppendChild(xelManualOperation);

            XmlElement xelAllowVoice = doc.CreateElement("AllowVoice");
            xelAllowVoice.InnerText = allowVoice ? "1" : "0";
            root.AppendChild(xelAllowVoice);

            doc.Save("Tetris.xml");
        }
    }
}
