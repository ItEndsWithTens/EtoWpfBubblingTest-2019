Testing Eto event bubbling with custom controls in WPF as compared to Windows Forms. Code is a modified version of [this old sample project](https://groups.google.com/d/topic/eto-forms/V8OjDLFJ3e8/discussion).

Information current as of July 14th, 2019, tested with Eto stable 2.4.1 and prerelease 2.5.0-ci-10252.

---

1. Build and run the WinForms project from Visual Studio (really from anything, as long as a debugger is attached). Check the "NodeWindowsFormsHost" checkbox to add an instance of the NodeWindowsFormsHost class to the green PixelLayout. The instance appears as a white square.

2. Click the white square, and your debugger should break in NodeWindowsFormsHost.OnMouseDown. Continue execution, and the as yet unhandled event bubbles up to the node's parent, with the debugger breaking in MainForm.OnMouseDown.

3. Stop debugging, then build and run the Wpf project. The NodeWindowsFormsHost checkbox should already be checked, but if not just click it.

4. Click the white square, and your debugger should break in EtoWpfBubblingTest.Wpf.NodeWindowsFormsHostHandler.WinFormsControl_MouseDown. Continue execution, and the WindowsFormsHostHandler should successfully call the proper override, with your debugger breaking in NodeWindowsFormsHost.OnMouseDown, just like in step 2. But continue execution further and you'll see that the event, which still hasn't been explicitly handled, fails to bubble up to MainForm.OnMouseDown.

5. The GUI should now be running again, so click the NodePanel checkbox. This will remove the white square and replace it with a purple one, an instance of a custom "NodePanel" class which is directly derived from Eto's Panel.

6. Click the purple square, and the debugger will break on NodePanel.OnMouseDown. Continue execution, and the unhandled event bubbles up as expected to MainForm.OnMouseDown.

---

Interestingly, NodeCheckBox fails to bubble on both platforms, suggesting perhaps a control implicitly marking the event handled at some point. But also worth noting is that the Events tab of WPF debugging utility [Snoop](https://github.com/snoopwpf/snoopwpf) shows events when clicking the menu bar, the green PixelLayout, or the cyan and purple squares, but not the white square of NodeWindowsFormsHost. This seems to run counter to the idea of this being caused by the MouseDown event getting prematurely marked handled, since it should still show up in the list.

The first question is then:

1. How does one get a custom class, implemented with the Eto.Wpf.Forms.WindowsFormsHostHandler handler on the Wpf platform, to behave the way NodePanel does here? Both NodePanel and NodeWindowsFormsHost bubble up properly in WinForms, while NodeWindowsFormsHost fails in Wpf.

A second question, not directly represented in the above steps but still demonstrable with this code:

2. Why do KeyDown events skip NodePanel.OnKeyDown (or NodeWindowsFormsHost.OnKeyDown) entirely, despite successfully making it up to MainForm.OnKeyDown? This seems to be common to both WinForms and Wpf; a deeper understanding of Eto's architecture might make the behavior obvious.