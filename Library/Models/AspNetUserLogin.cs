﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class AspNetUserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public Guid UserId { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
