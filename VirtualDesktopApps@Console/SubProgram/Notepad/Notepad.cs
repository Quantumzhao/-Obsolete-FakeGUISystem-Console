using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SupplementaryClassLibraryForStringManipulation;

namespace VirtualDesktopApps_Console
{
	public class Notepad : SubProgram
	{
		public Notepad()
		{
			Window_Component = new Window(WindowWidth, WindowHeight);

			Window_Component.InteractiveUnitsCollection.Add(new MenuItem_Edit<Notepad>());
			Window_Component.InteractiveUnitsCollection.Add(new MenuItem_File<Notepad>());
			Window_Component.InteractiveUnitsCollection.Add(new MenuItem_Help<Notepad>());
			Window_Component.InteractiveUnitsCollection.Add(new TextBox               ());

			Window_Component.GetAppearanceHandler = GetWindowAppearance_Notepad;

			Window_Component.InteractiveUnitsCollection[4].IsFocused  = true;
			Window_Component.InteractiveUnitsCollection[4].IsSelected = true;

			InitRenderBuffer();

			((TextBox)Window_Component.InteractiveUnitsCollection[4]).DisplayArea_Component.RenderBufferRef = Window_Component.RenderBuffer;
			((TextBox)Window_Component.InteractiveUnitsCollection[4]).DisplayArea_Component.Pointer_Component.RenderBufferRef = Window_Component.RenderBuffer;

		}

		public const int WindowWidth  = 66;
		public const int WindowHeight = 27;		

		private void InitRenderBuffer()
		{
			using (StreamReader streamReader = new StreamReader("Appearance_Notepad.txt"))
			{
				string currentLine;

				for (int j = 0; j < Window_Component.Height; j++)
				{
					currentLine = streamReader.ReadLine();

					for (int i = 0; i < Window_Component.Width; i++)
					{
						Window_Component.RenderBuffer[i, j] = new Pixel
						{
							DisplayCharacter = Convert.ToChar(StringManipulation.Mid(currentLine, i + 1, 1))
						};
					}
				}
			}
		}

		public void GetWindowAppearance_Notepad()
		{
			for (int j = 0; j < Window_Component.Height; j++)
			{
				for (int i = 0; i < Window_Component.Width; i++)
				{
					VSystem.Layers[ProgramID][i + Window_Component.Anchor.X, j + Window_Component
						.Anchor.Y] = Window_Component.RenderBuffer[i, j];
				}
			}
		}

		public override void ParseAndExecute(ConsoleKeyInfo keyPressed)
		{
			bool isKeyUsed = false;

			switch (Window_Component.GetSelectedComponent().ToString())
			{
				case "VirtualDesktopApps_Console.TextBox":
					isKeyUsed = ((TextBox)Window_Component.InteractiveUnitsCollection[4]).ParseAndExecute(keyPressed);
					break;

				default:
					break;
			}

			if (!isKeyUsed)
			{
				switch (keyPressed.Key)
				{
					default:
						break;
				}
			}
		}
	}
}
