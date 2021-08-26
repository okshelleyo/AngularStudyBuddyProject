using System;
using System.Collections.Generic;

#nullable disable

namespace StudyBuddyTest.Models
{
    public partial class User
    {
        public User()
        {
            Favorites = new HashSet<Favorite>();
        }

        public string UserName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}
