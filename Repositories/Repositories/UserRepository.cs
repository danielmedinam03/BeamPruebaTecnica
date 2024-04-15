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
    public class UserRepository : BaseRepository<long, User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> DeleteUserById(long Id)
        {
            
            var data = await FindByIdAsync(Id);

            data.Eliminado = true;

            await UpdateAsync(data);
            
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
