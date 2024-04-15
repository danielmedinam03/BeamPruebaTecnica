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
    }
}
