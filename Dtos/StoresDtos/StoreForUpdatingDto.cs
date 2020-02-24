using System.ComponentModel.DataAnnotations;

namespace ComparatorApp.API.Dtos.StoresDtos
{
    public class StoreForUpdatingDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(300,
            MinimumLength = 2,
            ErrorMessage = "Store name must have a minimum of 2 characters.")]
        public string Name { get; set; }
    }
}