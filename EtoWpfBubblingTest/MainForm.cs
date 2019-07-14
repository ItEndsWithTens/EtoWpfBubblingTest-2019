using System;
using System.Linq;
using Eto.Forms;
using Eto.Drawing;

namespace EtoWpfBubblingTest
{
    public partial class MainForm
    {
        public MainForm()
        {
            InitializeComponent();

            btnMove.Click += BtnMoveNode_Click;
        }

        public void BtnMoveNode_Click(object sender, EventArgs e)
        {
            int.TryParse(tbxMoveX.Text, out int desiredX);
            int.TryParse(tbxMoveY.Text, out int desiredY);

            Control node = pxlViewport.Controls.First();

            pxlViewport.Move(node, desiredX, desiredY);

            tbxLocation.Text = node.Location.ToString();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}