using System;
using System.Collections.Generic;

namespace apilearning.Models
{
    public partial class AccountMovie
    {
        public long Id { get; set; }
        public string MovieHeroName { get; set; } = null!;
        public DateOnly MovieDate { get; set; }
        public string MovieFilmName { get; set; } = null!;
        public string MovieImg { get; set; } = null!;
        public string MovieEvaluate { get; set; } = null!;
        public string MovieContent { get; set; } = null!;
    }
}
