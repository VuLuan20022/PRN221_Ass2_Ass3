using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRAss.Models
{
    public class AppUser
    {

        [Key]
        public int UserID { get; set; }


        public string FullName { get; set; }


        public string Address { get; set; }

    
        public string Email { get; set; }


        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
