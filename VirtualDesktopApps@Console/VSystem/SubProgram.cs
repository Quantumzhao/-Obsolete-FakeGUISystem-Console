using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	public abstract class SubProgram : IKeyEvent
	{
		public int ProgramID { get; set; }

		public Window Window_Component { get; set; }

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

					/* More codes coming this way
					 * All of its components should be set "not selected"
					 */
				}
			}
		}

		public abstract bool ParseAndExecute(ConsoleKeyInfo keyPressed);

		public abstract Pixel[,] GetRenderBuffer();
	}
}
