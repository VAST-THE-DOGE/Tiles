class RichPresenceHelper
{
    //stuff for discord rich presence.
    //need the .dll file for this as well.
    private static long startTime;
    private static System.Threading.Timer mainTimer;
    private static Discord.Discord discord;
    //need a boolean for a weird bug:
    private static bool UsingActivity;

    //discord application ID for images and stuff.
    private static readonly string clientID = "1234208031168921732"; 
    public async static void UpdateActivity(string details, string state)
    {
        while (UsingActivity)
        {
            Thread.Sleep(50);
        }
        UsingActivity = true;
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = details,
                State = state,
                Timestamps =
                {
                    Start = startTime,
                },
                Assets =
                {
                    LargeImage = "tilesicon",
                    LargeText = "",
                    SmallImage = "",
                    SmallText = "",
                },
                Instance = true,
            };
            activityManager.UpdateActivity(activity, result => {} );
        }
        catch
        {}
        UsingActivity = false;
    }
    public static void DiscordTick(object state)
    {
        if (!UsingActivity)
        {
            UsingActivity = true;
            try{
                discord.RunCallbacks();
            } catch {}
            UsingActivity = false;
        }
        
    } 
    public static void UpdateStartTime()
    {
        startTime = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }
    //setup and return true if successful setup, false otherwise.
    public static bool SetupRichPresence()
    {
        try
        {
            discord = new Discord.Discord(long.Parse(clientID), (UInt64)Discord.CreateFlags.Default);
            discord.SetLogHook(Discord.LogLevel.Debug, (level, message) => {} );
            var activityManager = discord.GetActivityManager();
            var applicationManager = discord.GetApplicationManager();
            UpdateStartTime();
            UpdateActivity("Loading...", "");
            mainTimer = new System.Threading.Timer(DiscordTick, null, 0, 100);
            return true;
        } catch
        {
            return false;
        }
    }
}
