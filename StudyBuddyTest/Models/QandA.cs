using System;
using System.Collections.Generic;

#nullable disable

namespace StudyBuddyTest.Models
{
    public partial class QandA
    {
        public QandA()
        {
            Favorites = new HashSet<Favorite>();
        }

        public int Qid { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}
