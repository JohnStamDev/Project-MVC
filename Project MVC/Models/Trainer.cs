using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_MVC.Models
{
    public class Trainer
    {
        public int ID { get; set; }
        [RegularExpression("^([A-Z]+[a-zA-Z .&'-]+)$", ErrorMessage = "First name must start with capital and must be valid")]
        [Required(ErrorMessage = "You must enter a first name.")]
        [Display(Name = "First Name")]
        [StringLength(40, MinimumLength = 2)]
        public string FirstName { get; set; }
        [RegularExpression("^([A-Z]+[a-zA-Z .&'-]+)$", ErrorMessage = "Last name must start with capital and must be valid")]
        [Required(ErrorMessage = "You must enter a last name.")]
        [Display(Name = "Last Name")]
        [StringLength(40, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "You must enter a subject.")]
        [CustomSubjectValidation]
        public string Subject { get; set; }

    }
    public class Subjects
    {
        public static string[] subjects = new string[] { "C#", "Java", "Javascript", "SQL", "Python", "PHP", "Swift" };
    }
    public class CustomSubjectValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool result = Subjects.subjects.Contains(value.ToString());
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (!result)
            {
                string subjectTypes = "Subject should be any of : " + Subjects.subjects.Aggregate((a, b) => a + ", " + b);
                return new ValidationResult(subjectTypes);
            }

            return ValidationResult.Success;
        }
    }
}