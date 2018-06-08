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

			Window_Component.GetAppearanceHandler = GetWindowAppearance_Notepad;
		}

		public void GetWindowAppearance_Notepad()
		{
			StreamReader streamReader = new StreamReader("Appearance_Notepad.txt");

			string currentLine;

			for (int j = 1; j <= Window_Component.Height; j++)
			{
				currentLine = streamReader.ReadLine();

				for (int i = 1; i <= Window_Component.Width; i++)
				{
					VSystem.Display[i, j].Layer[VSystem.Display[i, j].Layer.Count - 1] =
						Convert.ToChar(StringManipulation.Mid(currentLine, i, 1));
				}
			}
		}
	}
}
