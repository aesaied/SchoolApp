using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{

    public class StudentCreateModel
    {

        public int? Id { get; set; } 


        [Required(ErrorMessage = "هذا  حقل متطلب")]
        [StringLength(20,MinimumLength =2)]
        [Display(Name ="First name", Prompt ="i.e Atallah Esaied")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string? LastName { get; set; }


    }
}
