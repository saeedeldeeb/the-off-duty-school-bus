using BusManagement.Core.Data;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Core.Services;
using BusManagement.Infrastructure.Context;
using BusManagement.Infrastructure.DataStructureMapping;
using Microsoft.EntityFrameworkCore;

namespace BusManagement.Infrastructure.Services;

public class ProfileService(AppDbContext context) : IProfileService
{
    public async Task<SchoolVM> GetSchoolProfile(Guid id)
    {
        var school = await context.Schools.Where(s => s.EmployeeId == id).FirstOrDefaultAsync();
        if (school == null)
        {
            throw new Exception("School not found");
        }
        return school.Parse<School, SchoolVM>();
    }

    public async Task<CompanyVM> GetCompanyProfile(Guid id)
    {
        var company = await context.Companies.Where(c => c.EmployeeId == id).FirstOrDefaultAsync();
        if (company == null)
        {
            throw new Exception("Company not found");
        }
        return company.Parse<Company, CompanyVM>();
    }

    public async Task<UserVM> GetUserProfile(Guid id)
    {
        var user = await context.Users.FindAsync(id.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return user.Parse<ApplicationUser, UserVM>();
    }

    public async Task<UserVM> UpdateUserProfile(UserDTO user, Guid id)
    {
        var userEntity = await context.Users.FindAsync(id.ToString());
        if (userEntity == null)
        {
            throw new Exception("User not found");
        }
        userEntity.Name = user.Name;
        user.ProfilePicture = user.ProfilePicture;
        await context.SaveChangesAsync();
        return userEntity.Parse<ApplicationUser, UserVM>();
    }

    public async Task UpdateProfilePicture(string profilePicture, Guid id)
    {
        var user = await context.Users.FindAsync(id.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }
        user.ProfilePicture = profilePicture;
        await context.SaveChangesAsync();
    }

    public async Task<SchoolVM> UpdateSchoolProfile(SchoolDTO school, Guid id)
    {
        var schoolEntity = await context
            .Schools.Where(s => s.EmployeeId == id)
            .FirstOrDefaultAsync();
        if (schoolEntity == null)
        {
            // School not found, create a new one
            schoolEntity = school.Parse<SchoolDTO, School>();
            schoolEntity.EmployeeId = id;
            context.Schools.Add(schoolEntity);
        }
        else
        {
            // School found, update the existing one
            var parsedSchool = school.Parse<SchoolDTO, School>();
            schoolEntity.Name = parsedSchool.Name;
            schoolEntity.Location = parsedSchool.Location;
            schoolEntity.Email = parsedSchool.Email;
            context.Schools.Update(schoolEntity);
        }
        await context.SaveChangesAsync();
        return schoolEntity.Parse<School, SchoolVM>();
    }

    public async Task<CompanyVM> UpdateCompanyProfile(CompanyDTO company, Guid id)
    {
        var companyEntity = await context
            .Companies.Where(c => c.EmployeeId == id)
            .FirstOrDefaultAsync();
        if (companyEntity == null)
        {
            // Company not found, create a new one
            companyEntity = company.Parse<CompanyDTO, Company>();
            companyEntity.EmployeeId = id;
            context.Companies.Add(companyEntity);
        }
        else
        {
            // Company found, update the existing one
            var parsedCompany = company.Parse<CompanyDTO, Company>();
            companyEntity.Name = parsedCompany.Name;
            companyEntity.Location = parsedCompany.Location;
            companyEntity.Email = parsedCompany.Email;
            context.Companies.Update(companyEntity);
        }
        await context.SaveChangesAsync();
        return companyEntity.Parse<Company, CompanyVM>();
    }
}
