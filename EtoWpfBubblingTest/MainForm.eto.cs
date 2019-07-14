using System;
using Eto.Forms;
using Eto.Drawing;

namespace EtoWpfBubblingTest
{
    partial class MainForm : Form
    {
        private RadioButton rdoCheckBox;
        private RadioButton rdoPanel;
        private RadioButton rdoWindowsFormsHost;

        private PixelLayout pxlViewport = new PixelLayout { BackgroundColor = Colors.Green, Size = new Size(100, 100) };

        private NodeCheckBox nodeCheckBox = new NodeCheckBox { Size = new Size(100, 100), BackgroundColor = Colors.Cyan };
        private NodePanel nodePanel = new NodePanel { Size = new Size(100, 100), BackgroundColor = Colors.Purple };
        private NodeWindowsFormsHost nodeWindowsFormsHost = new NodeWindowsFormsHost { Size = new Size(100, 100), BackgroundColor = Colors.White };

        void InitializeComponent()
        {
            Title = "EtoWpfBubblingTest";
            ClientSize = new Size(400, 350);

            rdoCheckBox = new RadioButton { Text = "NodeCheckBox" };
            rdoPanel = new RadioButton(rdoCheckBox) { Text = "NodePanel" };
            rdoWindowsFormsHost = new RadioButton(rdoCheckBox) { Text = "NodeWindowsFormsHost" };

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
                                Items =
                                {
                                    rdoCheckBox,
                                    rdoPanel,
                                    rdoWindowsFormsHost
                                }
                            },
                            pxlViewport
                        }
                    }
                }
            };

            rdoCheckBox.CheckedChanged += ChangeNodeType;
            rdoPanel.CheckedChanged += ChangeNodeType;
            rdoWindowsFormsHost.CheckedChanged += ChangeNodeType;

            pxlViewport.MouseDown += PxlViewport_MouseDown;
        }

        private void ChangeNodeType(object sender, EventArgs e)
        {
            pxlViewport.RemoveAll();

            if (rdoCheckBox.Checked)
            {
                pxlViewport.Add(nodeCheckBox, 0, 0);
            }
            else if (rdoPanel.Checked)
            {
                pxlViewport.Add(nodePanel, 0, 0);
            }
            else if (rdoWindowsFormsHost.Checked)
            {
                pxlViewport.Add(nodeWindowsFormsHost, 0, 0);
            }
        }

        private void PxlViewport_MouseDown(object sender, MouseEventArgs e)
        {
            Title = e.Location.ToString();
        }
    }
}