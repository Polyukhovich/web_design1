using Electronick.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Electronick.Models
{
    public class Category

    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string NameCategory { get; set; }//Категорія
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string spacifications { get; set; }//технічні характеристики 
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string waranty { get; set; } //гарантія 

        public ICollection<Device>? Devices { get; set; }
        public Category() { }
        public Category(CategoryViewModel categoryViewModel)
        {
            Id = categoryViewModel.Id;
            NameCategory = categoryViewModel.NameCategory;
            spacifications = categoryViewModel.spacifications;
            waranty = categoryViewModel.waranty;
        }
    }
}
