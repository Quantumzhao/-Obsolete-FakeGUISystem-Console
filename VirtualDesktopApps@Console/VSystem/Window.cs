using System.IO;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace VirtualDesktopApps_Console
{
	public class Window : IEntity
	{
		public Coordinates Anchor { get; set; } = new Coordinates(2, 2);
		public int Width { get; set; }
		public int Height { get; set; }
		public string Name { get; set; }
		public bool IsSelected { get; set; }
		public bool IsFocused { get; set; }
		private List<Button> InteractiveUnitsCollection { get; set; } = new List<Button>();
		private string url;

		public Pixel[,] RenderBuffer;

		//public delegate Pixel[,] GetAppearanceDelegate();
		//public GetAppearanceDelegate GetRenderBufferHandler;

		public Window(int width, int height, string sourceFileUrl)
		{
			Width  = width;
			Height = height;

			RenderBuffer = new Pixel[width, height];

			InteractiveUnitsCollection.Add(new TitleBar());

			url = sourceFileUrl;
			InitRenderBuffer();
		}

		private void InitRenderBuffer()
		{
			using (StreamReader streamReader = new StreamReader(url))
			{
				string currentLine;

				for (int j = 0; j < Height; j++)
				{
					currentLine = streamReader.ReadLine();

					for (int i = 0; i < Width; i++)
					{
						RenderBuffer[i, j] = new Pixel
						{
							DisplayCharacter = currentLine.ToCharArray()[i]
						};
					}
				}
			}
		}

		public Pixel[,] GetRenderBuffer()
		{
			return RenderBuffer;

			//GetAppearanceHandler = null;	//This subprocess is removed merely for test
		}
		public void SetRenderBuffer()
		{
			foreach (Button unit in InteractiveUnitsCollection)
			{
				Pixel[,] tempRenderBuffer = unit.GetRenderBuffer();

				for (int j = 0; j < tempRenderBuffer.GetLength(1); j++)
				{
					for (int i = 0; i < tempRenderBuffer.GetLength(0); i++)
					{
						RenderBuffer[unit.Anchor.X + i, unit.Anchor.Y + j] = tempRenderBuffer[i, j];
					}
				}
			}
		}

		internal Button GetSelectedComponent()
		{
			for (int i = 0; i < InteractiveUnitsCollection.Count; i++)
			{
				if (InteractiveUnitsCollection[i].IsSelected)
				{
					return InteractiveUnitsCollection[i];
				}
			}

			return null;
		}

		internal void AddComponent(Button component)
		{
			InteractiveUnitsCollection.Add(component);
		}

		internal Button GetComponent(string name)
		{
			return (from component in InteractiveUnitsCollection
					where component.Name.Equals(name)
					select component).Single();
		}

		public bool ParseAndExecute(ConsoleKeyInfo key)
		{
			Button b = GetSelectedComponent();

			if (b != null && b.ParseAndExecute(key))
			{
				SetRenderBuffer();
			}

			/*
			 * Do something else
			 */

			return false;
		}
	}
}
