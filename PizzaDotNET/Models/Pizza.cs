using PizzaDotNET.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace PizzaDotNET.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        [ValidName(ErrorMessage = "Please capitalize the name")]
        public string Name { get; set; }
        [StringLength(255)]
        public string Toppings { get; set; }
        [Required(ErrorMessage = "Normal size price is required")]
        public int NormPrice { get; set; }
        [Required(ErrorMessage = "Family size price is required")]
        public int FamPrice { get; set; }

    }
}