using Microsoft.Extensions.Logging;

namespace UCMS.Logging;

// simple file logger so we don't need Serilog or NLog
public class AppFileLogger : ILogger
{
    private readonly string _path;
    private static readonly object _lk = new();
    private static bool _fresh = true;

    public AppFileLogger(string path) => _path = path;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
    public bool IsEnabled(LogLevel level) => true;

    public void Log<TState>(LogLevel level, EventId id, TState state, Exception? ex,
        Func<TState, Exception?, string> fmt)
    {
        if (!IsEnabled(level)) return;

        var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {fmt(state, ex)}\n";
        if (ex != null) line += ex + "\n";

        lock (_lk)
        {
            if (_fresh)
            {
                File.WriteAllText(_path, line);
                _fresh = false;
            }
            else
            {
                File.AppendAllText(_path, line);
            }
        }
    }
}

public class AppFileLoggerProvider : ILoggerProvider
{
    private readonly string _path;
    public AppFileLoggerProvider(string path) => _path = path;
    public ILogger CreateLogger(string category) => new AppFileLogger(_path);
    public void Dispose() { }
}

public static class LoggingExt
{
    public static ILoggingBuilder AddFileLogger(this ILoggingBuilder b, string path)
    {
        b.AddProvider(new AppFileLoggerProvider(path));
        return b;
    }
}
