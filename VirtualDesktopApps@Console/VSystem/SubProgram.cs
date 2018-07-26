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

		private bool isComponentSelected = false;
		public bool IsComponentSelected
		{
			get
			{
				return isComponentSelected;
			}

			set
			{
				if (value == true)
				{
					VSystem.IsFocused = false;

					if (VSystem.GetFocusedSubProgram() != null)
					{
						VSystem.GetFocusedSubProgram().IsComponentSelected = false;
					}
					isComponentSelected = true;
				}
				else
				{
					isComponentSelected = false;
				}
			}
		}

		public abstract void ParseAndExecute(ConsoleKeyInfo keyPressed);
	}
}
