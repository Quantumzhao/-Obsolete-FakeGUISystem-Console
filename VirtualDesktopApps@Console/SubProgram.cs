using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	class SubProgram
	{
		public int AnchorX { get; set; } 
		public int AnchorY { get; set; }

		public int Width  { get; set; }
		public int Height { get; set; }
	}

	class Notepad : SubProgram
	{
		public void InitWindow()
		{
			//Launcher.Display[AnchorX, AnchorY].Layer[]
		}

		public Notepad()
		{
			
		}
	}
}
