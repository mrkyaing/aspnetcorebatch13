using AutoMapper;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Utilities
{
    public class CloudHRMSProfile: Profile
    {
        public CloudHRMSProfile()
        {
            CreateMap<AttendancePolicyEntity, AttendancePolicyViewModel>().ReverseMap();
            CreateMap<ShiftEntity, ShiftViewModel>().ReverseMap();
            CreateMap<ShiftAssignEntity, ShiftAssignViewModel>().ReverseMap();
            CreateMap<EmployeeEntity, EmployeeViewModel>().ReverseMap();
            CreateMap<AttendanceMasterEntity, AttendanceMasterViewModel>().ReverseMap(); 
            CreateMap<DepartmentEntity, DepartmentViewModel>().ReverseMap();
            CreateMap<PayrollEntity, PayrollViewModel>().ReverseMap();
        }
    }
}
