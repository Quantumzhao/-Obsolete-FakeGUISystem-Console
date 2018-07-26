using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SupplementaryClassLibraryForStringManipulation;

namespace VirtualDesktopApps_Console
{
	class Launcher
	{
		public static ConsoleKeyInfo KeyPressed { get; set; }

		static void Main(string[] args)
		{
			initiation();

			//VSystem.KeyPressHandler(Console.ReadKey());
			//runNotepadTest();
			//VSystem.Test();

			VSystem.RenderAll();

			runNotepadTest();

			KeyPressed = Console.ReadKey();
			Console.Clear();

			VSystem.ParseAndExecute(KeyPressed);
			VSystem.RenderAll();

			Console.ReadKey();
		}

		private static void inputEliminator()
		{
			while (true)
			{
				Console.ReadKey();				

				Console.Clear();
			}
		}

		private static void initiation()
		{
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WindowWidth     = Console.LargestWindowWidth;
			Console.WindowHeight    = Console.LargestWindowHeight;
			Console.CursorVisible   = false;

			VSystem.IsFocused       = true;
						
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
			SubProgramCollectionClass<SubProgram>.AddNewSubprogram(new Notepad());
			SubProgramCollectionClass<SubProgram>.SubprogramCollection[SubProgramCollectionClass<SubProgram>.
				SubprogramCollection.Count - 1].Window_Component.GetAppearance();
			/*
			SubProgramCollectionClass<Notepad>.SubprogramCollection[SubProgramCollectionClass<Notepad>.
				SubprogramCollection.Count - 1].KeyPressHandler = VSystem.KeyPressHandler;
			*/

			SubProgramCollectionClass<SubProgram>.SubprogramCollection[SubProgramCollectionClass<SubProgram>.
				SubprogramCollection.Count - 1].IsComponentSelected = true;

		}
	}

	class VSystem
	{
		public const int Width  = 125;
		public const int Height = 50;

		public static bool IsFocused { get; set; } = false;
		public static Pixel[,] Display { get; set; } = new Pixel[Width, Height];

		//public delegate void KeyPressDelegate();

		//public static KeyPressDelegate KeyPressHandler;

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

		public static void ParseAndExecute(ConsoleKeyInfo keyPressed)
		{
			if (GetFocusedSubProgram() != null)
			{
				GetFocusedSubProgram().ParseAndExecute(keyPressed);

				return;
			}
			/*
			switch (keyPressed.Key)
			{
				case ConsoleKey.Escape:
					FocusCursor.BackwardToLowerHierarchy();
					break;
				case ConsoleKey.UpArrow:
					FocusCursor.BackwardToLowerHierarchy();
					break;

				case ConsoleKey.Enter:
					FocusCursor.ForwardToHigherHierarchy();
					break;
				case ConsoleKey.DownArrow:
					FocusCursor.ForwardToHigherHierarchy();
					break;

				case ConsoleKey.Tab:
					FocusCursor.ToNextFocus();
					break;
				case ConsoleKey.RightArrow:
					FocusCursor.ToNextFocus();
					break;

				case ConsoleKey.LeftArrow:
					FocusCursor.ToPreviousFocus();
					break;

				default:
					GetFocusedSubProgram().ParseAndExecute(k);
					break;
			}
			*/

		}

		public static SubProgram GetFocusedSubProgram()
		{
			for (int i = 0; i < SubProgramCollectionClass<SubProgram>.SubprogramCollection.Count; i++)
			{
				if (SubProgramCollectionClass<SubProgram>.SubprogramCollection[i].IsComponentSelected)
				{
					return SubProgramCollectionClass<SubProgram>.SubprogramCollection[i];
				}
			}

			return null;
		}
	}

	class SubProgramCollectionClass<T> where T : SubProgram
	{
		public static List<T> SubprogramCollection { get; set; } = new List<T>();

		public static void AddNewSubprogram(T subProgram)                                                //Create a new specific subprogram instance
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
