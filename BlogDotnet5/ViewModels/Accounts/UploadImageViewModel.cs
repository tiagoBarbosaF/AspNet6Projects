using System.ComponentModel.DataAnnotations;

namespace BlogDotnet5.ViewModels.Accounts
{
    public class UploadImageViewModel
    {
        [Required(ErrorMessage = "Imagem inválida")]
        public string Base64Image { get; set; }
    }   
}