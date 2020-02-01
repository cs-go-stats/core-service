﻿using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.Communication.Transport
{
    public class RabbitMqConnectionConfiguration
    {
        public string Host { get; }

        public int Port { get; }

        public string Username { get; }

        public string Password { get; }

        public int Heartbeat { get; }

        public RabbitMqConnectionConfiguration(string host, int port, string username, string password, int heartbeat)
        {
            Host = host.NotNull(nameof(host));
            Port = port.Positive(nameof(port)).LessThanOrEqual(ushort.MaxValue, nameof(port));
            Username = username.NotNull(nameof(username));
            Password = password.NotNull(nameof(password));
            Heartbeat = heartbeat.Positive(nameof(heartbeat)).LessThanOrEqual(ushort.MaxValue, nameof(heartbeat));
        }
    }
}