using Electronick.Models;
using System.ComponentModel.DataAnnotations;

namespace Electronick.ViewModels
{
    public class PurchaseViewModel
    {
        public int PurchaseId { get; set; }
        // ім'я й прізвище покупця
        [Required(ErrorMessage = "це поле повинно бути заповненне")]
        public string Person { get; set; }
        // адреса покупця
        [Required(ErrorMessage = "це поле повинно бути заповненне")]
        public string Address { get; set; }
        // ID електроніки
        public int DeviceId { get; set; }
        // дата покупки
        public DateTime Date { get; set; }
        public PurchaseViewModel() { }
        public PurchaseViewModel(Purchase purchase) 
        {
            PurchaseId = purchase.PurchaseId;
            Person = purchase.Person;
            Date = purchase.Date;
            DeviceId = purchase.DeviceId;

        }
    }
}
