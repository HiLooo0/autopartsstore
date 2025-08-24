using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoPartsStore.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; } // ID покупки
        public string Person { get; set; } // ПІБ покупця
        public string Address { get; set; } // Адреса
        public int PartId { get; set; } // ID запчастини
        public DateTime Date { get; set; } // Дата покупки
    }
}