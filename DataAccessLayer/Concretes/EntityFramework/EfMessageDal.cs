using DataAccessLayer.Abstracts;
using DataAccessLayer.Concretes.Context;
using DataAccessLayer.Concretes.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concretes.EntityFramework
{
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
        public EfMessageDal(ApplicationContext context) : base(context)
        {
        }
    }
}
