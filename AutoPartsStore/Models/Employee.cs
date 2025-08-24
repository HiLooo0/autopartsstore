using System;
using System.ComponentModel.DataAnnotations; 

namespace AutoPartsStore.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name = "ПІБ")] 
        public string FullName { get; set; }

        [Display(Name = "Посада")] 
        public string Position { get; set; }

        [Display(Name = "Відділ")] 
        public string Department { get; set; }

        [Display(Name = "Дата прийняття")]
        public DateTime CreatedAt { get; set; }
    }
}