namespace Tiles;

internal static class BasicGuiManager
{
    public static Bitmap[]? MenuIcons;
    public static bool ExtraEffects;
}

internal class StandardButton : Button
{
    private const int ImageId = 28;

    public StandardButton()
    {
        Resize += (_, _) => HelperStuff.UpdateFont(this);
        TextChanged += (_, _) => HelperStuff.UpdateFont(this);

        BackgroundImage = BasicGuiManager.MenuIcons?[ImageId] ?? new Bitmap(10, 10);
        BackColor = Color.SlateGray;

        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderColor = Color.Yellow;
        FlatAppearance.BorderSize = 3;

        if (BasicGuiManager.ExtraEffects)
        {
            HelperStuff.SetupMouseEffects(this, true, true, true);
        }
    }
}

internal class StandardLabel : Label
{
    private const int ImageId = 29;

    public StandardLabel()
    {
        Resize += (_, _) => HelperStuff.UpdateFont(this);
        TextChanged += (_, _) => HelperStuff.UpdateFont(this);

        BackgroundImage = BasicGuiManager.MenuIcons?[ImageId] ?? new Bitmap(10, 10);
        BackColor = Color.Tan;
    }
}