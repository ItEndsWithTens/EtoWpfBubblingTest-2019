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
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}

			base.OnMouseDown(e);

			Title = $"MouseDown! Buttons: {e.Buttons}, Modifiers: {e.Modifiers}, Location: {e.Location}";

			e.Handled = true;
		}
	}
}
