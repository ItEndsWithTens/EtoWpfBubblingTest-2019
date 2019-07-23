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
