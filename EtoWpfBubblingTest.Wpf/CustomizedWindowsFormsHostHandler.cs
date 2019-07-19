using Eto.Drawing;
using Eto.Forms;
using System;
using System.Windows.Forms.Integration;
using swf = System.Windows.Forms;
using swi = System.Windows.Input;

namespace Eto.Wpf.Forms
{
	public static class MyWpfConversions
	{
		public static MouseButtons ToEto(this swi.MouseButton wpfButton)
		{
			MouseButtons etoButton = MouseButtons.None;

			// The System.Windows.Input.MouseButton type isn't a Flags enum, so
			// etoButton doesn't benefit from appending with bitwise OR.
			if (wpfButton == swi.MouseButton.Left)
			{
				etoButton = MouseButtons.Primary;
			}
			else if (wpfButton == swi.MouseButton.Middle)
			{
				etoButton = MouseButtons.Middle;
			}
			else if (wpfButton == swi.MouseButton.Right)
			{
				etoButton = MouseButtons.Alternate;
			}

			return etoButton;
		}

		public static swi.MouseButtonEventArgs ToWpf(this MouseEventArgs e)
		{
			swi.MouseButton button = 0;

			if (e.Buttons.HasFlag(MouseButtons.Primary))
			{
				button |= swi.MouseButton.Left;
			}
			if (e.Buttons.HasFlag(MouseButtons.Middle))
			{
				button |= swi.MouseButton.Middle;
			}
			if (e.Buttons.HasFlag(MouseButtons.Alternate))
			{
				button |= swi.MouseButton.Right;
			}

			return new swi.MouseButtonEventArgs(swi.Mouse.PrimaryDevice, 0, button);
		}

		public static swi.MouseButton? ToWpf(this swf.MouseButtons winformsButton)
		{
			swi.MouseButton? wpfButton;

			// According to the System.Windows.Forms.MouseEventArgs.Button docs,
			// it represents "one of the System.Windows.Forms.MouseButtons
			// values" despite said enum having the Flags attribute. This switch
			// should then be safe, since the input is only ever one button.
			switch (winformsButton)
			{
				case swf.MouseButtons.Left:
					wpfButton = swi.MouseButton.Left;
					break;
				case swf.MouseButtons.None:
					wpfButton = null;
					break;
				case swf.MouseButtons.Right:
					wpfButton = swi.MouseButton.Right;
					break;
				case swf.MouseButtons.Middle:
					wpfButton = swi.MouseButton.Middle;
					break;
				case swf.MouseButtons.XButton1:
					wpfButton = swi.MouseButton.XButton1;
					break;
				case swf.MouseButtons.XButton2:
					wpfButton = swi.MouseButton.XButton2;
					break;
				default:
					throw new ArgumentOutOfRangeException("winformsButton");
			}

			return wpfButton;
		}
	}

