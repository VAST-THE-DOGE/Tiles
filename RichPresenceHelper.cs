using DiscordRPC;
using DiscordRPC.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tiles
{
	class RichPresenceHelper
	{
		private const string clientID = "1234208031168921732";
		private static DiscordRpcClient _client;
		private static DateTime startTime;
		private static System.Threading.Timer mainTimer;

		public static void Initialize()
		{
			_client = new DiscordRpcClient(clientID);
			
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
				Timestamps = new Timestamps {
				Start = startTime,
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
			startTime = DateTime.UtcNow;//.Subtract(new DateTime(1970, 1, 1));
		}
	}
}