using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage ="Display order must be between 1-100")] //to set range and custom error message in ui
        public int DisplayOrder { get; set; }
    }
}
