using DiscordRPC;
using Timer = System.Threading.Timer;

namespace Tiles;

internal static class RichPresenceHelper
{
	private const string ClientId = "1234208031168921732";
	private static DiscordRpcClient _client;
	private static DateTime _startTime;
	private static Timer _mainTimer;

	public static void Initialize()
	{
		_client = new DiscordRpcClient(ClientId);

		_client.OnReady += (sender, e) =>
		{
			//Console.WriteLine("Received Ready from user {0}", e.User.Username);
			var j = 0;
		};

		_client.OnPresenceUpdate += (sender, e) =>
		{
			//Console.WriteLine("Received Update! {0}", e.Presence);
			var j = 0;
		};

		//Connect to the RPC
		_client.Initialize();

		UpdateStartTime();
	}

	public static void DisposeClient()
	{
		_client.Dispose();
	}

	public static void UpdateActivity(string details, string state)
	{
		var activity = new RichPresence()
		{
			Details = details,
			State = state,
			Timestamps = new Timestamps
			{
				Start = _startTime,
			},
			Assets = new Assets()
			{
				LargeImageKey = "tilesicon",
				LargeImageText = "",
				SmallImageKey = "",
				SmallImageText = ""
			},
		};

		_client.SetPresence(activity);
	}

	public static void UpdateStartTime()
	{
		_startTime = DateTime.UtcNow; //.Subtract(new DateTime(1970, 1, 1));
	}
}