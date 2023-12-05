using Electronick.Models;
using System.ComponentModel.DataAnnotations;
namespace Electronick.ViewModels
{
    public class DeviceViewModel
    {
        public int Id { get; set; }
        public string? Parameter { get; set; }
        //назва 
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        // категорія
        // цiна
        public int Price { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int ParameterId { get; set; }
        public string? Description { get; set; }
        public string? CategoryCompany { get; set; }
        public string? NameCompany { get; set; }


        public int CategoryId { get; set; }
        public string? NameCategory { get; set; }
        public string? spacifications {  get; set; }
        public string? waranty { get; set; }

        public DeviceViewModel() { }
        public DeviceViewModel(Device device)
        {
            Id = device.Id;
            Name = device.Name;
            Price = device.Price;
            Date = device.Date;
            ParameterId = device.ParameterId;
            if (device.Parameter != null)
            {
                ParameterId = device.Parameter.Id;
               Description = device.Parameter.Description;
              CategoryCompany = device.Parameter.CategoryCompany;
              NameCompany = device.Parameter.NameCompany;

            }
            if(device.Category != null)
            {
                CategoryId = device.Category.Id;
                NameCategory = device.Category.NameCategory;
                spacifications = device.Category.spacifications;
                waranty = device.Category.waranty;


            }
        }
    }
}
