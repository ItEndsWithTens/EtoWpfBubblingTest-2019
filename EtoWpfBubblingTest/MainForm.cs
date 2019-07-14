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