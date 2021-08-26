using System;
using System.Collections.Generic;

#nullable disable

namespace StudyBuddyTest.Models
{
    public partial class Favorite
    {
        public int Qid { get; set; }
        public string UserName { get; set; }

        public virtual QandA QidNavigation { get; set; }
        public virtual User UserNameNavigation { get; set; }
    }
}
