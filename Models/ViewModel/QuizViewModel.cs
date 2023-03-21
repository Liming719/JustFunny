namespace JustFunny.Models.ViewModel
{
    public class QuizViewModel
    {
        public IEnumerable<Question> Questions { get; set; }
        public int CurrentIndex { get; set; }
    }
}