	public class CustomizedWindowsFormsHostHandler<TControl, TWidget, TCallback> : WpfFrameworkElement<WindowsFormsHost, TWidget, TCallback>
			where TControl : swf.Control
			where TWidget : Control
			where TCallback : Control.ICallback
	{
		public override bool UseKeyPreview => true;
		public override bool UseMousePreview => false;

		public override Color BackgroundColor
		{
			get { return WinFormsControl.BackColor.ToEto(); }
			set { WinFormsControl.BackColor = value.ToSD(); }
		}

		public TControl WinFormsControl
		{
			get { return (TControl)Control.Child; }
			set { Control.Child = value; }
		}

		public CustomizedWindowsFormsHostHandler(TControl control)
			: this()
		{
			Control.Child = control;
		}

		public CustomizedWindowsFormsHostHandler() : base()
		{
			Control = new WindowsFormsHost();
		}

		public override void AttachEvent(string id)
		{
			switch (id)
			{
				case Eto.Forms.Control.MouseMoveEvent:
					WinFormsControl.MouseMove += WinFormsControl_MouseMove;
					break;
				case Eto.Forms.Control.MouseUpEvent:
					WinFormsControl.MouseUp += WinFormsControl_MouseUp;
					break;
				case Eto.Forms.Control.MouseDownEvent:
					Control.MouseDown += Control_MouseDown;
					WinFormsControl.MouseDown += WinFormsControl_MouseDown;
					break;
				case Eto.Forms.Control.MouseDoubleClickEvent:
					WinFormsControl.MouseDoubleClick += WinFormsControl_MouseDoubleClick;
					break;
				case Eto.Forms.Control.MouseEnterEvent:
					WinFormsControl.MouseEnter += WinFormsControl_MouseEnter;
					break;
				case Eto.Forms.Control.MouseLeaveEvent:
					WinFormsControl.MouseLeave += WinFormsControl_MouseLeave;
					break;
				case Eto.Forms.Control.MouseWheelEvent:
					WinFormsControl.MouseWheel += WinFormsControl_MouseWheel;
					break;
				case Eto.Forms.Control.KeyDownEvent:
					Control.KeyDown += Control_KeyDown;
					WinFormsControl.KeyDown += WinFormsControl_KeyDown;
					WinFormsControl.KeyPress += WinFormsControl_KeyPress;
					break;
				case Eto.Forms.Control.KeyUpEvent:
					WinFormsControl.KeyUp += WinFormsControl_KeyUp;
					break;
				case TextControl.TextChangedEvent:
					WinFormsControl.TextChanged += WinFormsControl_TextChanged;
					break;
				case Eto.Forms.Control.TextInputEvent:
					HandleEvent(Eto.Forms.Control.KeyDownEvent);
					break;
				case Eto.Forms.Control.GotFocusEvent:
					WinFormsControl.GotFocus += WinFormsControl_GotFocus;
					break;
				case Eto.Forms.Control.LostFocusEvent:
					WinFormsControl.LostFocus += WinFormsControl_LostFocus;
					break;
				default:
					base.AttachEvent(id);
					break;
			}
		}

		private void Control_KeyDown(object sender, swi.KeyEventArgs e)
		{
			Keys keys = e.Key.ToEtoWithModifier(e.KeyboardDevice.Modifiers);

			Callback.OnKeyDown(Widget, new KeyEventArgs(keys, KeyEventType.KeyDown));
		}

		Keys key;
		bool handled;
		char keyChar;
		bool charPressed;
		public Keys? LastKeyDown { get; set; }

		void WinFormsControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			charPressed = false;
			handled = true;
			key = e.KeyData.ToEto();

			if (key != Keys.None && LastKeyDown != key)
			{
				var kpea = new KeyEventArgs(key, KeyEventType.KeyDown);
				Callback.OnKeyDown(Widget, kpea);

				handled = e.SuppressKeyPress = e.Handled = kpea.Handled;
			}
			else
				handled = false;

			if (!handled && charPressed)
			{
				// this is when something in the event causes messages to be processed for some reason (e.g. show dialog box)
				// we want the char event to come after the dialog is closed, and handled is set to true!
				var kpea = new KeyEventArgs(key, KeyEventType.KeyDown, keyChar);
				Callback.OnKeyDown(Widget, kpea);
				e.SuppressKeyPress = e.Handled = kpea.Handled;
			}

			LastKeyDown = null;
		}

