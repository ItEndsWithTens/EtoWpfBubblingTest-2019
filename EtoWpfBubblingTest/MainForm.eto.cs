using System;
using Eto.Forms;
using Eto.Drawing;

namespace EtoWpfBubblingTest
{
    partial class MainForm : Form
    {
        private Button btnMove = new Button { Text = "Move node" };
        private TextBox tbxMoveX = new TextBox();
        private TextBox tbxMoveY = new TextBox();
        private Label lblLocation = new Label { Text = "Node location:", VerticalAlignment = VerticalAlignment.Bottom, Height = 25 };
        private TextBox tbxLocation = new TextBox();
        private PixelLayout pxlViewport = new PixelLayout { BackgroundColor = Colors.Green, Size = new Size(100, 100) };

        private NodeCheckBox nodeCheckBox = new NodeCheckBox { Size = new Size(100, 100), BackgroundColor = Colors.Cyan };
        private NodePanel nodePanel = new NodePanel { Size = new Size(100, 100), BackgroundColor = Colors.Purple };
        private NodeWindowsFormsHost nodeWindowsFormsHost = new NodeWindowsFormsHost { Size = new Size(100, 100), BackgroundColor = Colors.White };

        void InitializeComponent()
        {
            Title = "EtoWpfBubblingTest";
            ClientSize = new Size(400, 350);

            Content = new TableLayout()
            {
                BackgroundColor = Colors.Red,
                Rows = 
                {
                    new TableRow()
                    {
                        Cells =
                        {
                            new StackLayout
                            {
                                Width = 137,
                                Items =
                                {
                                    btnMove,
                                    tbxMoveX,
                                    tbxMoveY,
                                    lblLocation,
                                    tbxLocation
                                }
                            },
                            pxlViewport
                        }
                    }
                }
            };

            //pxlViewport.Add(nodeCheckBox, 0, 0);
            //pxlViewport.Add(nodePanel, 0, 0);
            pxlViewport.Add(nodeWindowsFormsHost, 0, 0);

            pxlViewport.MouseDown += PxlViewport_MouseDown;
        }

        private void PxlViewport_MouseDown(object sender, MouseEventArgs e)
        {
            Title = e.Location.ToString();
        }
    }
}