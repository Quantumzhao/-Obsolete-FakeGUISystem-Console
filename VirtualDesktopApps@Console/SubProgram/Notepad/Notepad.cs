using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	class Notepad : SubProgram
	{
		public Window Window_Component { get; set; }

		public Notepad()
		{
			Window_Component.GetAppearance(AvailableProgs.Notepad);
		}
	}
}
