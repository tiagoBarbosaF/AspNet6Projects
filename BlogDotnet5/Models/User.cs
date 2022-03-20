using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlogDotnet5.Models
{
    public class User
    {
        public User()
        {
            Posts = new List<Post>();
            Roles = new List<Role>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public List<Post> Posts { get; set; }
        public List<Role> Roles { get; set; }
    }
}