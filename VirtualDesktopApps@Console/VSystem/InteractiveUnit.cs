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

			DisplayArea_Component.CharacterMapRef = CharacterMap;
			DisplayArea_Component.Pointer_Component.TextboxAnchorRef = Anchor;

			for (int j = 0; j < TextboxDisplayArea.Height; j++)
			{
				CharacterMap.Add(new List<char?>());

				for (int i = 0; i < TextboxDisplayArea.Width; i++)
				{
					CharacterMap[j].Add(new char?());
					CharacterMap[j][i] = null;
				}
			}

			/*
			string str = "Hello, World";

			for (int i = 1; i <= str.Length; i++)
			{
				CharacterMap[0][i - 1] = Convert.ToChar(StringManipulation.Mid(str, i, 1));
			}
			*/
		}

		public TextboxDisplayArea DisplayArea_Component { get; set; } = new TextboxDisplayArea();

		private List<List<char?>> CharacterMap = new List<List<char?>>();

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

		public void NewLine()
		{
			if (CharacterMap.Count >= TextboxDisplayArea.Height)
			{
				CharacterMap.Add(new List<char?>());

			}
		}

		public void DeleteAll()
		{
			for (int j = 0; j < CharacterMap.Count; j++)
			{
				for (int i = 0; i < CharacterMap[j].Count; i++)
				{
					CharacterMap[j][i] = null;
				}
			}
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
			
		}

		public bool ParseAndExecute(ConsoleKeyInfo keyPressed)
		{
			if (!DisplayArea_Component.Pointer_Component.ParseAndExecute(keyPressed))
			{
				int ascii = StringManipulation.ToChar(keyPressed.KeyChar);

				if (ascii >= 32 && ascii <= 126)
				{
					WriteCharMap
						(
						keyPressed.KeyChar, 
						DisplayArea_Component.Pointer_Component.Anchor.Y, 
						DisplayArea_Component.Pointer_Component.Anchor.X
						);

					if (DisplayArea_Component.Pointer_Component.Anchor.X != TextboxDisplayArea.Width)
					{
						DisplayArea_Component.Pointer_Component.MoveRight();
					}
					else
					{
						DisplayArea_Component.MoveRight();
					}
				}
				else
				{
					switch (keyPressed.Key)
					{
						case ConsoleKey.Backspace:
							RemoveCharMap(DisplayArea_Component.Pointer_Component.Anchor.Y, DisplayArea_Component.Pointer_Component.Anchor.X);    //For Test Only
							break;

						case ConsoleKey.Enter:
							NewLine();
							break;

						//case ConsoleKey.Escape:
						//break;

						default:
							return false;
					}
				}

				DisplayArea_Component.SetRenderBuffer(Anchor.X, Anchor.Y);

				return true;
			}

			return true;
		}
	}

	class TextboxDisplayArea
	{
		public TextboxDisplayArea()
		{

		}

		public const int Width  = 64;
		public const int Height = 23;

		public Coordinates Anchor { get; set; } = new Coordinates();
		public TextboxPointer Pointer_Component { get; set; } = new TextboxPointer();

		public List<List<char?>> CharacterMapRef { get; set; }
		public Pixel[,] RenderBufferRef { get; set; }

		public void SetRenderBuffer(int textboxAnchorX, int textboxAnchorY)
		{
			for (int j = 0; j < Height; j++)
			{
				for (int i = 0; i < Width; i++)
				{
					RenderBufferRef[i + textboxAnchorX, j + textboxAnchorY].DisplayCharacter = CharacterMapRef[j][i];
				}
			}
		}

		public void MoveUp()
		{

		}

		public void MoveDown()
		{

		}

		public void MoveLeft()
		{

		}

		public void MoveRight()
		{

		}
	}

	class TextboxPointer
	{
		public Coordinates Anchor { get; set; } = new Coordinates();
		public Coordinates TextboxAnchorRef { get; set; }

		public ConsoleColor PointerColor { get; set; } = ConsoleColor.Blue;

		public Pixel[,] RenderBufferRef { get; set; }

		public bool ParseAndExecute(ConsoleKeyInfo keyPressed)
		{
			switch (keyPressed.Key)
			{
				case ConsoleKey.UpArrow:
					MoveUp();
					break;

				case ConsoleKey.DownArrow:
					MoveDown();
					break;

				case ConsoleKey.LeftArrow:
					MoveLeft();
					break;

				case ConsoleKey.RightArrow:
					MoveRight();
					break;

				default:
					return false;
			}

			return true;
		}

		public void MoveUp()
		{
			Anchor.Y--;

			SetRenderBuffer(TextboxAnchorRef.X, TextboxAnchorRef.Y);
		}

		public void MoveDown()
		{
			Anchor.Y++;

			SetRenderBuffer(TextboxAnchorRef.X, TextboxAnchorRef.Y);
		}

		public void MoveLeft()
		{
			SetRenderBuffer(TextboxAnchorRef.X, TextboxAnchorRef.Y);
			RenderBufferRef[TextboxAnchorRef.X + Anchor.X + 1, TextboxAnchorRef.Y + Anchor.Y].ForegroundColor = ConsoleColor.Black;
			RenderBufferRef[TextboxAnchorRef.X + Anchor.X + 1, TextboxAnchorRef.Y + Anchor.Y].BackgroundColor = ConsoleColor.White;

			Anchor.X--;

		}

		public void MoveRight()
		{
			SetRenderBuffer(TextboxAnchorRef.X, TextboxAnchorRef.Y);
			RenderBufferRef[TextboxAnchorRef.X + Anchor.X - 1, TextboxAnchorRef.Y + Anchor.Y].ForegroundColor = ConsoleColor.Black;
			RenderBufferRef[TextboxAnchorRef.X + Anchor.X - 1, TextboxAnchorRef.Y + Anchor.Y].BackgroundColor = ConsoleColor.White;

			Anchor.X++;

		}

		public void SetRenderBuffer(int textboxAnchorX, int textboxAnchorY)
		{
			RenderBufferRef[textboxAnchorX + Anchor.X, textboxAnchorY + Anchor.Y].ForegroundColor = ConsoleColor.White;
			RenderBufferRef[textboxAnchorX + Anchor.X, textboxAnchorY + Anchor.Y].BackgroundColor = ConsoleColor.Blue;
		}
	}

	class PopUpMenu_Files : PopUpMenu
	{

	}
}
