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

			Window_Component.AddComponent(new MenuItem_Edit<Notepad>(), "Edit");
			Window_Component.AddComponent(new MenuItem_File<Notepad>(), "File");
			Window_Component.AddComponent(new MenuItem_Help<Notepad>(), "Help");
			Window_Component.AddComponent(new TextBox(1, 3),         "TextBox");

			TextBox textBox = (TextBox)Window_Component.GetComponent("TextBox");

			textBox.IsFocused  = true;
			textBox.IsHighlighted = true;
			Window_Component.IsHighlighted = true;
		}

		public override Pixel[,] GetRenderBuffer()
		{
			return Window_Component.GetRenderBuffer();
		}

		public override bool ParseAndExecute(ConsoleKeyInfo keyPressed)
		{
			if (!Window_Component.ParseAndExecute(keyPressed))
			{
				return false;
			}

			/*
			 * Do something else
			 */

			return false;
		}
	}
}
