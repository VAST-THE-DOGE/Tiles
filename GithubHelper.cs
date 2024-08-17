using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tiles
{
    internal class GithubHelper
    {
        public const string GithubProject = "https://github.com/VAST-THE-DOGE/Tiles";

        public static void ShowError(Exception error, string errorLevel, string gameState)
        {
            var box = MessageBox.Show(
                $"An error occurred, do you wish to submit a generated bug report to help fix it?\nError:\n {error.Message}",
                "Report Error/Bug?", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (box == DialogResult.Yes)
            {
                try
                {
                    var title = $"Bug - {errorLevel} - {gameState} - {Program.VERSION}";

                    //TODO: fix this giant line of text (hard to edit and read):
                    var body =
                        $"**Tiles Generated Bug Report:**%0AVersion: {Program.VERSION}%0ASeverity: {errorLevel}%0AGame State: {GlobalVariableManager.GameStateMain} - {GlobalVariableManager.GameStateSecondary}%0A%0A**Error Source:**%0A`{(string.IsNullOrEmpty(error.Source) ? "Unknown" : error.Source)}`%0A%0A**Error Message:**%0A`{(string.IsNullOrEmpty(error.Message) ? "Unknown" : error.Message)}`%0A%0A**Stack Trace:**%0A`{(string.IsNullOrEmpty(error.StackTrace) ? "Unknown" : error.StackTrace).Replace("`", "\"")}`";

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName =
                            $"{GithubProject}/issues/new?title={title}&body={body}&labels=bug&assignees=vast-the-doge",
                        UseShellExecute = true
                    });
                }
                catch
                {
                }
            }
        }

        public static async Task CheckForUpdate()
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("Tiles"));
            IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("VAST-THE-DOGE", "Tiles");

            Regex pattern = new Regex(@"\d+(\.\d+)+");
            Match m = pattern.Match(releases[0].TagName);
            string version = m.Value;

            //Setup the versions
            Version latestGitHubVersion = new Version(version);
            Version localVersion = new Version(GlobalVariableManager.VERSION);

            int versionComparison = localVersion.CompareTo(latestGitHubVersion);
            if (versionComparison < 0)
            {
                //The version on GitHub is more up to date than this local release.
            }
            else if (versionComparison > 0)
            {
                MessageBox.Show(
                    $"You are using a pre release version! Expect bugs and unfinished features.\n    Latest Version: {latestGitHubVersion}\n    Current Version: {GlobalVariableManager.VERSION}",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //This local version is greater than the release version on GitHub.
            }
            else
            {
                return;
                //This local Version and the Version on GitHub are equal.
            }
        }
    }
}