using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace apilearning.Models {
    /// <summary>
    /// 数据表模型
    /// </summary>
    public partial class User {


        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BornDate { get; set; }
    }
}
