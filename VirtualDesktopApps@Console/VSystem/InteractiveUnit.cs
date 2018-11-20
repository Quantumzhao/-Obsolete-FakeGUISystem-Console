using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesktopApps_Console
{
	delegate void ChangeHighLightDelegate();

	public class ComponentsCollection
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
		// Not recommended to use this as well
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
					if (components[i].Name.Equals(name))
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
		public void SetHighlighted(int index)
		{
			for (int i = 0; i < components.Count; i++)
			{
				if (i == index)
				{
					components[i].IsHighlighted = true;
				}
				else
				{
					components[i].IsHighlighted = false;
				}
			}
		}
		// though not recommended to use this
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
