using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplementaryClassLibraryForStringManipulation;

namespace VirtualDesktopApps_Console
{
	class Launcher
	{
		public static ConsoleKeyInfo KeyPressed;

		static void Main(string[] args)
		{
			initiation();

			runNotepadTest();

			//VSystem.KeyPressHandler(Console.ReadKey());
			//runNotepadTest();
			//VSystem.Test();

			VSystem.RenderAll();

			KeyPressed = Console.ReadKey();

			VSystem.InputParser(KeyPressed);

			VSystem.KeyPressHandler();

		}

		private static void initiation()
		{
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WindowWidth  = Console.LargestWindowWidth;
			Console.WindowHeight = Console.LargestWindowHeight;
			Console.CursorVisible = false;
						
			for (int i = 0; i < VSystem.Width; i++)
			{
				for (int j = 0; j < VSystem.Height; j++)
				{
					VSystem.Display[i, j] = new Pixel();
				}
			}			
		}

		private static void runNotepadTest()
		{
			SubProgramCollectionClass<Notepad>.AddNewProg(new Notepad());
			SubProgramCollectionClass<Notepad>.SubprogramCollection[SubProgramCollectionClass<Notepad>.
				SubprogramCollection.Count - 1].Window_Component.GetAppearance(AvailableProgs.Notepad);
			SubProgramCollectionClass<Notepad>.SubprogramCollection[SubProgramCollectionClass<Notepad>.
				SubprogramCollection.Count - 1].KeyPressHandler = VSystem.KeyPressHandler;
		}
	}

	class VSystem
	{
		public const int Width = 125;
		public const int Height = 50;
		public static Pixel[,] Display { get; set; } = new Pixel[Width, Height];

		public delegate void KeyPressDelegate();

		public static KeyPressDelegate KeyPressHandler;

		static public void RenderAll()
		{
			for (int j = 0; j < Height; j++)
			{
				for (int i = 0; i < Width; i++)
				{
					int k = 0;
					
					while (Display[i, j].Layer[k] == ' ')
					{
						if (k != Display[i, j].Layer.Count - 1)
						{
							k++;
						}
						else
						{							
							break;
						}
					}

					Console.Write(Display[i, j].Layer[k]);
				}

				Console.Write("║");
				Console.WriteLine();
			}

			for (int i = 0; i < 125; i++)
			{
				Console.Write("═");
			}
			Console.Write("╝");
		}
		
		public static void Test()
		{
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					Display[i, j].Layer[0] = ' ';
				}
			}
		}

		public static void InputParser(ConsoleKeyInfo keyPressed)
		{			
			switch (keyPressed.Key)
			{
				case ConsoleKey.Escape:
					KeyPressHandler = FocusCursor.BackwardToLowerHierarchy;
					break;

				case ConsoleKey.Enter:
					KeyPressHandler = FocusCursor.ForwardToHigherHierarchy;
					break;

				case ConsoleKey.Tab:
					KeyPressHandler = FocusCursor.ToNextFocus;
					break;

				default:
					KeyPressHandler = GetFocusedSubProgram().KeyPressHandler;
					break;
			}
		}

		public static SubProgram GetFocusedSubProgram()
		{
			return new Notepad();                                                                  //For Test Only
		}
	}

	class SubProgramCollectionClass<T> where T : SubProgram
	{
		public static List<T> SubprogramCollection { get; set; } = new List<T>();

		public static void AddNewProg(T subProgram)                                                //Create a new specific subprogram instance
		{
			SubprogramCollection.Add(subProgram);

			SubprogramCollection[SubprogramCollection.Count - 1].ProgramID = SubprogramCollection.Count - 1;

			for (int j = 0; j < VSystem.Height; j++)                                               //Create a new layer for the new subprogram
			{
				for (int i = 0; i < VSystem.Width; i++)
				{
					VSystem.Display[i, j].Layer.Add(' ');
				}
			}
		}
	}

	class Pixel
	{
		public List<char> Layer { get; set; } = new List<char>();

		public Pixel()
		{			
			Layer.Add(' ');						
		}
	}

	class FocusCursor
	{
		public static void ForwardToHigherHierarchy()
		{
			
		}

		public static void BackwardToLowerHierarchy()
		{

		}
		
		public static void ToPreviousFocus()
		{

		}

		public static void ToNextFocus()
		{

		}
	}
	
	interface IEntity
	{
		int AnchorX { get; set; }
		int AnchorY { get; set; }
		int Width { get; set; }
		int Height { get; set; }
		bool IsSelected { get; set; }
		bool IsFocused { get; set; }
	}

	enum AvailableProgs
	{
		Notepad, 
	}
}
