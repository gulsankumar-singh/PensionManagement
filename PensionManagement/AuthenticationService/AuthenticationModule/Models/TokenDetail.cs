﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationModule.Models
{
    public class TokenDetail
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
