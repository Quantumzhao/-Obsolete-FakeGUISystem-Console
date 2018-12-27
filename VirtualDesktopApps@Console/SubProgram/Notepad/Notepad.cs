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
			Windows.Add(new Window(66, 27, "Appearance_Notepad.txt")
								   { Name = "Main"});

			Windows[0].Components.Add(new MenuItem_Edit<Notepad>(), "Edit");
			Windows[0].Components.Add(new MenuItem_File<Notepad>(), "File");
			Windows[0].Components.Add(new MenuItem_Help<Notepad>(), "Help");
			Windows[0].Components.Add(new TextBox(1, 3),         "TextBox");

			Windows[0].IsFocused = Focus.Focused;
			Windows[0].SetRenderBuffer();

			IsComponentSelected = true;

			// This refers to the "TextBox" component ▼
			Windows[0].Components.SetFocusing(4);			
		}

		public override Pixel[,] GetRenderBuffer()
		{
			return Windows[0].GetRenderBuffer();
		}

		public override bool ParseAndExecute(ConsoleKeyInfo keyPressed)
		{
			if (!Windows[0].ParseAndExecute(keyPressed))
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
