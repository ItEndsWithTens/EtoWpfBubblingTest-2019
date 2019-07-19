using Eto.Forms;
using System;
using swf = System.Windows.Forms;
using swi = System.Windows.Input;

namespace EtoWpfBubblingTest.Wpf
{
	public class NodeCheckBoxHandler : Eto.Wpf.Forms.Controls.CheckBoxHandler
	{
	}

	public class NodePanelHandler : Eto.Wpf.Forms.Controls.PanelHandler
	{
	}

	public class NodeWindowsFormsHostHandler : Eto.Wpf.Forms.CustomizedWindowsFormsHostHandler<swf.Control, NodeWindowsFormsHost, NodeWindowsFormsHost.ICallback>, NodeWindowsFormsHost.IHandler
	{
		public NodeWindowsFormsHostHandler() : base(new swf.Control())
		{
			Control.KeyDown += Control_KeyDown;
			Control.MouseDown += Control_MouseDown;

			WinFormsControl.KeyDown += WinFormsControl_KeyDown;
			WinFormsControl.MouseDown += WinFormsControl_MouseDown;
		}

		private void Control_KeyDown(object sender, swi.KeyEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}
		}
		private void Control_MouseDown(object sender, swi.MouseButtonEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}
		}

		private void WinFormsControl_KeyDown(object sender, swf.KeyEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}
		}
		private void WinFormsControl_MouseDown(object sender, swf.MouseEventArgs e)
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
