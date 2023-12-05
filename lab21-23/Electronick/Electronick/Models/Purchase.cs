using Electronick.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Electronick.Models
{
    public class Purchase
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

        public Purchase() { }
        public Purchase(PurchaseViewModel purchaseViewModel)
        {
            PurchaseId = purchaseViewModel.PurchaseId;
            Person = purchaseViewModel.Person;
            Address = purchaseViewModel.Address;
            DeviceId = purchaseViewModel.DeviceId;
            Date = purchaseViewModel.Date;
        }

    }
}
