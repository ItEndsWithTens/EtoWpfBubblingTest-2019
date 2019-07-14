using Eto.Forms;
using System;

namespace EtoWpfBubblingTest.Wpf
{
    public class NodeCheckBoxHandler : Eto.Wpf.Forms.Controls.CheckBoxHandler
    {
    }

    public class NodePanelHandler : Eto.Wpf.Forms.Controls.PanelHandler
    {
    }

    public class NodeWindowsFormsHostHandler : Eto.Wpf.Forms.WindowsFormsHostHandler<System.Windows.Forms.Control, NodeWindowsFormsHost, NodeWindowsFormsHost.ICallback>, NodeWindowsFormsHost.IHandler
    {
        public NodeWindowsFormsHostHandler() : base(new System.Windows.Forms.Control())
        {
            Control.KeyDown += Control_KeyDown;
            Control.MouseDown += Control_MouseDown;

            WinFormsControl.KeyDown += WinFormsControl_KeyDown;
            WinFormsControl.MouseDown += WinFormsControl_MouseDown;
        }

        private void Control_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
        private void Control_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        private void WinFormsControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
        private void WinFormsControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

    }

    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var platform = new Eto.Wpf.Platform();
            platform.Add<NodeCheckBox.IHandler>(() => new NodeCheckBoxHandler());
            platform.Add<NodePanel.IHandler>(() => new NodePanelHandler());
            platform.Add<NodeWindowsFormsHost.IHandler>(() => new NodeWindowsFormsHostHandler());

            new Application(platform).Run(new MainForm());
        }
    }
}
