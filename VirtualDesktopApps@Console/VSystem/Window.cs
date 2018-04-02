using System.IO;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;
using SupplementaryClassLibraryForStringManipulation;
using VirtualDesktopApps_Console.Properties;

namespace VirtualDesktopApps_Console
{
	class Window : IEntity
	{
		public int AnchorX { get; set; } = 2;
		public int AnchorY { get; set; } = 2;

		public int Width { get; set; } = 66;
		public int Height { get; set; } = 27;

		public bool IsSelected { get; set; }
		public bool IsFocused { get; set; }

		public Window()
		{
			InteractiveUnitsCollectionClass<TitleBar>.InteractiveUnitsCollection.Add(new TitleBar());
		}

		public void GetAppearance(AvailableProgs program)
		{
			StreamReader streamReader = new StreamReader("Appearance_Notepad.txt");

			string currentLine;

			for (int j = 1; j <= Height; j++)
			{
				currentLine = streamReader.ReadLine();

				for (int i = 1; i <= Width; i++)
				{
					VSystem.Display[i, j].Layer[VSystem.Display[i, j].Layer.Count - 1] = 
						Convert.ToChar(StringManipulation.Mid(currentLine, i, 1));
				}
			}
		}
	}
}
