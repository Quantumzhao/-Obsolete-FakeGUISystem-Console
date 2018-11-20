using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	delegate void ChangeHighLightDelegate();

	class ComponentsCollection
	{
		private List<Button> components { get; set; } = new List<Button>();
		public Button this[int index]
		{
			get
			{
				return components[index];
			}

			set
			{
				components[index] = value;
			}
		}
		public Button this[string name]
		{
			get
			{
				return (from component in components
						where component.Name.Equals(name)
						select component).Single();
			}

			set
			{
				for (int i = 0; i < components.Count; i++)
				{
					if (components[i].Name.Equals("name"))
					{
						components[i] = value;
					}
				}
			}
		}

		public int Count
		{
			get
			{
				return components.Count;
			}
		}

		public void Add(Button component, string name)
		{
			component.Name = name;
			components.Add(component);
		}

		public Button GetHighlighted()
		{
			return (from component in components
					where component.IsHighlighted
					select component).Single();
		}

		public void SetHighlighted(string name)
		{
			foreach (Button component in components)
			{
				if (component.Name.Equals(name))
				{
					component.IsHighlighted = true;
				}
				else
				{
					component.IsHighlighted = false;
				}
			}
		}
	}

	abstract class Button : IEntity
	{
		public Coordinates Anchor { get; set; } = new Coordinates();

		public int Width { get; set; }
		public int Height { get; set; }

		public string Name { get; set; }

		private ChangeHighLightDelegate changeHighLightHandler;

		bool isHighLighted;
		public bool IsHighlighted
		{
			get
			{
				return isHighLighted;
			}
			set
			{
				if (value)
				{
					changeHighLightHandler();

					isHighLighted = value;
				}
			}
		}
		private bool isFocused;
		// It needs to be changed to a more detailed structure
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
				}
			}
		}

		private Pixel[,] renderBuffer;
		public virtual Pixel[,] GetRenderBuffer()
		{
			return new Pixel[Width, Height];
		}

		public abstract bool ParseAndExecute(ConsoleKeyInfo key);
	}

	class PopUpMenu : IEntity
	{
		public Coordinates Anchor { get; set; } = new Coordinates();

		public int Width { get; set; } = 15;
		public int Height { get; set; } = 7;

		public string Name { get; set; }

		public bool IsHighlighted { get; set; }
		public bool IsFocused { get; set; }
		public bool IsComponentFocused { get; set; }

		public bool IsVisible { get; set; }

		private Pixel[,] renderBuffer;
		public Pixel[,] GetRenderBuffer()
		{
			return new Pixel[Width, Height];
		}

		public PopUpMenu()
		{
			/*--For Test Only--*/
			Anchor.X = 2;
			Anchor.Y = 2;
			
		}

		public bool ParseAndExecute(ConsoleKeyInfo key)
		{
			throw new NotImplementedException();
		}
	}

	class TitleBar : Button
	{
		public override bool ParseAndExecute(ConsoleKeyInfo key)
		{
			throw new NotImplementedException();
		}
	}

	class MenuItem : Button
	{
		public override bool ParseAndExecute(ConsoleKeyInfo key)
		{
			throw new NotImplementedException();
		}
	}

	class MenuItem_File<T> : MenuItem where T : SubProgram
	{
		public void PopUpMenu(T subProgram)
		{
			throw new NotImplementedException();
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
		public TextBox(int xPos, int yPos)
		{
			Anchor.X = xPos;
			Anchor.Y = yPos;

			//DisplayArea_Component.CharacterMapRef = CharacterMap;
			//DisplayArea_Component.Pointer_Component.TextboxAnchorRef = Anchor;

			for (int j = 0; j < DisplayArea_Component.Height; j++)
			{
				CharacterMap.Add(new List<char?>());

				for (int i = 0; i < DisplayArea_Component.Width; i++)
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

			CharacterMap[pointerRef.Anchor.Y].Remove(null);
			/*
			if (pointerRef.Anchor.X != DisplayArea_Component.Width)
			{
				DisplayArea_Component.Pointer_Component.MoveRight();
			}
			else
			{
				DisplayArea_Component.MoveRight();
			}
			*/
		}

		public void RemoveCharMap(int anchorY, int anchorX)
		{
			if (anchorX == 0 && anchorY != 0)
			{
				MergeLine();
			}

			try
			{
				CharacterMap[anchorY].RemoveAt(anchorX);
			}
			catch { }

			if (CharacterMap[anchorY].Count < DisplayArea_Component.Width)
			{
				CharacterMap[anchorY].Add(null);
			}
		}

		public void MergeLine()
		{

		}

		public void NewLine()
		{
			if (CharacterMap.Count >= DisplayArea_Component.Height)
			{
				CharacterMap.Add(new List<char?>());
				/*More codes needed*/
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
							RemoveCharMap(DisplayArea_Component.Pointer_Component.Anchor.Y, DisplayArea_Component.Pointer_Component.Anchor.X - 1);

							if (DisplayArea_Component.Pointer_Component.Anchor.X != DisplayArea_Component.Width)
							{
								DisplayArea_Component.Pointer_Component.MoveLeft();
							}
							else
							{
								DisplayArea_Component.MoveLeft();
							}

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

				DisplayArea_Component.SetRenderBuffer(CharacterMap);

				return true;
			}

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
		}

		public int Width  { get; set; }
		public int Height { get; set; }

		public Coordinates Anchor { get; set; } = new Coordinates();
		public TextboxPointer Pointer_Component { get; set; } = new TextboxPointer();

		//public List<List<char?>> CharacterMapRef { get; set; }
		//The child component has a reference to its parent component, 
		//	and the reference is set when its parent component initializes

		private Pixel[,] renderBuffer;
		public Pixel[,] GetRenderBuffer()
		{
			return renderBuffer;
		}
		public void SetRenderBuffer(List<List<char?>> CharacterMap)
		{
			for (int j = 0; j < Height; j++)
			{
				for (int i = 0; i < Width; i++)
				{
					try
					{
						renderBuffer[i, j].DisplayCharacter = CharacterMap[j][i];
					}
					catch (IndexOutOfRangeException)
					{
						renderBuffer[i, j].DisplayCharacter = null;
					}
				}
			}

			TextboxPointer p = Pointer_Component;

			renderBuffer[p.Anchor.X, p.Anchor.Y].BackgroundColor = p.PointerBackColor;
			renderBuffer[p.Anchor.X, p.Anchor.Y].ForegroundColor = p.PointerForeColor;
		}

		public bool ParseAndExecute(ConsoleKeyInfo keyPressed, ref List<List<char?>> characterMap)
		{
			switch (Pointer_Component.ParseAndExecute(keyPressed, ref renderBuffer))
			{
				case 'w':
					MoveUp();
					break;

				case 's':
					MoveDown();
					break;

				case 'a':
					MoveLeft();
					break;

				case 'd':
					MoveRight();
					break;

				case 'n':
					break;

				case '0':
					return false;
			}

			return true;
		}

		public void MoveUp()
		{
			throw new NotImplementedException();
		}

		public void MoveDown()
		{
			throw new NotImplementedException();
		}

		public void MoveLeft()
		{
			throw new NotImplementedException();
		}

		public void MoveRight()
		{
			throw new NotImplementedException();
		}
	}

	class TextboxPointer
	{
		public Coordinates Anchor { get; set; } = new Coordinates();
		//public Coordinates TextboxAnchorRef { get; set; }

		public ConsoleColor PointerBackColor { get; set; } = ConsoleColor.Blue;
		public ConsoleColor PointerForeColor { get; set; } = ConsoleColor.White;

		public char ParseAndExecute(ConsoleKeyInfo keyPressed, ref Pixel[,] renderBuffer)
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
					if (!MoveUp())
						return 'w';
					break;

				case ConsoleKey.DownArrow:
					if (!MoveDown())
						return 's';
					break;

				case ConsoleKey.LeftArrow:
					if (!MoveLeft())
						return 'a';
					break;

				case ConsoleKey.RightArrow:
					if (!MoveRight())
						return 'd';
					break;

				default:
					return '0';
			}

			return 'n';
		}

		public bool MoveUp()
		{
			try
			{
				Anchor.Y--;

				return true;
			}
			catch (IndexOutOfRangeException)
			{
				return false;
			}
		}

		public bool MoveDown()
		{
			try
			{
				Anchor.Y++;

				return true;
			}
			catch (IndexOutOfRangeException)
			{

				return false;
			}
		}

		public bool MoveLeft()
		{
			try
			{
				Anchor.X--;

				return true;
			}
			catch
			{
				return false;
			}

		}

		public bool MoveRight()
		{
			try
			{
				Anchor.X++;

				return true;
			}
			catch
			{
				return false;
			}

		}
		/*
		public void SetRenderBuffer(ref Pixel[,] renderBuffer, int textboxAnchorX, int textboxAnchorY)
		{
			renderBuffer[textboxAnchorX + Anchor.X, textboxAnchorY + Anchor.Y].ForegroundColor = ConsoleColor.White;
			renderBuffer[textboxAnchorX + Anchor.X, textboxAnchorY + Anchor.Y].BackgroundColor = ConsoleColor.Blue;
		}
		*/
	}

	class PopUpMenu_Files : PopUpMenu
	{

	}
}
