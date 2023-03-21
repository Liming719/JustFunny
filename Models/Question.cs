using System.ComponentModel.DataAnnotations;

namespace JustFunny.Models
{
    public class Question
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Answer { get; set; }
        public string Type { get; set; } = "Others";
    }
}
