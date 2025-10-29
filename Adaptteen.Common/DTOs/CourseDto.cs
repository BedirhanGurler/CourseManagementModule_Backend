using System.ComponentModel.DataAnnotations;

namespace Adaptteen.Common.DTOs
{
    public class CourseData
    {
        public int numberOfAllCourses { get; set; }
        public int numberOfActiveCourses { get; set; }
        public int numberOfPassiveCourses { get; set; }
        public List<CourseDto>? data { get; set; } = new List<CourseDto>();
        public CourseData() { }
    }
    public class CourseDto
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage ="Kategori seçimi zoruludur!")]
        public Guid? CategoryId { get; set; }

        [Required(ErrorMessage = "Ders adı girmek zorunludur!")]
        [MaxLength(100, ErrorMessage = "Ders adı en fazla 100 karakter olabilir!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Ders kodu girmek zorunludur!")]
        [MaxLength(12, ErrorMessage = "Ders kodu en fazla 12 karakter olabilir!")]
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public bool? IsActive { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
