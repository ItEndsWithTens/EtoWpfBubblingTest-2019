using Eto;
using Eto.Forms;

namespace EtoWpfBubblingTest
{
	[Handler(typeof(NodeCheckBox.IHandler))]
	public class NodeCheckBox : CheckBox
	{
		public new IHandler Handler => (IHandler)base.Handler;

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}

			base.OnKeyDown(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}

			base.OnMouseDown(e);
		}
	}

	[Handler(typeof(NodePanel.IHandler))]
	public class NodePanel : Panel
	{
		public new IHandler Handler => (IHandler)base.Handler;

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}

			base.OnKeyDown(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}

			base.OnMouseDown(e);
		}
	}

	[Handler(typeof(NodeWindowsFormsHost.IHandler))]
	public class NodeWindowsFormsHost : Control
	{
		public new IHandler Handler => (IHandler)base.Handler;

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}

			base.OnKeyDown(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}

			base.OnMouseDown(e);
		}
	}
}
