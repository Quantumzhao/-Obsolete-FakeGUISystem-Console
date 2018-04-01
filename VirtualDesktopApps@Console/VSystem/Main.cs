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
		static void Main(string[] args)
		{
			initiation();

			VSystem.KeyPressHandler(Console.ReadKey());

			//runNotepadTest();

			//VSystem.Test();

			VSystem.RenderAll();

			Console.ReadKey();
		}

		private static void initiation()
		{
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

			runNotepadTest();
		}

		private static void runNotepadTest()
		{
			SubProgramCollection<Notepad>.AddNewProg(new Notepad());

			VSystem.KeyPressHandler = SubProgram.KeyPressHandler;
		}
	}

	class VSystem
	{
		public const int Width = 125;
		public const int Height = 50;

		public static Pixel[,] Display { get; set; } = new Pixel[Width, Height];

		public delegate void KeyPressDelegate(ConsoleKeyInfo key);
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
		
		static public void Test()
		{
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					Display[i, j].Layer[0] = ' ';
				}
			}
		}
	}

	class SubProgramCollection<T> where T : SubProgram
	{
		private static List<T> SubProgColle = new List<T>();

		public T this[int index]
		{
			get
			{
				return SubProgColle[index];
			}

			set
			{
				SubProgColle[index] = value;
			}
		}

		public static void AddNewProg(T subProgram)                                                //Create a new specific subprogram instance
		{
			SubProgColle.Add(subProgram);

			SubProgColle[SubProgColle.Count - 1].ProgramID = SubProgColle.Count - 1;

			for (int j = 0; j < VSystem.Height; j++)                                               //Create a new layer for the new subprogram
			{
				for (int i = 0; i < VSystem.Width; i++)
				{
					VSystem.Display[i, j].Layer.Add(' ');
				}
			}
		}
	}

	class InteractiveUnitsCollection<T> where T : Button
	{
		private static List<T> interactiveUnitsCollection = new List<T>();

		public T this[int index]
		{
			get
			{
				return interactiveUnitsCollection[index];
			}

			set
			{
				interactiveUnitsCollection[index] = value;
			}
		}

		public static void AddNewComponent(T component)
		{
			interactiveUnitsCollection.Add(component);
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

	enum AvailableProgs
	{
		Notepad, 
	}
}
