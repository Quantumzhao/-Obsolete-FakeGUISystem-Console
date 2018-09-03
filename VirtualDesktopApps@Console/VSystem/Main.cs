using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SupplementaryClassLibraryForStringManipulation;

namespace VirtualDesktopApps_Console
{
	public class Launcher
	{
		public static ConsoleKeyInfo KeyPressed { get; set; }

		public static void Main(string[] args)
		{
			initiation();
			
			runNotepadTest();
			VSystem.SubPrograms[VSystem.SubPrograms.Count - 1].Window_Component.GetAppearance();
			VSystem.RenderAll();

			while (true)
			{
				KeyPressed = Console.ReadKey();
				Console.Clear();

				VSystem.ParseAndExecute(KeyPressed);
				VSystem.SubPrograms[VSystem.SubPrograms.Count - 1].Window_Component.GetAppearance();
				VSystem.RenderAll();
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
		}

		private static void runNotepadTest()
		{
			VSystem.SubPrograms.Add(new Notepad());

			VSystem.SubPrograms[VSystem.
				SubPrograms.Count - 1].IsComponentSelected = true;
		}
	}

	public class VSystem
	{
		public const int Width  = 125;
		public const int Height = 50;

		public static bool IsFocused { get; set; } = false;
		public static LayerCollection Layers { get; set; } = new LayerCollection();
		public static SubProgramCollection SubPrograms { get; set; } = new SubProgramCollection();

		public static void RenderAll()
		{
			for (int j = 0; j < Height; j++)
			{
				for (int i = 0; i < Width; i++)
				{
					int k = 0;
					
					while 
					(
						(Layers[k][i, j].DisplayCharacter == null) && 
						((Layers[k][i, j].ForegroundColor == ConsoleColor.Black) &&
						(Layers[k][i, j].BackgroundColor == ConsoleColor.White))
					)
					{
						if (k != Layers.Count - 1)
						{
							k++;
						}
						else
						{
							break;
						}
					}

					Console.BackgroundColor = Layers[k][i, j].BackgroundColor;
					Console.ForegroundColor = Layers[k][i, j].ForegroundColor;
					if (Layers[k][i, j].DisplayCharacter != null)
					{
						Console.Write(Layers[k][i, j].DisplayCharacter);
					}
					else
					{
						Console.Write(" ");
					}
				}

				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write("║");
				Console.WriteLine();
			}

			for (int i = 0; i < Width; i++)
			{
				Console.Write("═");
			}
			Console.Write("╝");
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
			for (int i = 0; i < SubPrograms.Count; i++)
			{
				if (SubPrograms[i].IsComponentSelected)
				{
					return SubPrograms[i];
				}
			}

			return null;
		}

		public void Start()
		{
			// For placeholder temporarily
		}
	}

	public class SubProgramCollection
	{
		private static List<SubProgram> subPrograms { get; set; } = new List<SubProgram>();

		public SubProgram this[int index]
		{
			get
			{
				return subPrograms[index];
			}

			set
			{
				subPrograms[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return subPrograms.Count;
			}
		}

		public void Add(SubProgram subProgram)
		{
			subPrograms.Add(subProgram);

			subPrograms[subPrograms.Count - 1].ProgramID = subPrograms.Count - 1;

			VSystem.Layers.Add(new Layer());
		}
	}

	public class LayerCollection
	{
		// The container of all layers

		public LayerCollection()
		{
			layers.Add(new Layer());
		}

		private List<Layer> layers= new List<Layer>();

		public int Count
		{
			get
			{
				return layers.Count;
			}
		}

		public Layer this[int index]
		{
			get
			{
				return layers[index];
			}

			set
			{
				layers[index] = value;
			}
		}

		public void Add(Layer layer)
		{
			layers.Add(layer);
		}
	}

	public class Layer
	{
		public Layer()
		{
			for (int j = 0; j < VSystem.Height; j++)
			{
				for (int i = 0; i < VSystem.Width; i++)
				{
					programLayer[i, j] = new Pixel();
				}
			}
		}

		private Pixel[,] programLayer= new Pixel[VSystem.Width, VSystem.Height];

		public Pixel this[int x, int y]
		{
			get
			{
				return programLayer[x, y];
			}

			set
			{
				programLayer[x, y] = value;
			}
		}
	}

	public class Pixel
	{
		public char? DisplayCharacter { get; set; } = null;

		public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Black;

		public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.White;
	}

	public class FocusCursor
	{
		// An abstract object

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

	public class Coordinates
	{
		public Coordinates(int x = 0, int y = 0)
		{
			X = x;
			Y = y;
		}

		public int X { get; set; }
		public int Y { get; set; }
	}
	
	interface IEntity
	{
		// All interactive units must implement the following properties in order to function

		Coordinates Anchor { get; set; }
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
