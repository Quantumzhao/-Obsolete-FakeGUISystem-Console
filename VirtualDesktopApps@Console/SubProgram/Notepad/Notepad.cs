using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	class Notepad : SubProgram
	{
		public Notepad()
		{
			InteractiveUnitsCollectionClass<MenuItem_Edit>.InteractiveUnitsCollection.Add(new MenuItem_Edit());
			InteractiveUnitsCollectionClass<MenuItem_File>.InteractiveUnitsCollection.Add(new MenuItem_File());
			InteractiveUnitsCollectionClass<MenuItem_Help>.InteractiveUnitsCollection.Add(new MenuItem_Help());
		}
	}
}
