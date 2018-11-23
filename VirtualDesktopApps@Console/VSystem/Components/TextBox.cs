using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	class TextBox : Button
	{
		public TextBox(int xPos, int yPos)
		{
			Anchor.X = xPos;
			Anchor.Y = yPos;

			CharacterMap.Add(new List<char?>());
			CharacterMap[0].Add(new char?());
			DisplayArea_Component.SetRenderBuffer(CharacterMap);
		}

		public TextboxDisplayArea DisplayArea_Component { get; set; } = new TextboxDisplayArea(64, 23);

		private List<List<char?>> CharacterMap = new List<List<char?>>();

		public List<List<char?>> ReadCharMap()
		{
			return CharacterMap;
		}

		public void WriteCharMap(char input)
		{
			TextboxPointer pointerRef = DisplayArea_Component.Pointer_Component;

			CharacterMap[pointerRef.Anchor.Y].Insert(pointerRef.Anchor.X, input);

			if (DisplayArea_Component.Pointer_Component.MoveRight(DisplayArea_Component, CharacterMap))
			{
				DisplayArea_Component.MoveRight(ref CharacterMap);
			}

			CharacterMap[pointerRef.Anchor.Y].Remove(null);
		}

		public void RemoveCharMap()
		{
			TextboxPointer p = DisplayArea_Component.Pointer_Component;

			if (p.Anchor.X > 0)
			{
				CharacterMap[p.Anchor.Y].RemoveAt(p.Anchor.X - 1);
			}
			else
			{
				MergeLine();
				return;
			}

			// Deprecated
			if (DisplayArea_Component.Pointer_Component.Anchor.X != DisplayArea_Component.Width)
			{
				DisplayArea_Component.Pointer_Component.MoveLeft(DisplayArea_Component, CharacterMap);
			}
			else
			{
				DisplayArea_Component.MoveLeft();
			}
		}

		public void MergeLine()
		{
			TextboxPointer p = DisplayArea_Component.Pointer_Component;

			p.MoveLeft(DisplayArea_Component, CharacterMap);

			List<char?> targetLine = CharacterMap[p.Anchor.Y];

			//p.Anchor.X = targetLine.Count;			

			try
			{
				targetLine.AddRange(CharacterMap[p.Anchor.Y + 1]);

				CharacterMap.RemoveAt(p.Anchor.Y + 1);
			}
			catch (ArgumentOutOfRangeException) { }

			//p.Anchor.Y--;
		}

		public void NewLine()
		{
			TextboxPointer p = DisplayArea_Component.Pointer_Component;

			List<char?> currentLine = CharacterMap[p.Anchor.Y];
			List<char?> newLine = new List<char?>(currentLine.GetRange(
				p.Anchor.X, currentLine.Count - p.Anchor.X));

			currentLine.RemoveRange(p.Anchor.X, currentLine.Count - p.Anchor.X);

			p.Anchor.Y++;
			p.Anchor.X = 0;

			CharacterMap.Insert(p.Anchor.Y, newLine);
		}

		public void DeleteAll()
		{
			CharacterMap = new List<List<char?>>();
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

		public override Pixel[,] GetRenderBuffer()
		{
			return DisplayArea_Component.GetRenderBuffer();
		}

		public override bool ParseAndExecute(ConsoleKeyInfo keyPressed)
		{
			if (!DisplayArea_Component.ParseAndExecute(keyPressed, ref CharacterMap))
			{
				int ascii = keyPressed.KeyChar;

				if (ascii >= 32 && ascii <= 126)
				{
					WriteCharMap(keyPressed.KeyChar);
				}
				else
				{
					switch (keyPressed.Key)
					{
						case ConsoleKey.Backspace:
							RemoveCharMap();
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
			}

			DisplayArea_Component.SetRenderBuffer(CharacterMap);

			return true;
		}
	}

	class TextboxDisplayArea
	{
		public TextboxDisplayArea(int width, int height)
		{
			Width = width;
			Height = height;
			renderBuffer = new Pixel[Width, Height];

			for (int j = 0; j < height; j++)
			{
				for (int i = 0; i < width; i++)
				{
					renderBuffer[i, j] = new Pixel();
				}
			}
		}

		public int Width { get; set; }
		public int Height { get; set; }

		public Coordinates Anchor { get; set; } = new Coordinates();
		public TextboxPointer Pointer_Component { get; set; } = new TextboxPointer();

		private Pixel[,] renderBuffer;
		public Pixel[,] GetRenderBuffer()
		{
			return renderBuffer;
		}
		public void SetRenderBuffer(List<List<char?>> CharacterMap)
		{
			int numOfLine = CharacterMap.Count;

			for (int j = 0; j < Height; j++)
			{
				if (Anchor.Y + j < CharacterMap.Count)
				{
					int numOfChar = CharacterMap[j].Count;

					for (int i = 0; i < Width; i++)
					{

						if (numOfChar > Anchor.X + i)
						{
							renderBuffer[i, j].DisplayCharacter = CharacterMap[j + Anchor.Y][i + Anchor.X];
						}
						else
						{
							renderBuffer[i, j].DisplayCharacter = null;
						}

						// Find the Pixel that the pointer WAS at and reset it
						if (renderBuffer[i, j].BackgroundColor == ConsoleColor.Blue)
						{
							renderBuffer[i, j].ForegroundColor = ConsoleColor.Black;
							renderBuffer[i, j].BackgroundColor = ConsoleColor.White;
						}
					}
				}
				else
				{
					for (int i = 0; i < Width; i++)
					{
						renderBuffer[i, j].DisplayCharacter = null;
					}
				}
			}

			if (renderBuffer[0, CharacterMap.Count - Anchor.Y].BackgroundColor == ConsoleColor.Blue)
			{
				renderBuffer[0, CharacterMap.Count - Anchor.Y].ForegroundColor = ConsoleColor.Black;
				renderBuffer[0, CharacterMap.Count - Anchor.Y].BackgroundColor = ConsoleColor.White;
			}

			TextboxPointer p = Pointer_Component;

			renderBuffer[p.Anchor.X, p.Anchor.Y].BackgroundColor = p.PointerBackColor;
			renderBuffer[p.Anchor.X, p.Anchor.Y].ForegroundColor = p.PointerForeColor;
		}

		public bool ParseAndExecute(ConsoleKeyInfo keyPressed, ref List<List<char?>> characterMap)
		{
			switch (Pointer_Component.ParseAndExecute(keyPressed, this, ref characterMap))
			{
				case 'w':
					MoveUp();
					break;

				case 's':
					MoveDown(ref characterMap);
					break;

				case 'a':
					MoveLeft();
					break;

				case 'd':
					MoveRight(ref characterMap);
					break;

				case 'n':
					break;

				case '0':
					return false;
			}

			return true;
		}

		public bool MoveUp()
		{
			if (Anchor.Y > 0)
			{
				Anchor.Y--;

				return true;
			}

			return false;
		}

		public bool MoveDown(ref List<List<char?>> characterMap)
		{
			if (Anchor.Y < characterMap.Count - 1)
			{
				Anchor.Y++;

				return true;
			}

			return false;
		}

		public bool MoveLeft()
		{
			if (Anchor.X > 0)
			{
				Anchor.X--;

				return true;
			}

			return false;
		}

		public bool MoveRight(ref List<List<char?>> characterMap)
		{
			int maxWidth = 0;
			foreach (var line in characterMap)
			{
				if (line.Count > maxWidth)
				{
					maxWidth = line.Count;
				}
			}

			if (Anchor.X < maxWidth - 1)
			{
				Anchor.X++;

				return true;
			}

			return false;
		}
	}

	class TextboxPointer
	{
		public Coordinates Anchor { get; set; } = new Coordinates();

		public ConsoleColor PointerBackColor { get; set; } = ConsoleColor.Blue;
		public ConsoleColor PointerForeColor { get; set; } = ConsoleColor.White;

		public char ParseAndExecute(ConsoleKeyInfo keyPressed, 
			TextboxDisplayArea parentComponent, ref List<List<char?>> characterMap)
		{
			switch (keyPressed.Key)
			{
				/*
				 * If pointer succeed in moving in whatever direction, it gives no feedback
				 * If fail, then pass out the reason
				 * "w" for up; "s" for down; "a" for left; "d" for right; "n" for "NO CHANGE"
				 * "0" for not using the key
				*/

				case ConsoleKey.UpArrow:
					if (MoveUp(parentComponent, characterMap))
						return 'w';
					break;

				case ConsoleKey.DownArrow:
					if (MoveDown(parentComponent, characterMap))
						return 's';
					break;

				case ConsoleKey.LeftArrow:
					if (MoveLeft(parentComponent, characterMap))
						return 'a';
					break;

				case ConsoleKey.RightArrow:
					if (MoveRight(parentComponent, characterMap))
						return 'd';
					break;

				default:
					return '0';
			}

			return 'n';
		}

		/*                         --IMPORTANT NOTE--
		 *              If any of the following returns a FALSE, 
		 * it means that there is NO NEED to MOVE the display area component
		 * 
		 *        Otherwise DO MOVE it, in the direction of the pointer
		 */

		public bool MoveUp(TextboxDisplayArea displayArea, List<List<char?>> characterMap)
		{
			int absX = Anchor.X + displayArea.Anchor.X;
			int absY = Anchor.Y + displayArea.Anchor.Y;

			if (absY == 0)
			{
				return false;
			}
			else
			{
				if (absX > characterMap[absY - 1].Count)
				{
					absX = characterMap[absY - 1].Count;
				}

				absY--;
			}

			Anchor.X = absX - displayArea.Anchor.X;
			Anchor.Y = absY - displayArea.Anchor.Y;

			if (Anchor.X < displayArea.Width && Anchor.Y < displayArea.Height)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool MoveDown(TextboxDisplayArea displayArea, List<List<char?>> characterMap)
		{
			int absX = Anchor.X + displayArea.Anchor.X;
			int absY = Anchor.Y + displayArea.Anchor.Y;

			if (absY == characterMap.Count - 1)
			{
				return false;
			}
			else
			{
				if (absX > characterMap[absY + 1].Count)
				{
					absX = characterMap[absY + 1].Count;
				}

				absY++;
			}

			Anchor.X = absX - displayArea.Anchor.X;
			Anchor.Y = absY - displayArea.Anchor.Y;

			if (Anchor.X < displayArea.Width && Anchor.Y < displayArea.Height)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool MoveLeft(TextboxDisplayArea displayArea, List<List<char?>> characterMap)
		{
			int absX = Anchor.X + displayArea.Anchor.X;
			int absY = Anchor.Y + displayArea.Anchor.Y;

			if (absX == 0)
			{
				if (absY == 0)
				{
					return false;
				}
				else
				{
					absY--;
					absX = characterMap[absY].Count;
				}
			}
			else
			{
				absX--;
			}

			Anchor.X = absX - displayArea.Anchor.X;
			Anchor.Y = absY - displayArea.Anchor.Y;

			if (Anchor.X < displayArea.Width && Anchor.Y < displayArea.Height)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool MoveRight(TextboxDisplayArea displayArea, List<List<char?>> characterMap)
		{
			int absX = Anchor.X + displayArea.Anchor.X;
			int absY = Anchor.Y + displayArea.Anchor.Y;

			if (absX == characterMap[absY].Count)
			{
				if (absY == characterMap.Count - 1)
				{
					return false;
				}
				else
				{
					absX = 0;
					absY++;
				}
			}
			else
			{
				absX++;
			}

			Anchor.X = absX - displayArea.Anchor.X;
			Anchor.Y = absY - displayArea.Anchor.Y;

			if (Anchor.X < displayArea.Width && Anchor.Y < displayArea.Height)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
