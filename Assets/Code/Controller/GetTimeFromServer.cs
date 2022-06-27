using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class GetTimeFromServer
{
    private TimeSpan _dateTime;
    private int _count = 0;

    private readonly string[] _servers = {
    "time-c.nist.gov",
    "time-d.nist.gov",
    "nist-time-server.eoni.com",
    "nist-time-server.eoni.com"
    };

    public TimeSpan TryGetTimeFromServer()
    {
        foreach (var server in _servers)
        {
            try
            {
                _count++;
                var client = new TcpClient(server, 13);
                var streamReader = new StreamReader(client.GetStream());
                var response = streamReader.ReadToEnd();
                var utcDateTimeString = response.Substring(16, 8);
                var dateTime = DateTime.ParseExact(utcDateTimeString, "HH:mm:ss",
                    CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
                _dateTime = dateTime - dateTime.Date;
                var t = DateTime.Now - DateTime.Now.Date;
                return _dateTime;
            }
            catch
            {
                if (_count >= _servers.Length)
                {
                    _dateTime = DateTime.Now.TimeOfDay;
                    return _dateTime;
                }
            }
        }
        return _dateTime;
    }
}

