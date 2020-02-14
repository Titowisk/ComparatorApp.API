using System.ComponentModel.DataAnnotations;

namespace ComparatorApp.API.Dtos.BrandsDtos
{
    public class BrandForUpdatingDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300,
            MinimumLength = 2,
            ErrorMessage = "Item name must have a minimum of 2 characters.")]
        public string Name { get; set; }

    }
}