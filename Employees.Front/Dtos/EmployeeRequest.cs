using System.ComponentModel.DataAnnotations;

namespace Employees.Front.Dtos
{
    public class EmployeeRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
