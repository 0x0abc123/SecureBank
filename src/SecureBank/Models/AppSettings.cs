﻿using SecureBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureBank.Models
{
    public class AppSettings
    {
        public string BaseUrl { get; set; }

        public ApiEndpoint StoreEndpoint { get; set; }
        public SmtpCredentials SmtpCredentials { get; set; }
        public CtfConfig Ctf { get; set; }

        public bool IgnoreEmails { get; set; }
    }
}
