using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplementaryClassLibraryForStringManipulation;

namespace VirtualDesktopApps_Console
{
	abstract class Button : IEntity
	{
		public int AnchorX { get; set; }
		public int AnchorY { get; set; }

		public int Width { get; set; }
		public int Height { get; set; }

		public bool IsSelected { get; set; }
		private bool isFocused;
		public bool IsFocused
		{
			get
			{
				return isFocused;
			}
			set
			{
				if (isFocused == value)
				{
					return;
				}
				else
				{
					isFocused = value;
					GetAppearance(value);
				}
			}
		}

		public abstract void GetAppearance(bool isFocused);
	}

	class PopUpMenu : IEntity
	{
		public int AnchorX { get; set; }
		public int AnchorY { get; set; }

		public int Width { get; set; } = 15;
		public int Height { get; set; } = 7;

		public bool IsSelected { get; set; }
		public bool IsFocused { get; set; }
		public bool IsComponentFocused { get; set; }

		public bool IsVisible { get; set; }

		public PopUpMenu()
		{
			/*--For Test Only--*/
			AnchorX = 2;
			AnchorY = 2;
			
		}

		public void GetAppearance()
		{
			for (int j = 1; j <= Height; j++)
			{

			}

			VSystem.Display[2, 2].Layer[VSystem.Display[2, 2].Layer.Count - 1] = 'a';
		}
	}

	class TitleBar : Button
	{
		public override void GetAppearance(bool isFocused)
		{

		}
	}

	class MenuItem : Button
	{
		public override void GetAppearance(bool isFocused)
		{

		}
	}

	class MenuItem_File<T> : MenuItem where T : SubProgram
	{
		public void PopUpMenu(T subProgram)
		{
			
		}

		public override void GetAppearance(bool isFocused)
		{

		}
	}

	class MenuItem_Edit<T> : MenuItem where T : SubProgram
	{
		public void FocusShiftToTextBox(T subProgram)
		{

		}
	}

	class MenuItem_Help<T> : MenuItem where T : SubProgram
	{

	}

	class TextBox : Button
	{
		private string content = "";
		
		public string ReadContent()
		{
			return content;
		}

		public void AppendContent(char input)
		{
			content += input.ToString();
		}

		public void DeleteContent()
		{
			content = StringManipulation.Mid(content, 1, (int)StringManipulation.Len(content) - 1);
		}

		public void DeleteAll()
		{
			content = "";
		}

		public void Select(int start, int end)
		{

		}

		public void SelectAll()
		{

		}

		public void MoveCursor()
		{

		}

		public override void GetAppearance(bool isFocused)
		{

		}
	}

	class Pointer
	{

	}

	class PopUpMenu_Files : PopUpMenu
	{

	}
}
