using DTO.AppUserDtos;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstracts
{
    public interface IAppUserDal : IGenericDal<AppUser>
    {
        Task UserRegisterAsync(RegisterAppUserDto dto);
    }
}
