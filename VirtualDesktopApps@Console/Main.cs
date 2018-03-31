using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	class Launcher
	{
		static void Main(string[] args)
		{
			initiation();

			VSystem.Test();

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
		}
	}

	class VSystem
	{
		public const int Width = 125;
		public const int Height = 50;

		public static Pixel[,] Display { get; set; } = new Pixel[Width, Height];

		static public void RenderAll()
		{
			for (int j = 0; j < Height; j++)
			{
				for (int i = 0; i < Width; i++)
				{
					int k = 0;
					/*--POTENTIAL BUGS FROM HERE--*/
					while (Display[i, j].Layer[k] == ' ')
					{
						if (k != Display[i, j].Layer.Capacity - 1)
						{
							k++;
						}
						else
						{
							k--;

							return;
						}
					}
					/*--END MARKING--*/
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

		public static void AddNewTask()
		{

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

	class SubProgramCollection<T>
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
	}

	class Pixel
	{
		public List<char> Layer { get; set; } = new List<char>();

		public Pixel()
		{
			for (int i = 0; i < 3; i++)
			{
				Layer.Add(' ');
			}			
		}
	}

	enum AvailableProgs
	{
		Notepad, 
	}
}
