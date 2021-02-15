using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarInventorySystem_Project.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Brand")]
        public string Brand_Name { get; set; }

        public IList<CarModel> CarModels { get; } = new List<CarModel>();
    }
}
