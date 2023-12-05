using Electronick.Models;
using System.ComponentModel.DataAnnotations;

namespace Electronick.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string NameCategory { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string spacifications { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string waranty { get; set; }

        public CategoryViewModel() { }

        public CategoryViewModel(Category category)
        {
            Id = category.Id;
            NameCategory = category.NameCategory;
            spacifications = category.spacifications;
            waranty = category.waranty;
        }
    }
}
