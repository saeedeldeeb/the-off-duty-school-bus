using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;

namespace BusManagement.Core.Services;

public interface IProfileService
{
    Task<SchoolVM> GetSchoolProfile(Guid id);
    Task<CompanyVM> GetCompanyProfile(Guid id);
    Task<UserVM> GetUserProfile(Guid id);
    Task<UserVM> UpdateUserProfile(UserDTO user, Guid id);
    Task UpdateProfilePicture(string profilePicture, Guid id);
    Task<SchoolVM> UpdateSchoolProfile(SchoolDTO school, Guid id);
    Task<CompanyVM> UpdateCompanyProfile(CompanyDTO company, Guid id);
}
