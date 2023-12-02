using System.ComponentModel.DataAnnotations;

namespace Electronick.Models
{
    public class Parameter
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string Name { get; set; }// назва виробника
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string Category { get; set; }//категогорія пристрою
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string Description { get; set; } // Опис компанії 
                                                // Зовнішній ключ

        public ICollection<Device>? Devices { get; set; }
    }
}
