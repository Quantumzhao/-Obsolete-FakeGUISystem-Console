using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	abstract class SubProgram
	{
		public int ProgramID { get; set; }

		public Window Window_Component { get; set; } = new Window();

		public static VSystem.KeyPressDelegate KeyPressHandler;

		public SubProgram()
		{
			
		}
	}
}
