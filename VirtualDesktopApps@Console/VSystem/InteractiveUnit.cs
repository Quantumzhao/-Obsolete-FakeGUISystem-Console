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
		public Coordinates Anchor { get; set; } = new Coordinates();

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
					GetAppearance();
				}
			}
		}

		public abstract void GetAppearance();
	}

	class PopUpMenu : IEntity
	{
		public Coordinates Anchor { get; set; } = new Coordinates();

		public int Width { get; set; } = 15;
		public int Height { get; set; } = 7;

		public bool IsSelected { get; set; }
		public bool IsFocused { get; set; }
		public bool IsComponentFocused { get; set; }

		public bool IsVisible { get; set; }

		public PopUpMenu()
		{
			/*--For Test Only--*/
			Anchor.X = 2;
			Anchor.Y = 2;
			
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
		public override void GetAppearance()
		{

		}
	}

	class MenuItem : Button
	{
		public override void GetAppearance()
		{

		}
	}

	class MenuItem_File<T> : MenuItem where T : SubProgram
	{
		public void PopUpMenu(T subProgram)
		{
			
		}

		public override void GetAppearance()
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
		public TextBox()
		{
			Anchor.X = 2;
			Anchor.Y = 4;

			content.Add("123456");

			DisplayArea_Component.TextboxContentClone = content;
		}

		public TextboxDisplayArea DisplayArea_Component { get; set; } = new TextboxDisplayArea();

		private List<string> content = new List<string>();

		public List<string> ReadContent()
		{
			return content;
		}

		public void InsertContent(char input, int line, int position)
		{
			try
			{
				content[line] = content[line].Insert(position, input.ToString());
			}
			catch(ArgumentOutOfRangeException) { }			
		}

		public void DeleteContent(int line, int position)
		{
			try
			{
				content[line] = content[line].Remove(position - 1, 1);
			}
			catch (ArgumentOutOfRangeException) { }
		}

		public void DeleteAll()
		{
			content.Clear();
			content.Add("");
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

		public override void GetAppearance()
		{
			for (int j = 0; j < content.Count - 1; j++)
			{
				for (int i = 0; i < content[j].Length; i++)
				{

				}
			}

			if (IsFocused)
			{

			}
			else
			{

			}
		}

		public bool ParseAndExecute(ConsoleKeyInfo keyPressed)
		{
			int ascii = StringManipulation.ToChar(keyPressed.KeyChar);

			if ( ascii >= 32 && ascii <= 126)
			{
				InsertContent(keyPressed.KeyChar, 0, 5);	//For Test Only

				return true;
			}

			switch (keyPressed.Key)
			{
				case ConsoleKey.Backspace:
					DeleteContent(0, 5);	//For Test Only
					break;

				case ConsoleKey.UpArrow:
					DisplayArea_Component.Pointer_Component.MoveUp();
					break;

				case ConsoleKey.DownArrow:
					DisplayArea_Component.Pointer_Component.MoveDown();
					break;

				case ConsoleKey.LeftArrow:
					DisplayArea_Component.Pointer_Component.MoveLeft();
					break;

				case ConsoleKey.RightArrow:
					DisplayArea_Component.Pointer_Component.MoveRight();
					break;

				//case ConsoleKey.Escape:
					//break;

				default:
					return false;
			}

			return true;
		}
	}

	class TextboxDisplayArea
	{
		public const int Width  = 66;
		public const int Height = 23;

		public Coordinates Anchor { get; set; } = new Coordinates();
		public TextboxPointer Pointer_Component { get; set; } = new TextboxPointer();

		public List<string> TextboxContentClone { get; set; }

		private char[,] Content = new char[Width, Height];
		public char[,] GetContent()
		{
			return Content;
		}
		public void SetContent()
		{
			int y = TextboxContentClone.Count;
			int x = 0;
			for (int i = 0; i < y; i++)
			{
				if (x > TextboxContentClone[i].Length)
				{
					x = TextboxContentClone[i].Length;
				}
			}

			char[,] CharacterMap = new char[x, y];

			for (int j = 0; j < y; j++)
			{
				for (int i = 0; i < x; i++)
				{
					if (i < TextboxContentClone[j].Length)
					{
						CharacterMap[i, j] = Convert.ToChar(StringManipulation.Mid(TextboxContentClone[j], i, 1));
					}
					else
					{
						CharacterMap[i, j] = ' ';
					}
				}
			}

			for (int j = 0; j < Height; j++)
			{
				for (int i = 0; i < Width; i++)
				{
					Content[i, j] = CharacterMap[i, j];
				}
			}
		}
	}

	class TextboxPointer
	{
		public int Position { get; set; }
		public int Line { get; set; } = 0;

		public ConsoleColor PointerColor { get; set; } = ConsoleColor.Blue;

		public void MoveUp()
		{
			Line--;
		}

		public void MoveDown()
		{
			Line++;
		}

		public void MoveLeft()
		{
			Position--;
		}

		public void MoveRight()
		{
			Position++;
		}
	}

	class PopUpMenu_Files : PopUpMenu
	{

	}
}
