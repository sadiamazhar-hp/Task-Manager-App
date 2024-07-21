using System.Drawing;

namespace TaskManager.Models
{
    public enum Roles
    {
        TeamLead,
        TeamMemeber
    }
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
    public class UserDto
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Roles Role { get; set; }

    }
}
