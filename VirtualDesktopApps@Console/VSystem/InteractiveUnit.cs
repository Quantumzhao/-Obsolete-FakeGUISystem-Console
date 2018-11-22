using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	delegate void ChangeHighLightDelegate();

	public class EntityCollection<T> : AbstractCollection<T> where T : IEntity
	{
		public T GetHighlighted()
		{
			return (from element in collection
					where element.IsHighlighted
					select element).Single();
		}
		public void SetHighlighted(int index)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				if (i == index)
				{
					collection[i].IsHighlighted = true;
				}
				else
				{
					collection[i].IsHighlighted = false;
				}
			}
		}

		// though not recommended to use this
		public void SetHighlighted(string name)
		{
			foreach (T element in collection)
			{
				if (element.Name.Equals(name))
				{
					element.IsHighlighted = true;
				}
				else
				{
					element.IsHighlighted = false;
				}
			}
		}
	}

	public abstract class Button : IEntity
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
					//changeHighLightHandler();

					IsFocused = true;
					isHighLighted = true;
				}
				else
				{
					isHighLighted = false;
				}
			}
		}

		private bool isFocused;
		public bool IsFocused
		{
			get
			{
				return isFocused;
			}

			set
			{
				if (!value)
				{
					IsHighlighted = false;
					isFocused = false;
				}
				else
				{
					isFocused = true;
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

	

	class PopUpMenu_Files : PopUpMenu
	{

	}
}
