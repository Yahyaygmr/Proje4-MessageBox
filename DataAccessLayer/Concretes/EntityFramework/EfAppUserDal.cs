using DataAccessLayer.Abstracts;
using DataAccessLayer.Concretes.Context;
using DataAccessLayer.Concretes.Repository;
using DTO.AppUserDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concretes.EntityFramework
{
    public class EfAppUserDal : GenericRepository<AppUser>, IAppUserDal
    {
        private readonly UserManager<AppUser> _userManager;
        public EfAppUserDal(ApplicationContext context, UserManager<AppUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task UserRegisterAsync(RegisterAppUserDto dto)
        {
            AppUser appUser = new AppUser()
            {
                Name = dto.Name,
                Email = dto.Email,
                Surname = dto.Surname,
                UserName = dto.UserName,
            };
            await _userManager.CreateAsync(appUser, dto.Password);
        }
    }
}
