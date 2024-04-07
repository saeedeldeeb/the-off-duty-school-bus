using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;

namespace BusManagement.Core.Services;

public interface IAuthService
{
    Task<AuthorizedVM> RegisterAsync(RegisterDTO model);

    Task<AuthorizedVM> LoginAsync(LoginDTO dto);
}
