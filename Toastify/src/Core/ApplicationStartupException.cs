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
            if (Spotify.Instance == null)
                return string.Join(nl, "No Spotify instance created.");

            List<string> messages = new List<string>();
            messages.Add("Spotify is running: " + Spotify.Instance.IsRunning);

            var status = Spotify.Instance.Status;
            messages.Add("Status: ");
            if (status == null)
                messages.Add("null");
            else
            {
                // TODO: add a status.toString() to Spotify API and replace this:
                messages.Add(nl + "-- Online: " + status.Online);
                messages.Add(nl + "-- Running: " + status.Running);
                messages.Add(nl + "-- Version: " + status.Version);
                messages.Add(nl + "-- Client Version: " + status.ClientVersion);
                messages.Add(nl + "--  has Track: " + (status.Track != null));
            }

            return string.Join(nl, messages);
        }
    }
}