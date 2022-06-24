using System;
using System.IO;
using System.Net.Sockets;

public class GetTimeFromServer
{
    public int[] GetTime()
    {
        int[] time = new int[3];    
        var utcDateTimeString = AskServer("time-d.nist.gov");
        var utcDateTimeStringSecond = AskServer("time.nist.gov");
        for (int i = 0; i < utcDateTimeString.Length; i++)
        {
            time[i] = (utcDateTimeString[i] + utcDateTimeStringSecond[i]) / 2;
        }
        return time;
    }

    private int [] AskServer(string host)
    {
        int[] time = new int[3];
        var utcOffset = TimeZoneInfo.Local.BaseUtcOffset.Hours;
        var client = new TcpClient(host, 13);
        var streamReader = new StreamReader(client.GetStream());
        var response = streamReader.ReadToEnd();
        var utcDateTimeString = response.Substring(16, 8);
        string[] words = utcDateTimeString.Split(':');
        for (int i = 0; i < words.Length; i++)
        {
            time[i] = int.Parse(words[i]);
            if (i == 0)
            {
                time[i] += utcOffset;
            }
        }
        return time;
    }
}