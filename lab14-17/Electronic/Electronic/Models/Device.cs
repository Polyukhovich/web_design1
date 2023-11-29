

namespace Electronic.Models
{
    public class Device
    {// ID 
        public int Id { get; set; }
        //назва 
        public string Name { get; set; }
        // категорія
        public string Сategory { get; set; }
        // цiна
        public int Price { get; set; }
        //дата виходу 
        public string Date { get; set; }

        public int ParameterId { get; set; }
        public virtual Parameter Parameter { get; set; }

    }
}