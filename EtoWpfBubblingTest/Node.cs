using Eto;
using Eto.Forms;
using System;

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

		bool dragging = false;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("NodeWindowsFormsHost.OnMouseDown");

			dragging = true;

			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("NodeWindowsFormsHost.OnMouseUp");

			dragging = false;

			base.OnMouseUp(e);
		}

		Random random = new Random();
		protected override void OnMouseMove(MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("NodeWindowsFormsHost.OnMouseMove");

			base.OnMouseMove(e);

			if (dragging)
			{
				BackgroundColor = new Eto.Drawing.Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("NodeWindowsFormsHost.OnMouseWheel");

			if (e.Delta.Height > 0)
			{
				Width += 10;
				Height += 10;
			}
			else if (e.Delta.Height < 0)
			{
				Width -= 10;
				Height -= 10;
			}

			base.OnMouseWheel(e);
		}
	}
}
