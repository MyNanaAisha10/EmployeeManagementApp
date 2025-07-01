﻿using System.ComponentModel;

namespace Presentation.EmailServices;

public class EmailSettings
{
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FromName { get; set; } = default!;
    public string FromAddress { get; set; } = default!;
}
