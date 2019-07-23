using Eto.Forms;

namespace EtoWpfBubblingTest
{
	public partial class MainForm
	{
		public MainForm()
		{
			InitializeComponent();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}

			base.OnKeyDown(e);

			Title = $"KeyDown! Keys: {e.Key}, Modifiers: {e.Modifiers}";

			e.Handled = true;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("MainForm.OnMouseDown");

			base.OnMouseDown(e);

			Title = $"MouseDown! Buttons: {e.Buttons}, Modifiers: {e.Modifiers}, Location: {e.Location}";
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("MainForm.OnMouseUp");

			base.OnMouseUp(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("MainForm.OnMouseMove");

			base.OnMouseMove(e);
		}
	}
}
