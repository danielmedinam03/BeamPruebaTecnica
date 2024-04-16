using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.IRepositories;
using Repositories.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class LoginEventRepository : BaseRepository<int, LoginEvent>, ILoginEventRepository
    {
        public LoginEventRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> AddLoginSp(LoginEvent request)
        {
            try
            {
                _unitOfWork.GetContext().Database.ExecuteSqlInterpolated($"EXEC AddLoginEvent @Id={request.Id}, @UserId={request.UserId}, @HoraIngreso={request.HoraIngreso}, @Resultado={request.Resultado}");
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
