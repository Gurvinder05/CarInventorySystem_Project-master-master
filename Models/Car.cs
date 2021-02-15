using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarInventorySystem_Project.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Manufacturing Date")]
        public DateTime Manufacturing_Date { get; set; }
        public string Comments { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        [Required]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        [Required]
        public int ModelId { get; set; }
        public CarModel Model { get; set; }
    }
}
