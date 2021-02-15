using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarInventorySystem_Project.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Model")]
        public string Model_Name { get; set; }
        [Required]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        [Required]
        [Display(Name ="Model Start Date")]
        public DateTime Start_Date { get; set; }
        [Display(Name = "Model End Date")]
        public DateTime? End_Date { get; set; }

        public IList<Car> Cars { get; } = new List<Car>();
    }
}
