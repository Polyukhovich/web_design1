using Electronick.Models;
using System.ComponentModel.DataAnnotations;

namespace Electronick.ViewModels
{
    public class ParameterViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string NameCompany { get; set; }// назва виробника
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string CategoryCompany { get; set; }//категогорія пристрою
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string Description { get; set; } // Опис компанії 
        public ParameterViewModel() { }
        public ParameterViewModel(Parameter parameter)
        {   Id = parameter.Id;
            NameCompany = parameter.NameCompany;
            CategoryCompany = parameter.CategoryCompany;
            Description = parameter.Description;
        }
        }                                   
}
