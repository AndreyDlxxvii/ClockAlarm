using System;
using System.Globalization;
using System.Net;

public class GetTimeFromServer
{
    private TimeSpan _dateTime;

    private string[] _servers =
    {
        "http://www.ya.ru",
        "http://www.google.com",
        "http://www.rambler.ru",
        "http://www.rbc.ru"
    };

    public TimeSpan TryGetTimeFromServer()
    {
        foreach (var ell in _servers)
        {
            try
            {
                using var response = WebRequest.Create(ell).GetResponse();
                var time = DateTime.ParseExact(response.Headers["date"],
                    "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                    CultureInfo.InvariantCulture.DateTimeFormat,
                    DateTimeStyles.AssumeUniversal);
                _dateTime = time - time.Date;
                return _dateTime;
            }
            catch
            {
                // ignored
            }
        }

        return _dateTime;
    }
}
