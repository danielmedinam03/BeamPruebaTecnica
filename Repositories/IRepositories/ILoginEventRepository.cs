using Models.Models;
using Repositories.IRepositories.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface ILoginEventRepository : IBaseReporitory<int, LoginEvent>
    {
    }
}
