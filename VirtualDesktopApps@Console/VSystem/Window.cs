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
		private string url;

		private Pixel[,] renderBuffer;
		public ComponentsCollection Components { get; set; } = new ComponentsCollection();

		public Window(int width, int height, string sourceFileUrl)
		{
			Width  = width;
			Height = height;

			renderBuffer = new Pixel[width, height];

			Components.Add(new TitleBar(), "TitleBar");

			url = sourceFileUrl;

			InitRenderBuffer();
		}

		private void InitRenderBuffer()
		{
			using (StreamReader streamReader = new StreamReader(url))
			{
				for (int j = 0; j < Height; j++)
				{
					char[] currentCharArray = streamReader.ReadLine().ToCharArray();

					for (int i = 0; i < Width; i++)
					{
						renderBuffer[i, j] = new Pixel
						{
							DisplayCharacter = currentCharArray[i]
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
			for (int k = 0; k < Components.Count; k++)
			{
				Pixel[,] tempRenderBuffer = Components[k].GetRenderBuffer();

				for (int j = 0; j < tempRenderBuffer.GetLength(1); j++)
				{
					for (int i = 0; i < tempRenderBuffer.GetLength(0); i++)
					{
						renderBuffer[Components[k].Anchor.X + i, Components[k].Anchor.Y + j]
							= tempRenderBuffer[i, j];
					}
				}
			}
		}
		
		public bool ParseAndExecute(ConsoleKeyInfo key)
		{
			Button b = Components.GetHighlighted();

			if (b != null && b.ParseAndExecute(key))
			{
				SetRenderBuffer();

				return true;
			}

			/*
			 * Do something else
			 */

			return false;
		}
	}
}
