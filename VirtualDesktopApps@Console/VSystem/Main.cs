using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	public class Launcher
	{
		public static ConsoleKeyInfo KeyPressed { get; set; }
		public static void Main(string[] args)
		{
			initiation();

			#region TestAddNotepadInstanceToRuntime

			VSystem.SubPrograms.Add(new Notepad());
			VSystem.Layers.Add(new Layer());

			#endregion

			try
			{
				while (true)
				{
					VSystem.Layers[VSystem.GetFocusedSubProgram().ProgramID].Update();
					VSystem.RenderAll();

					KeyPressed = Console.ReadKey();
					Console.SetCursorPosition(0, 0);

					VSystem.ParseAndExecute(KeyPressed);
				}
			}
			catch (Exception exception)
			{
				ShowMessage(EffectiveField.Global, MessageType.Error, "Unknown Error", true, exception);

				throw exception;
			}
		}

		private static void initiation()
		{
			try
			{
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.WindowWidth = Console.LargestWindowWidth;
				Console.WindowHeight = Console.LargestWindowHeight;
				Console.CursorVisible = false;

				// | I might intend to apply the same "Select & Focus" action to VSystem
				// | Anyway, this will be fixed in the future. 
				// V Just leave it untouched by now
				VSystem.IsFocused = true;
			}
			catch (Exception exception)
			{
				ShowMessage(EffectiveField.Global, MessageType.Error, "Initiation Failed", true, exception);
			}
		}

		private delegate void ShowErrorDelegate(params string[] messages);
		public static void ShowMessage(
			EffectiveField field, 
			MessageType type, 
			string message, 
			bool showDetailedErrorInfo, 
			Exception exception = null)
		{
			ShowErrorDelegate showErrorHandler;

			switch (field)
			{
				case EffectiveField.Global:
					showErrorHandler =
						(string[] messages) =>
						{
							Console.Clear();
							Console.SetCursorPosition(0, 0);
						};
					break;

				case EffectiveField.local:

					showErrorHandler =
						(string[] messages) =>
						{
							throw new NotImplementedException();
						};
					break;

				case EffectiveField.Toast:

					showErrorHandler =
						(string[] messages) =>
						{
							throw new NotImplementedException();
						};
					break;

				case EffectiveField.Hint:

					showErrorHandler =
						(string[] messages) =>
						{
							throw new NotImplementedException();
						};
					break;

				default:
					showErrorHandler = null;
					ShowMessage(EffectiveField.Global, MessageType.Error, "Invalid Command", false);
					break;
			}

			switch (type)
			{
				case MessageType.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.BackgroundColor = ConsoleColor.White;

					if (showDetailedErrorInfo)
					{
						try
						{
							showErrorHandler += Console.WriteLine;

							showErrorHandler(
								message,
								exception?.Message,
								exception?.InnerException.Message,
								exception?.TargetSite.ToString(),
								exception?.StackTrace,
								exception?.Source, 
								exception?.HResult.ToString(), 
								exception?.HelpLink);
						}
						catch (ArgumentNullException argumentNullException)
						{
							ShowMessage(
								EffectiveField.Global,
								MessageType.Error,
								"",
								true,
								argumentNullException);
						}						
					}

					break;

				case MessageType.Warning:
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.BackgroundColor = ConsoleColor.White;
					break;

				case MessageType.Information:
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.BackgroundColor = ConsoleColor.White;
					break;

				default:
					ShowMessage(EffectiveField.Global, MessageType.Error, "Invalid Command", false);
					break;
			}

			Console.Write(message);

			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.White;
		}
		
		public enum EffectiveField
		{
			Global, local, Toast, Hint
		}

		public enum MessageType
		{
			Error, Warning, Information
		}
	}

	public class VSystem
	{
		public const int Width  = 125;
		public const int Height = 50;

		public static bool IsFocused { get; set; } = false;
		public static AbstractCollection<Layer> Layers { get; set; } = new AbstractCollection<Layer>();
		public static AbstractCollection<SubProgram> SubPrograms { get; set; } = new AbstractCollection<SubProgram>();

		public static List<int[]> RenderBufferModificationQueue = new List<int[]>();

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
						(Layers[k][i, j].ForegroundColor == ConsoleColor.Black) &&
						(Layers[k][i, j].BackgroundColor == ConsoleColor.White)
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

		public void RenderPartially()
		{
			for (int index = 0; index < RenderBufferModificationQueue.Count; index++)
			{
				Console.CursorLeft = RenderBufferModificationQueue[index][0];
				Console.CursorTop = RenderBufferModificationQueue[index][1];

				for (int j = 0; j < RenderBufferModificationQueue[index][3]; j++)
				{
					for (int i = 0; i < RenderBufferModificationQueue[index][2]; i++)
					{
						int k = 0;

						while
						(
							(Layers[k][i, j].DisplayCharacter == null) &&
							(Layers[k][i, j].ForegroundColor == ConsoleColor.Black) &&
							(Layers[k][i, j].BackgroundColor == ConsoleColor.White)
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
				}
			}

			Console.CursorLeft = 0;
			Console.CursorTop  = 0;
		}
		
		public static bool ParseAndExecute(ConsoleKeyInfo keyPressed)
		{
			if (GetFocusedSubProgram() != null)
			{
				GetFocusedSubProgram().ParseAndExecute(keyPressed);
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

			//SubProgram p = GetFocusedSubProgram();

			//Coordinates c = p.Windows.GetHighlighted().Anchor;

			//Pixel[,] tempRenderBuffer = p.GetRenderBuffer();

			return true;
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
	
	public class Layer : INameable
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

		public string Name { get; set; }

		public int Index { get; set; }

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

		public void Update()
		{
			Window tempWindow = VSystem.SubPrograms[Index].Windows.GetHighlighted();

			Pixel[,] graphBuffer = tempWindow.GetRenderBuffer();

			int anchorX = tempWindow.Anchor.X;
			int anchorY = tempWindow.Anchor.Y;

			for (int j = 0; j < VSystem.Height; j++)
			{
				for (int i = 0; i < VSystem.Width; i++)
				{
					if (i >= anchorX && i < tempWindow.Width + anchorX &&
						j >= anchorY && j < tempWindow.Height + anchorY)
					{
						this[i, j] = graphBuffer[i - anchorX, j - anchorY];
					}
					else if (!this[i, j].Equals(new Pixel()))
					{
						this[i, j] = new Pixel();
					}
				}
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

	public class AbstractCollection<T> where T : INameable
	{
		protected List<T> collection = new List<T>();

		public delegate void moreAddActionDelegate(T element/*, object parent*/);
		/*
		protected moreAddActionDelegate moreAddActionHandler { get; set; }

		public AbstractCollection(moreAddActionDelegate method = null, object parent = null)
		{
			moreAddActionHandler = method;
		}
		*/
		public T this[int index]
		{
			get
			{
				try
				{
					return collection[index];
				}
				catch (IndexOutOfRangeException indexOutOfRangeException)
				{
					Launcher.ShowMessage(Launcher.EffectiveField.Global, Launcher.MessageType.Error, "", true, indexOutOfRangeException);

					throw indexOutOfRangeException;
				}
			}

			set
			{
				collection[index] = value;
			}
		}

		public T this[string name]
		{
			get
			{
				return (from element in collection
						where element.Name.Equals(name)
						select element).Single();
			}

			set
			{
				for (int i = 0; i < collection.Count; i++)
				{
					if (collection[i].Name.Equals(name))
					{
						collection[i] = value;
					}
				}
			}
		}

		public int Count
		{
			get
			{
				return collection.Count;
			}
		}

		public void Add(T element, string name = "", object parent = null)
		{
			element.Name = name.Equals("") ? $"{element.GetType().ToString()}{collection.Count}" : name;
			collection.Add(element);

			/*moreAddActionHandler?.Invoke(element, parent);*/
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

	public interface IComponent : IEntity
	{
		IEntity GetParent(ref object invoker);
	}
	
	public interface IEntity : IKeyEvent, INameable, IFocusable
	{
		Coordinates Anchor { get; set; }
		int Width { get; set; }
		int Height { get; set; }
		
		Pixel[,] GetRenderBuffer();
	}

	public interface IKeyEvent
	{
		bool ParseAndExecute(ConsoleKeyInfo key);
	}

	public interface INameable
	{
		string Name { get; set; }
	}

	public interface IFocusable
	{
		bool IsHighlighted { get; set; }
		bool IsFocused { get; set; }
	}

	enum AvailableProgs
	{
		Notepad, 
	}
}
