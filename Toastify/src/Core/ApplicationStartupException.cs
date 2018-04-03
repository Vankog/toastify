using System;
using System.Collections.Generic;

namespace Toastify.Core
{
    public class ApplicationStartupException : ApplicationException
    {
        public ApplicationStartupException(string message) : base(message + Environment.NewLine + CreateMessage())
        {
        }

        private static string CreateMessage()
        {
            var nl = Environment.NewLine;

            // Touching Spotify.Instance forces instance creation, but it fails if called too early
            try
            {
                var tmp = Spotify.Instance;
            }
            catch (NullReferenceException)
            {
                return "Spotify instance status: not yet created.";
            }

            List<string> messages = new List<string>();
            messages.Add("Spotify instance running status: " + Spotify.Instance.IsRunning);

            var status = Spotify.Instance.Status;
            messages.Add("Spotify instance Status: ");
            if (status == null)
                messages.Add("null");
            else
            {
                // TODO: add a status.toString() to Spotify API and replace this:
                messages.Add("-- Online: " + status.Online);
                messages.Add("-- Running: " + status.Running);
                messages.Add("-- Version: " + status.Version);
                messages.Add("-- Client Version: " + status.ClientVersion);
                messages.Add("--  has Track: " + (status.Track != null));
            }

            return string.Join(nl, messages);
        }
    }
}