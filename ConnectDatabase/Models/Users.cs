using System.ComponentModel.DataAnnotations;

namespace ConnectDatabase.Models
{
    public class Users
    {
        [Key]
        
        [Required(ErrorMessage = "StudentID is required")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "StudentID must be a positive integer")]

        public int StudentID { get; set; }
        [Required(ErrorMessage = "Score must be integer")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "ScoreAI must be a positive integer")]
        public float? Score { get; set; }

        public string? Evaluation { get; set; }

        [Required(ErrorMessage = "ScoreAI is required")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "ScoreAI must be a positive integer")]
        public float? ScoreAI { get; set; }
        [Required(ErrorMessage = "ScoreAI is required")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "ScoreAI must be a positive integer")]

        public string? Note { get; set; }
    }
}
