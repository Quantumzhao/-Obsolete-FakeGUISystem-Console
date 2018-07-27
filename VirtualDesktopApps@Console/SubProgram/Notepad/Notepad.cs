using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SupplementaryClassLibraryForStringManipulation;

namespace VirtualDesktopApps_Console
{
	class Notepad : SubProgram
	{
		public Notepad()
		{
			Window_Component.InteractiveUnitsCollection.Add(new MenuItem_Edit<Notepad>());
			Window_Component.InteractiveUnitsCollection.Add(new MenuItem_File<Notepad>());
			Window_Component.InteractiveUnitsCollection.Add(new MenuItem_Help<Notepad>());
			Window_Component.InteractiveUnitsCollection.Add(new TextBox               ());

			Window_Component.GetAppearanceHandler = GetWindowAppearance_Notepad;

			Window_Component.InteractiveUnitsCollection[4].IsFocused  = true;
			Window_Component.InteractiveUnitsCollection[4].IsSelected = true;
		}

		public void GetWindowAppearance_Notepad()
		{
			using (StreamReader streamReader = new StreamReader("Appearance_Notepad.txt"))
			{
				string currentLine;

				for (int j = 1; j <= Window_Component.Height; j++)
				{
					currentLine = streamReader.ReadLine();

					for (int i = 1; i <= Window_Component.Width; i++)
					{
						VSystem.Display[i - 1 + Window_Component.Anchor.X, j - 1 + 
							Window_Component.Anchor.Y].Layer[VSystem.Display[i - 1 + 
							Window_Component.Anchor.X, j - 1 + Window_Component.Anchor.Y].
							Layer.Count - 1] = Convert.ToChar(StringManipulation.Mid(currentLine, i, 1));
					}
				}
			}

			((TextBox)Window_Component.InteractiveUnitsCollection[4]).GetAppearance();
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
				switch (keyPressed)
				{
					default:
						break;
				}
			}
		}
	}
}
