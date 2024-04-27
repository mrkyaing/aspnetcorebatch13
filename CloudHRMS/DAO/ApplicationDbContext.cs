using CloudHRMS.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CloudHRMS.DAO
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser,IdentityRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> db):base(db){}
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<PositionEntity> Positions { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<DailyAttendanceEntity> DailyAttendances { get; set; }
        public DbSet<AttendancePolicyEntity> AttendancePolicyEntity { get; set; } 
        public DbSet<ShiftEntity> Shifts { get; set; }
        public DbSet<ShiftAssignEntity> ShiftAssigns { get; set; }
        public DbSet<AttendanceMasterEntity> AttendanceMasters { get; set; }
        public DbSet<PayrollEntity> Payrolls { get; set; }      
    }
}
