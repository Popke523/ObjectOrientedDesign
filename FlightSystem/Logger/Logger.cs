using System.Globalization;
using System.Text;

namespace ObjectOrientedDesign.FlightSystem.Logger;

public class Logger(string logPath)
{
    public void Log(string message)
    {
        using var writer = new StreamWriter(Path.Combine(logPath,
            "log-" + DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)) + ".txt", true);

        var sb = new StringBuilder();
        sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF", CultureInfo.InvariantCulture));
        sb.Append(": ");
        sb.Append(message);

        writer.WriteLine(sb.ToString());
    }
}