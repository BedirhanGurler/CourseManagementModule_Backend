using System.ComponentModel.DataAnnotations;

namespace Adaptteen.Common.DTOs
{
    public class CategoryData
    {
        public int numberOfAllCategories { get; set; }
        public int numberOfActiveCategories { get; set; }
        public int numberOfPassiveCategories { get; set; }
        public List<CategoryDto>? data { get; set; } = new List<CategoryDto>();
        public CategoryData() { }
    }
    public class CategoryDto
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Kategori adı girmek zorunludur!")]
        [MaxLength(50, ErrorMessage = "Kategori adı en fazla 50 karakter olabilir!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Kategori kodu girmek zorunludur!")]
        [MaxLength(12, ErrorMessage = "Kategori kodu en fazla 12 karakter olabilir!")]
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
