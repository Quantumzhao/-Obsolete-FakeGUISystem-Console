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
		public bool IsHighlighted { get; set; }
		public bool IsFocused { get; set; }
		private ComponentsCollection components { get; set; } = new ComponentsCollection();
		private string url;

		private Pixel[,] renderBuffer;

		//public delegate Pixel[,] GetAppearanceDelegate();
		//public GetAppearanceDelegate GetRenderBufferHandler;

		public Window(int width, int height, string sourceFileUrl)
		{
			Width  = width;
			Height = height;

			renderBuffer = new Pixel[width, height];

			components.Add(new TitleBar(), "TitleBar");

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
						renderBuffer[i, j] = new Pixel
						{
							DisplayCharacter = currentLine.ToCharArray()[i]
						};
					}
				}
			}
		}

		public Pixel[,] GetRenderBuffer()
		{
			return renderBuffer;
		}
		public void SetRenderBuffer()
		{
			for (int k = 0; k < components.Count; k++)
			{
				Pixel[,] tempRenderBuffer = components[k].GetRenderBuffer();

				for (int j = 0; j < tempRenderBuffer.GetLength(1); j++)
				{
					for (int i = 0; i < tempRenderBuffer.GetLength(0); i++)
					{
						renderBuffer[components[k].Anchor.X + i, components[k].Anchor.Y + j]
							= tempRenderBuffer[i, j];
					}
				}
			}
		}

		internal void AddComponent(Button component, string name = "")
		{
			components.Add(component, name);
		}

		internal Button GetComponent(string name)
		{
			return components[name];
		}
		
		public bool ParseAndExecute(ConsoleKeyInfo key)
		{
			Button b = components.GetHighlighted();

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
