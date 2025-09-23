using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCoreService.BL;
using BaseCoreService.Entities.DTO;

namespace BaseCoreService.BL
{
    public interface IUserBL : IBaseBL
    {
        Task<ServiceResponse> Register(RegisterInfo registerInfo);
    }
}
