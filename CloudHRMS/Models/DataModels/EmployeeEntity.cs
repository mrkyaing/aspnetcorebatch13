using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels
{
    [Table("Employee")]
    public class EmployeeEntity: BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOE { get; set; }
        public DateTime? DOR { get; set; }
        public decimal BasicSalary { get; set; }
        [MaxLength(1)]
        public string Gender { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? ProfilePicture { get; set; }
        
        
        //One-to-Many relationship between Employee and Department
        [ForeignKey(nameof(DepartmentId))]
        public string DepartmentId { get; set; }
        public virtual DepartmentEntity Department { get; set; }



        //One-to-Many relationship between Employee and Position
        [ForeignKey(nameof(PositionId))]
        public string PositionId { get; set; }
        public virtual PositionEntity Position { get; set; }
       
        
        //for relationship with User
       // [ForeignKey("UserId")]
        public string? UserId { get; set; }
    }
}
