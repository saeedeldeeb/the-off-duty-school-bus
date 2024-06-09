using BusManagement.Core.Data;
using BusManagement.Core.Repositories;
using BusManagement.Infrastructure.Context;
using BusManagement.Infrastructure.Repositories.Base;

namespace BusManagement.Infrastructure.Repositories;

public class RentRepository(AppDbContext context) : BaseRepository<Rent>(context), IRentRepository;
