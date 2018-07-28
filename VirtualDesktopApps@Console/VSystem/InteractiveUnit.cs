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
			Anchor.X = 1;
			Anchor.Y = 3;

			DisplayArea_Component.CharacterMapClone = CharacterMap;

			for (int j = 0; j < TextboxDisplayArea.Height; j++)
			{
				CharacterMap.Add(new List<char?>());

				for (int i = 0; i < TextboxDisplayArea.Width; i++)
				{
					CharacterMap[j].Add(new char?());
					CharacterMap[j][i] = null;
				}
			}

			string str = "Hello, World";

			for (int i = 1; i <= str.Length; i++)
			{
				CharacterMap[0][i - 1] = Convert.ToChar(StringManipulation.Mid(str, i, 1).ToString());
			}
		}

		public TextboxDisplayArea DisplayArea_Component { get; set; } = new TextboxDisplayArea();

		private List<List<char?>> CharacterMap = new List<List<char?>>();

		private List<string> content = new List<string>();

		public List<List<char?>> ReadCharMap()
		{
			return CharacterMap;
		}

		public void WriteCharMap(char input, int anchorY, int anchorX)
		{
			CharacterMap[anchorY].Insert(anchorX, input);

			CharacterMap[anchorY].Remove(null);
		}

		public void RemoveCharMap(int anchorY, int anchorX)
		{
			if (anchorX == 0 && anchorY != 0)
			{
				MergeLine();
			}

			CharacterMap[anchorY].RemoveAt(anchorX);

			if (CharacterMap[anchorY].Count < TextboxDisplayArea.Width)
			{
				CharacterMap[anchorY].Add(null);
			}
		}

		public void MergeLine()
		{

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
				WriteCharMap(keyPressed.KeyChar, 0, 0);	//For Test Only

				return true;
			}

			switch (keyPressed.Key)
			{
				case ConsoleKey.Backspace:
					RemoveCharMap(0, 1);	//For Test Only
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

			DisplayArea_Component.SetRenderBuffer(Anchor.X, Anchor.Y);

			return true;
		}
	}

	class TextboxDisplayArea
	{
		public const int Width  = 64;
		public const int Height = 23;

		public Coordinates Anchor { get; set; } = new Coordinates();
		public TextboxPointer Pointer_Component { get; set; } = new TextboxPointer();

		public List<List<char?>> CharacterMapClone { get; set; }
		public char[,] RenderBufferClone { get; set; }

		private char[,] ContentDisplayed = new char[Width, Height];
		public char[,] GetContentDisplayed()
		{
			return ContentDisplayed;
		}
		public void SetContentDisplayed()
		{
			for (int j = 0; j < Height; j++)
			{
				for (int i = 0; i < Width; i++)
				{
					ContentDisplayed[i, j] = (char)CharacterMapClone[j + Anchor.Y][i + Anchor.X];
				}
			}
		}

		public void SetRenderBuffer(int textboxAnchorX, int textboxAnchorY)
		{
			for (int j = 0; j < Height; j++)
			{
				for (int i = 0; i < Width; i++)
				{
					if (CharacterMapClone[j][i] != null)
					{
						RenderBufferClone[i + textboxAnchorX, j + textboxAnchorY] = (char)CharacterMapClone[j][i];

					}
					else
					{
						RenderBufferClone[i + textboxAnchorX, j + textboxAnchorY] = ' ';
					}
				}
			}
		}
	}

	class TextboxPointer
	{
		public Coordinates Anchor { get; set; } = new Coordinates();

		public ConsoleColor PointerColor { get; set; } = ConsoleColor.Blue;

		public void MoveUp()
		{
			Anchor.Y--;
		}

		public void MoveDown()
		{
			Anchor.Y++;
		}

		public void MoveLeft()
		{
			Anchor.X--;
		}

		public void MoveRight()
		{
			Anchor.X++;
		}
	}

	class PopUpMenu_Files : PopUpMenu
	{

	}
}
