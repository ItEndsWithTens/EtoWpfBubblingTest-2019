using Eto.Drawing;
using Eto.Forms;
using System;

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
            Title = $"EtoWpfBubblingTest - Platform: {Eto.Platform.Instance.ID}";
            ClientSize = new Size(400, 350);

            rdoWindowsFormsHost = new RadioButton { Text = "NodeWindowsFormsHost" };
            rdoPanel = new RadioButton(rdoWindowsFormsHost) { Text = "NodePanel" };
            rdoCheckBox = new RadioButton(rdoWindowsFormsHost) { Text = "NodeCheckBox" };

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

            rdoWindowsFormsHost.Checked = true;
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
    }
}