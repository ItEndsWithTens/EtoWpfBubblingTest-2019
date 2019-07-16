using Eto.Forms;
using System;

namespace EtoWpfBubblingTest.WinForms
{
	public class NodeCheckBoxHandler : Eto.WinForms.Forms.Controls.CheckBoxHandler
	{
	}

	public class NodePanelHandler : Eto.WinForms.Forms.Controls.PanelHandler
	{
	}

	public class NodeWindowsFormsHostHandler : Eto.WinForms.Forms.Controls.ControlHandler
	{
	}

	public class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			var platform = new Eto.WinForms.Platform();
			platform.Add<NodeCheckBox.IHandler>(() => new NodeCheckBoxHandler());
			platform.Add<NodePanel.IHandler>(() => new NodePanelHandler());
			platform.Add<NodeWindowsFormsHost.IHandler>(() => new NodeWindowsFormsHostHandler());

			new Application(platform).Run(new MainForm());
		}
	}
}
