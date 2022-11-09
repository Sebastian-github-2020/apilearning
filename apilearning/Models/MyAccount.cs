﻿using System;
using System.Collections.Generic;

namespace apilearning.Models
{
    public partial class MyAccount
    {
        public uint Id { get; set; }
        public string AccountDescription { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}