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
		public List<Button> InteractiveUnitsCollection { get; set; } = new List<Button>();

		public delegate void GetAppearanceDelegate();
		public GetAppearanceDelegate GetAppearanceHandler;

		public Window()
		{
			InteractiveUnitsCollection.Add(new TitleBar());
		}

		public void GetAppearance(AvailableProgs program)
		{
			GetAppearanceHandler();

			GetAppearanceHandler = null;
		}
	}
}
