using System.Text.Json;

class HelperStuff
{
    public static Bitmap NO_IMAGE_ICON;
    public static Bitmap ResizeImage(Bitmap oldImage, int newWidth, int newHeight)
    {
        var newImage = new Bitmap(newWidth, newHeight);
        try {
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                graphics.DrawImage(oldImage, new Rectangle(0, 0, newWidth+7, newHeight+7));
            }
        }
        catch
        {
            return oldImage;
        }
        oldImage.Dispose();
        return newImage;
    }
    public static Bitmap LoadImage(string name) {
        try
        {
                //need this new system to fix bug with world screenshots.
                using (var image = new Bitmap(Directory.GetCurrentDirectory()+@"\Data\ImageData\"+name+".png"))
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
            File.WriteAllText(Directory.GetCurrentDirectory()+@"\Data\"+Name+".json", JsonSerializer.Serialize(Info));
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

        float scaleFactor = Math.Min
        (control.Width / (textSize.Width), control.Height / textSize.Height);
        
        if (Math.Abs(scaleFactor - 1) < 0.20
            && textSize.Width < control.Width 
            && textSize.Height < control.Height 
            && control is not Button)
        {
            return;
        }

        if (control is Button)
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
    //gets a background using the ID in menu icons and a button for an outline.
    public static Bitmap GetLabelBackground(int width, int height, int iconID, Color outlineColor, int outlineSize)
    {
        Button tempButton = new Button();
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
        Button tempButton = new Button();
        tempButton.Width =  img.Size.Width;
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
        Bitmap controlBitmap = new Bitmap(control.Width, control.Height);
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
}