		void WinFormsControl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			charPressed = true;
			keyChar = e.KeyChar;
			if (!handled)
			{
				if (!char.IsControl(e.KeyChar))
				{
					var tia = new TextInputEventArgs(keyChar.ToString());
					Callback.OnTextInput(Widget, tia);
					e.Handled = tia.Cancel;
				}

				if (!e.Handled)
				{
					var kpea = new KeyEventArgs(key, KeyEventType.KeyDown, keyChar);
					Callback.OnKeyDown(Widget, kpea);
					e.Handled = kpea.Handled;
				}
			}
			else
				e.Handled = true;
		}


		void WinFormsControl_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			key = e.KeyData.ToEto();

			var kpea = new KeyEventArgs(key, KeyEventType.KeyUp);
			Callback.OnKeyUp(Widget, kpea);
			e.Handled = kpea.Handled;
		}

		void WinFormsControl_TextChanged(object sender, EventArgs e)
		{
			var widget = Widget as TextControl;
			if (widget != null)
			{
				var callback = (TextControl.ICallback)((ICallbackSource)widget).Callback;
				callback.OnTextChanged(widget, e);
			}
		}

		void WinFormsControl_MouseWheel(object sender, swf.MouseEventArgs e) => Callback.OnMouseWheel(Widget, e.ToEto(WinFormsControl));

		void WinFormsControl_MouseLeave(object sender, EventArgs e) => Callback.OnMouseLeave(Widget, new MouseEventArgs(Mouse.Buttons, Keyboard.Modifiers, PointFromScreen(Mouse.Position)));

		void WinFormsControl_MouseEnter(object sender, EventArgs e) => Callback.OnMouseEnter(Widget, new MouseEventArgs(Mouse.Buttons, Keyboard.Modifiers, PointFromScreen(Mouse.Position)));

		void WinFormsControl_MouseDoubleClick(object sender, swf.MouseEventArgs e) => Callback.OnMouseDoubleClick(Widget, e.ToEto(WinFormsControl));

		void Control_MouseDown(object sender, swi.MouseButtonEventArgs e) => Callback.OnMouseDown(Widget, e.ToEto(Control));
		void WinFormsControl_MouseDown(object sender, swf.MouseEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				System.Diagnostics.Debugger.Break();
			}

			Control.CaptureMouse();

			MouseEventArgs eto = e.ToEto(WinFormsControl);

			swi.MouseButton changed = eto.ToWpf().ChangedButton;
			var args = new swi.MouseButtonEventArgs(swi.InputManager.Current.PrimaryMouseDevice, Environment.TickCount, changed)
			{
				RoutedEvent = swi.Mouse.MouseDownEvent,
				Source = Control
			};

			Control.RaiseEvent(args);
		}

		void WinFormsControl_MouseUp(object sender, swf.MouseEventArgs e) => Callback.OnMouseUp(Widget, e.ToEto(WinFormsControl));

		void WinFormsControl_MouseMove(object sender, swf.MouseEventArgs e) => Callback.OnMouseMove(Widget, e.ToEto(WinFormsControl));

		void WinFormsControl_LostFocus(object sender, EventArgs e) => Callback.OnLostFocus(Widget, EventArgs.Empty);

		void WinFormsControl_GotFocus(object sender, EventArgs e) => Callback.OnGotFocus(Widget, EventArgs.Empty);

		public override void Focus()
		{
			if (Widget.Loaded && WinFormsControl.IsHandleCreated)
				WinFormsControl.Focus();
			else
				Widget.LoadComplete += Widget_LoadComplete;
		}

		void Widget_LoadComplete(object sender, EventArgs e)
		{
			Widget.LoadComplete -= Widget_LoadComplete;
			WinFormsControl.Focus();
		}

		public override bool HasFocus => WinFormsControl.Focused;

		public override bool AllowDrop
		{
			get { return WinFormsControl.AllowDrop; }
			set { WinFormsControl.AllowDrop = value; }
		}

		public override void SuspendLayout()
		{
			base.SuspendLayout();
			WinFormsControl.SuspendLayout();
		}

		public override void ResumeLayout()
		{
			base.ResumeLayout();
			WinFormsControl.ResumeLayout();
		}

		public override void Invalidate(bool invalidateChildren)
		{
			WinFormsControl.Invalidate(invalidateChildren);
		}

		public override void Invalidate(Rectangle rect, bool invalidateChildren)
		{
			WinFormsControl.Invalidate(rect.ToSD(), invalidateChildren);
		}

		public override bool Enabled
		{
			get { return WinFormsControl.Enabled; }
			set { WinFormsControl.Enabled = value; }
		}
	}
}
