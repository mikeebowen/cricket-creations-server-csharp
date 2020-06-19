using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class User
    {
        //  id: this.number(null).nullable(),
        //name: {
        //  first: this.string (null).nullable(),
        //  last: this.string (null).nullable(),
        //},
        //email: this.string(null).nullable(),
        //avatar: this.string(null).nullable(),
        //blogPosts: this.hasMany(BlogPost, 'author_id'),
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}
