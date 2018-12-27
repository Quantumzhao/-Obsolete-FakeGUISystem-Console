using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	delegate void ChangeHighLightDelegate();

	public class ComponentCollection<T> : AbstractCollection<T> where T : IEntity
	{
		/*public ComponentCollection(moreAddActionDelegate method, object parent) : base(method, parent) { }*/

		public T GetFocused()
		{
			return (from element in collection
					where element.IsFocused == Focus.Focused
					select element).Single();
		}
		public void SetFocusing(int index)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				if (i == index)
				{
					collection[i].IsFocused = Focus.Focused;
				}
				else
				{
					collection[i].IsFocused = Focus.NoFocus;
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
					element.IsFocused = Focus.Focused;
				}
				else
				{
					element.IsFocused = Focus.NoFocus;
				}
			}
		}
	}

	public abstract class Button : IComponent
	{
		public Coordinates Anchor { get; set; } = new Coordinates();

		public int Width { get; set; }
		public int Height { get; set; }

		public string Name { get; set; }

		//private ChangeHighLightDelegate changeHighLightHandler;

		private IEntity parent;
		public IEntity GetParent(ref object invoker)
		{
			if (invoker is IEntity)
			{
				return parent;
			}
			else
			{
				throw new InvalidOperationException();
			}
		}
		public void SetParent(IEntity target)
		{
			if (parent == null)
			{
				parent = target;
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		private Focus isFocused;
		public Focus IsFocused
		{
			get
			{
				return isFocused;
			}

			set
			{
				switch (isFocused)
				{
					case Focus.Focusing:
					case Focus.Focused:
					case Focus.NoFocus:
						isFocused = value;
						break;
				}
			}
		}

		//private Pixel[,] renderBuffer;
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

		public Focus IsFocused { get; set; }

		public bool IsVisible { get; set; }

		//private Pixel[,] renderBuffer;
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
