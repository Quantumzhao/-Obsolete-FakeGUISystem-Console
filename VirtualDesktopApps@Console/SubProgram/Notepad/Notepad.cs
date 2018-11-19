using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	public class Notepad : SubProgram
	{
		public const int WindowWidth  = 66;
		public const int WindowHeight = 27;

		public Notepad()
		{
			Window_Component = new Window(WindowWidth, WindowHeight, "Appearance_Notepad.txt")
								   { Name = "Main"};

			Window_Component.AddComponent(new MenuItem_Edit<Notepad> { Name = "Edit"});
			Window_Component.AddComponent(new MenuItem_File<Notepad> { Name = "File"});
			Window_Component.AddComponent(new MenuItem_Help<Notepad> { Name = "Help"});
			Window_Component.AddComponent(new TextBox(1, 3)       { Name = "TextBox"});

			Window_Component.GetComponent("TextBox").IsFocused  = true;
			Window_Component.GetComponent("TextBox").IsSelected = true;
		}

		public override Pixel[,] GetRenderBuffer()
		{
			return Window_Component.RenderBuffer;
		}

		public override bool ParseAndExecute(ConsoleKeyInfo keyPressed)
		{
			if (Window_Component.GetSelectedComponent() != null)
			{
					return Window_Component.ParseAndExecute(keyPressed);
			}

			/*
			 * Do something else
			 */

			return false;
		}
	}
}
