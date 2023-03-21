using System.ComponentModel.DataAnnotations;

namespace JustFunny.Models
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        [Required]
        public string Email { get; set; } = String.Empty;
        public UserRole Role { get; set; }
        public bool Enabled { get; set; }
        public DateTime ModifyTime { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public enum UserRole
    {
        Teacher = 0,
        Student = 1,
        Admin
    }
}
