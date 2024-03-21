using DTO.AppUserDtos;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstracts
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task UserRegisterAsync(RegisterAppUserDto dto);
    }
}
