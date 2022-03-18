using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Categories;

public class EditorCategoryViewModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [StringLength(40,
        MinimumLength = 3,
        ErrorMessage = "Este campo deve conter nomes com no mínimo três caracteres e no máximo quarenta")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo Slug é obrigatório")]
    public string Slug { get; set; }
}