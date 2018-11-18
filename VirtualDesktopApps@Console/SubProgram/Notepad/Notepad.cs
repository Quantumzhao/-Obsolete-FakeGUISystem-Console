using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	public class Notepad : SubProgram
	{
		public Notepad()
		{
			Window_Component = new Window(WindowWidth, WindowHeight);

			Window_Component.InteractiveUnitsCollection.Add(new MenuItem_Edit<Notepad>());
			Window_Component.InteractiveUnitsCollection.Add(new MenuItem_File<Notepad>());
			Window_Component.InteractiveUnitsCollection.Add(new MenuItem_Help<Notepad>());
			Window_Component.InteractiveUnitsCollection.Add(new TextBox           (1, 3));

			//Window_Component.GetRenderBufferHandler = GetRenderBuffer;

			Window_Component.InteractiveUnitsCollection[4].IsFocused  = true;
			Window_Component.InteractiveUnitsCollection[4].IsSelected = true;

			InitRenderBuffer();

			//((TextBox)Window_Component.InteractiveUnitsCollection[4]).DisplayArea_Component.RenderBufferRef = Window_Component.RenderBuffer;
			//((TextBox)Window_Component.InteractiveUnitsCollection[4]).DisplayArea_Component.Pointer_Component.RenderBufferRef = Window_Component.RenderBuffer;

		}

		public const int WindowWidth  = 66;
		public const int WindowHeight = 27;		

		private void InitRenderBuffer()
		{
			using (StreamReader streamReader = new StreamReader("Appearance_Notepad.txt"))
			{
				string currentLine;

				for (int j = 0; j < Window_Component.Height; j++)
				{
					currentLine = streamReader.ReadLine();

					for (int i = 0; i < Window_Component.Width; i++)
					{
						Window_Component.RenderBuffer[i, j] = new Pixel
						{
							DisplayCharacter = currentLine.ToCharArray()[i]
						};
					}
				}
			}
		}

		public override Pixel[,] GetRenderBuffer()
		{
			for (int unit = 0; unit < Window_Component.InteractiveUnitsCollection.Count; unit++)
			{
				Button tempUnit = Window_Component.InteractiveUnitsCollection[unit];

				Pixel[,] tempRenderBuffer = tempUnit.GetRenderBuffer();

				for (int j = 0; j < tempRenderBuffer.GetLength(1); j++)
				{
					for (int i = 0; i < tempRenderBuffer.GetLength(0); i++)
					{
						Window_Component.RenderBuffer[tempUnit.Anchor.X + i, tempUnit.Anchor.Y + j]
							= tempRenderBuffer[i, j];
					}
				}
			}
			/*
			for (int j = 0; j < Window_Component.Height; j++)
			{
				for (int i = 0; i < Window_Component.Width; i++)
				{
					VSystem.Layers[ProgramID][i + Window_Component.Anchor.X, j + Window_Component
						.Anchor.Y] = Window_Component.RenderBuffer[i, j];
				}
			}
			*/

			return Window_Component.RenderBuffer;
		}

		public override bool ParseAndExecute(ConsoleKeyInfo keyPressed)
		{
			if (Window_Component.GetSelectedComponent() != null)
			{
					return Window_Component.ParseAndExecute(keyPressed);
			}

			/*
			 * Do something else
			 */

			return false;
		}
	}
}
