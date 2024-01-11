using cw.Data;
using System;
namespace NemishPOS.Data
{
    public class Orders
    {
        public List<Coffees> coffeeList { get; set; }
        public List<AddIns> addInsList { get; set; }
        public int totalAmount { get; set; }
        public int discount { get; set; }
        public string customerPhone { get; set; }
        public DateTime orderDate { get; set; }
        public int grandTotal { get; set; }
    }
}

