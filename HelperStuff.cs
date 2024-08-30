using System.Drawing.Drawing2D;
using System.Reflection;
using System.Text.Json;
using Timer = System.Windows.Forms.Timer;

namespace Tiles;

using Timer = Timer;

public class HelperStuff
{
	public static Bitmap NO_IMAGE_ICON;
	public static Cursor[] cursors = new Cursor[3];

	public static Bitmap ResizeImage(Bitmap oldImage, int newWidth, int newHeight, bool disposeOldImage = true)
	{
		var newImage = new Bitmap(newWidth, newHeight);
		try
		{
			using (var graphics = Graphics.FromImage(newImage))
			{
				graphics.PixelOffsetMode = PixelOffsetMode.Half;
				//graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				graphics.DrawImage(oldImage, new Rectangle(0, 0, newWidth, newHeight));
			}
		}
		catch
		{
			return oldImage;
		}

		if (disposeOldImage)
			oldImage.Dispose();

		return newImage;
	}

	public static Bitmap LoadImage(string name)
	{
		try
		{
			//need this new system to fix bug with world screenshots.
			using (var image = new Bitmap(Directory.GetCurrentDirectory() + @"\Data\ImageData\" + name + ".png"))
			{
				return new Bitmap(image);
			}

			//var image = new Bitmap(Directory.GetCurrentDirectory()+@"\Data\ImageData\"+name+".png");
			//if (image == null)
			//{
			//    return NO_IMAGE_ICON;
			//}
			//else 
			//{
			//    return image;
			//}        
		}
		catch
		{
			return NO_IMAGE_ICON;
		}
	}

	public static void SaveToJson<T>(string Name, T Info)
	{
		SaveToJson(Name, Info, 1);
	}

	public async static Task SaveToJson<T>(string Name, T Info, int runCount)
	{
		//return after many tries to prevent lag. (keep at 1)
		if (runCount > 1)
		{
			return;
		}

		try
		{
			File.WriteAllText(Directory.GetCurrentDirectory() + @"\Data\" + Name + ".json",
				JsonSerializer.Serialize(Info));
		}
		catch
		{
			Thread.Sleep(250);
			SaveToJson(Name, Info, runCount++);
		}
	}

	public static void AppendToArray<T>(ref T[] array, T NewInfo)
	{
		T[] Temp = new T[array.Length + 1];
		Array.Copy(array, Temp, array.Length);
		Temp[Temp.Length - 1] = NewInfo;
		array = new T[Temp.Length];
		Array.Copy(Temp, array, Temp.Length);
	}

	public static void RemoveAtIndex<T>(ref T[] array, int index)
	{
		T[] Temp = new T[array.Length - 1];
		if (index > 0)
		{
			Array.Copy(array, 0, Temp, 0, index);
		}

		if (index < array.Length - 1)
		{
			Array.Copy(array, index + 1, Temp, index, array.Length - index - 1);
		}

		array = Temp;
	}

	//need to rework.
	public static void UpdateFont(Control control)
	{
		if (control is null)
		{
			return;
		}

		SizeF textSize = TextRenderer.MeasureText(control.Text, control.Font);

		//check for any error creating values.
		if (control.Width <= 0
		    || control.Height <= 0
		    || textSize.Width <= 0
		    || textSize.Height <= 0)
		{
			return;
		}

		var scaleFactor = Math.Min
			(control.Width / (textSize.Width), control.Height / textSize.Height);

		if (Math.Abs(scaleFactor - 1) < 0.20
		    && textSize.Width < control.Width
		    && textSize.Height < control.Height
		    && control is not Button)
		{
			return;
		}

		if (control is Button or StandardButton)
		{
			control.Font = new Font
				(control.Font.FontFamily, control.Font.Size * (scaleFactor * 0.55f), control.Font.Style);
		}
		else
		{
			control.Font = new Font
				(control.Font.FontFamily, control.Font.Size * (scaleFactor * 0.85f), control.Font.Style);
		}
	}

	public static async Task UpdateFontNew(Control control)
	{
		// Create a Graphics object for the button
		using (var g = control.CreateGraphics())
		{
			// Start with a large font size to scale down
			var fontSize = 30.0f;
			var testFont = new Font(control.Font.FontFamily, fontSize);

			// Measure the string size in the current font
			var stringSize = g.MeasureString(control.Text, testFont);

			// Calculate the font scaling factor
			var widthRatio = (float)Math.Floor(control.Width * 0.75) / stringSize.Width;
			var heightRatio = (float)Math.Floor(control.Height * 0.75) / stringSize.Height;
			var scaleRatio = Math.Min(widthRatio, heightRatio);

			// Scale the font size based on the ratio
			fontSize *= scaleRatio;

			// Set the button's font to the new size
			control.Font = new Font(control.Font.FontFamily, fontSize, FontStyle.Regular);
		}
	}

	//gets a background using the ID in menu icons and a button for an outline.
	public static Bitmap GetLabelBackground(int width, int height, int iconID, Color outlineColor, int outlineSize)
	{
		var tempButton = new Button();
		tempButton.Width = width;
		tempButton.Height = height;
		tempButton.BackgroundImage = Game.menuIcons[iconID];
		tempButton.FlatStyle = FlatStyle.Flat;
		tempButton.FlatAppearance.BorderSize = outlineSize;
		tempButton.FlatAppearance.BorderColor = outlineColor;
		return CaptureControlBitmap(tempButton);
	}

	//same as above, but uses a custom image to outline.
	public static Bitmap OutlineImage(Bitmap img, Color outlineColor, int outlineSize)
	{
		var tempButton = new Button();
		tempButton.Width = img.Size.Width;
		tempButton.Height = img.Size.Height;
		tempButton.BackgroundImage = img;
		tempButton.FlatStyle = FlatStyle.Flat;
		tempButton.FlatAppearance.BorderSize = outlineSize;
		tempButton.FlatAppearance.BorderColor = outlineColor;
		return CaptureControlBitmap(tempButton);
	}

	//to screenshot stuff
	public static Bitmap CaptureControlBitmap(Control control)
	{
		var controlBitmap = new Bitmap(control.Width, control.Height);
		control.DrawToBitmap(controlBitmap, new Rectangle(0, 0, control.Width, control.Height));
		return controlBitmap;
	}

	//stuff for async flags
	public static bool CheckFlags(bool[] flags)
	{
		return flags.All(flag => flag);
	}

	public static void WaitForFlags(bool[] flags, int delay)
	{
		while (!CheckFlags(flags))
		{
			Thread.Sleep(delay);
		}

		return;
	}

	//more array stuff
	public static int[] AddIntArrays(int[] array1, int[] array2, bool Subtract)
	{
		return LongToInt(
			AddLongArrays(
				array1.Select(i => (long)i).ToArray(),
				array2.Select(i => (long)i).ToArray(),
				Subtract));
	}

	public static long[] AddLongArrays(long[] array1, long[] array2, bool Subtract)
	{
		//find the length that both arrays will work with.
		int length;
		if (array1.Length > array2.Length)
		{
			length = array2.Length;
		}
		else
		{
			length = array1.Length;
		}

		//if subtract mode, flip the sign of array 2 elements.
		if (Subtract)
		{
			array2 = MultiplyLongArrayByDouble(array2, -1);
		}

		// go through each number and add/subtract
		for (var k = 0; k < length; k++)
		{
			array1[k] += array2[k];
		}

		return array1;
	}

	public static int[] MultiplyIntArrayByDouble(int[] array, double number)
	{
		return
			LongToInt(MultiplyLongArrayByDouble(array.Select(i => (long)i).ToArray(), number));
	}

	public static long[] MultiplyLongArrayByDouble(long[] array, double number)
	{
		// go through each number and multiply
		for (var k = 0; k < array.Length; k++)
		{
			array[k] = (int)Math.Floor(array[k] * number);
		}

		return array;
	}

	private static int[] LongToInt(long[] array)
	{
		//checks each value, does a cap if needed
		var intArray = new int[array.Length];

		for (var i = 0; i < array.Length; i++)
		{
			if (array[i] > int.MaxValue)
			{
				intArray[i] = int.MaxValue;
			}

			intArray[i] = (int)array[i];
		}

		return intArray;
	}

	public static void SetupMouseEffects(Control control, bool MouseHoverEffects, bool MouseClickEffects,
		bool ButtonBorderEffects)
	{
		if (MouseHoverEffects)
		{
			control.MouseEnter += (s, e) => { GlobalVariableManager.frame.Cursor = cursors[1]; };
			control.MouseLeave += (s, e) => { GlobalVariableManager.frame.Cursor = cursors[0]; };
		}

		if (MouseClickEffects)
		{
			control.MouseDown += (s, e) => { GlobalVariableManager.frame.Cursor = cursors[2]; };
			control.MouseUp += (s, e) => { GlobalVariableManager.frame.Cursor = cursors[1]; };
		}

		if (ButtonBorderEffects && control is Button button)
		{
			button.FlatStyle = FlatStyle.Flat;
			// tag = int array.
			// 0 = default border size, 1 = hover size, 2 = hovering over bool, 3 = sizing bool A, 4 = sizing bool B.
			button.Tag = new int[5];
			((int[])button.Tag)[0] = button.FlatAppearance.BorderSize;
			((int[])button.Tag)[1] = (int)Math.Ceiling(button.FlatAppearance.BorderSize * 1.5);

			control.MouseEnter += (s, e) =>
			{
				var button = s as Button;
				if (button != null && ((int[])button.Tag)[2] == 0 && ((int[])button.Tag)[3] == 0 &&
				    ((int[])button.Tag)[4] == 0)
				{
					((int[])button.Tag)[4] = 1;
					((int[])button.Tag)[2] = 1;
					if (button.FlatAppearance.BorderSize != ((int[])button.Tag)[0])
					{
						((int[])button.Tag)[0] = button.FlatAppearance.BorderSize;
					}

					var timer = new Timer();
					timer.Interval = 20; // Speed of the animation
					timer.Tick += (sender, args) =>
					{
						if (button.FlatAppearance.BorderSize < ((int[])button.Tag)[1] &&
						    ((int[])button.Tag)[3] == 0)
						{
							button.FlatAppearance.BorderSize += 1; // Increment the border size
						}
						else
						{
							timer.Stop();
							timer.Dispose();

							var mousePosition = button.PointToClient(Cursor.Position);

							((int[])button.Tag)[4] = 0;
							if (button.ClientRectangle.Contains(mousePosition))
							{
								button.FlatAppearance.BorderSize = ((int[])button.Tag)[1];
							}
							else
							{
								var onMouseLeaveMethod =
									typeof(Control).GetMethod("OnMouseLeave",
										BindingFlags.NonPublic | BindingFlags.Instance);
								onMouseLeaveMethod.Invoke(button, new object[] { EventArgs.Empty });
							}
						}
					};
					timer.Start();
				}
			};

			control.MouseLeave += (s, e) =>
			{
				var button = s as Button;
				if (button != null && ((int[])button.Tag)[2] == 1 && ((int[])button.Tag)[3] == 0 &&
				    ((int[])button.Tag)[4] == 0)
				{
					((int[])button.Tag)[4] = 1;
					((int[])button.Tag)[2] = 0;
					var timer = new Timer();
					timer.Interval = 20; //Speed of the animation (inverse)
					timer.Tick += (sender, args) =>
					{
						if (button.FlatAppearance.BorderSize > ((int[])button.Tag)[0] &&
						    ((int[])button.Tag)[3] == 0)
						{
							button.FlatAppearance.BorderSize -= 1;
						}
						else
						{
							timer.Stop();
							timer.Dispose();

							var mousePosition = button.PointToClient(Cursor.Position);

							((int[])button.Tag)[4] = 0;
							if (button.ClientRectangle.Contains(mousePosition))
							{
								var onMouseEnterMethod =
									typeof(Control).GetMethod("OnMouseEnter",
										BindingFlags.NonPublic | BindingFlags.Instance);
								onMouseEnterMethod.Invoke(button, new object[] { EventArgs.Empty });
							}
							else
							{
								button.FlatAppearance.BorderSize = ((int[])button.Tag)[0];
							}
						}
					};
					timer.Start();
				}
			};

			control.MouseDown += (s, e) =>
			{
				if (((int[])button.Tag)[2] == 1 && ((int[])button.Tag)[3] == 0)
				{
					button.FlatAppearance.BorderColor = Color.Red;
					button.FlatAppearance.BorderSize = ((int[])button.Tag)[1];
					((int[])button.Tag)[3] = 1;
				}
			};

			control.MouseUp += (s, e) =>
			{
				if (((int[])button.Tag)[2] == 1 && ((int[])button.Tag)[3] == 1)
				{
					button.FlatAppearance.BorderColor = Color.Yellow;
					((int[])button.Tag)[3] = 0;
				}
			};
		}
	}

	public static Task AnimatePanelBounds(Control panel, Rectangle newBounds, int duration)
	{
		var tcs = new TaskCompletionSource<object>();

		var timer = new Timer { Interval = duration / 30 };
		timer.Disposed += (sender, e) => tcs.SetResult(null);

		var stepCount = (double)duration / timer.Interval;
		var stepX = (newBounds.X - panel.Left) / stepCount;
		var stepY = (newBounds.Y - panel.Top) / stepCount;
		var stepWidth = (newBounds.Width - panel.Width) / stepCount;
		var stepHeight = (newBounds.Height - panel.Height) / stepCount;

		timer.Tick += (sender, args) =>
		{
			if (stepCount-- > 0)
			{
				panel.SetBounds(
					(int)(panel.Left + stepX),
					(int)(panel.Top + stepY),
					(int)(panel.Width + stepWidth),
					(int)(panel.Height + stepHeight));
			}
			else
			{
				panel.Bounds = newBounds;
				timer.Stop();
				timer.Dispose();
			}
		};
		timer.Start();

		return tcs.Task;
	}
}