using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF
{
    public abstract class BaseDataContext : DbContext
    {
        private readonly PostgreConnectionSettings _settings;

        protected BaseDataContext(PostgreConnectionSettings settings)
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_settings.GetConnectionString());

            if (_settings.IsAuditEnabled)
            {
                optionsBuilder.UseLoggerFactory(new ContextLoggerFactory());
            }
        }

        private class ContextLoggerFactory : ILoggerFactory
        {
            public void Dispose()
            {
            }

            public ILogger CreateLogger(string categoryName) => new ContextLogger();

            public void AddProvider(ILoggerProvider provider)
            {
            }
        }

        private class ContextLogger : ILogger
        {
            public void Log<TState>(
                LogLevel logLevel, 
                EventId eventId, 
                TState state, 
                Exception exception, 
                Func<TState, Exception, string> formatter) =>
                    Trace.WriteLine($"{logLevel}: {formatter(state, exception)}");

            public bool IsEnabled(LogLevel logLevel) => true;

            public IDisposable BeginScope<TState>(TState state) => LoggerScope.Scope;
        }

        private class LoggerScope : IDisposable
        {
            public static LoggerScope Scope { get; } = new LoggerScope();

            private LoggerScope()
            {
            }

            public void Dispose()
            {
            }
        }
    }
}