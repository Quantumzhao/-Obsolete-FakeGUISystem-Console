using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	class Window
	{
		public int AnchorX { get; set; } = 2;
		public int AnchorY { get; set; } = 2;

		public int Width { get; set; } = 67;
		public int Height { get; set; } = 27;

		public Window()
		{
			InteractiveUnitsCollection<TitleBar>.AddNewComponent(new TitleBar());
		}

		public void GetAppearance(AvailableProgs program)
		{

		}
	}
}
