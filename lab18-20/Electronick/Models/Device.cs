using System.ComponentModel.DataAnnotations;

namespace Electronick.Models
{
    public class Device
    {// ID 
        public int Id { get; set; }
        //назва 
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        // категорія
        public string Сategory { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        // цiна
        public int Price { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        //дата виходу 
        public DateTime Date { get; set; }
        public int ParameterId { get; set; }
        public  Parameter? Parameter { get; set; }
    }
}
