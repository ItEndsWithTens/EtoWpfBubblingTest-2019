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
			System.Diagnostics.Debug.WriteLine("MainForm.OnKeyDown");

			base.OnKeyDown(e);

			Title = $"KeyDown! Keys: {e.Key}, Modifiers: {e.Modifiers}";

			e.Handled = true;
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("MainForm.OnKeyUp");

			base.OnKeyUp(e);

			Title = $"KeyUp! Keys: {e.Key}, Modifiers: {e.Modifiers}";

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
			//System.Diagnostics.Debug.WriteLine("MainForm.OnMouseMove");

			base.OnMouseMove(e);
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("MainForm.OnMouseWheel");

			base.OnMouseWheel(e);

			Title = $"MouseWheel! Delta: {e.Delta}";
		}

		// As per Microsoft docs, the UIElement.MouseEnter and MouseLeave events
		// in WPF use direct routing, and shouldn't bubble. These two methods
		// will as a result not catch a MouseEnter or Leave from the contained
		// NodeWindowsFormsHost, but they're still useful to demonstrate when a
		// cursor moves from the form to the control, or vice versa.
		//
		// https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.mouseenter?view=netframework-4.8#remarks
		//
		protected override void OnMouseEnter(MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("MainForm.OnMouseEnter");

			base.OnMouseEnter(e);
		}
		protected override void OnMouseLeave(MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("MainForm.OnMouseLeave");

			base.OnMouseLeave(e);
		}
	}
}
