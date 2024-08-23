using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Octokit;

namespace Tiles;

public class GithubHelper
{
	public const string GithubProject = "https://github.com/VAST-THE-DOGE/Tiles";
	public static int UpdateCheckStatus { get; private set; }

	public static void ShowError(Exception error, string errorLevel, string gameState)
	{
		var box = MessageBox.Show(
			$@"An error occurred, do you wish to submit a generated bug report to help fix it?\nError:\n {error.Message}",
			@"Report Error/Bug?", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
		if (box == DialogResult.Yes)
		{
			try
			{
				var title = $"Bug - {errorLevel} - {gameState} - {Program.VERSION}";

				var body =
					new StringBuilder()
						.Append("**Tiles Generated Bug Report:**%0AVersion: ")
						.Append(Program.VERSION)
						.Append("%0ASeverity: ")
						.Append(errorLevel)
						.Append("%0AGame State: ")
						.Append(GlobalVariableManager.GameStateMain)
						.Append(" - ")
						.Append(GlobalVariableManager.GameStateSecondary)
						.Append("%0A%0A**Error Source:**%0A`")
						.Append(string.IsNullOrEmpty(error.Source) ? "Unknown" : error.Source)
						.Append("`%0A%0A**Error Message:**%0A`")
						.Append(string.IsNullOrEmpty(error.Message) ? "Unknown" : error.Message)
						.Append("`%0A%0A**Stack Trace:**%0A`")
						.Append((string.IsNullOrEmpty(error.StackTrace) ? "Unknown" : error.StackTrace).Replace("`",
							"\""))
						.Append("`")
						.ToString();

				Process.Start(new ProcessStartInfo
				{
					FileName =
						$"{GithubProject}/issues/new?title={title}&body={body}&labels=bug&assignees=vast-the-doge",
					UseShellExecute = true
				});
			}
			catch
			{
				// ignored
			}
		}
	}

	public static async Task CheckForUpdate()
	{
		UpdateCheckStatus = 0;

		var client = new GitHubClient(new ProductHeaderValue("Tiles"));
		IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("VAST-THE-DOGE", "Tiles");

		var pattern = new Regex(@"\d+(\.\d+)+");
		var m = pattern.Match(releases[0].TagName);
		var version = m.Value;

		//Setup the versions
		var latestGitHubVersion = new Version(version);
		var localVersion = new Version(GlobalVariableManager.VERSION);

		var versionComparison = localVersion.CompareTo(latestGitHubVersion);
		UpdateCheckStatus = 1;

		if (versionComparison < 0)
		{
			//The version on GitHub is more up to date than this local release.
			var result = MessageBox.Show(
				$"You are using an old version, please update to experience any new features.\n\n\t\tLatest Version:\t{latestGitHubVersion}\n\t\tCurrent Version:\t{GlobalVariableManager.VERSION}",
				"Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

			if (result == DialogResult.Yes)
			{
				var result2 = MessageBox.Show(
					$"Updating has not been implemented, please do this process using Github.\nGithub Link: {GithubProject}/releases/tag/{releases[0].TagName} \nI hope to have this system working one day :)",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				if (result2 == DialogResult.OK)
				{
				}

				UpdateCheckStatus = 2;
			}
		}
		else if (versionComparison > 0)
		{
			//This local version is greater than the release version on GitHub.
			var result = MessageBox.Show(
				$"You are using a pre release version! Expect bugs and unfinished features.\n\n\tLatest Version:\t{latestGitHubVersion}\n\tCurrent Version:\t{GlobalVariableManager.VERSION}",
				"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			UpdateCheckStatus = 2;
		}
		else
		{
			//This local Version and the Version on GitHub are equal.
			UpdateCheckStatus = 3;
		}
	}
}