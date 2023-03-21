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
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Type { get; set; } = "Others";
    }
}
