using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace apilearning.Models
{
    public partial class MyAccount
    {
        [JsonIgnore] // 反序列化时候 忽略，用户输入时候忽略
        public uint Id { get; set; }
        public string AccountDescription { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        [IgnoreDataMember] // 序列化时候忽略 不返回给用户
        public string AccountPassword { get; set; } = null!;
        [JsonIgnore]
        public DateTime? CreateDate { get; set; }
        [JsonIgnore]
        public DateTime? ModifyDate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
