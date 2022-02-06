using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models;
using WebApiAspNetCore5.Models.Base;

namespace WebApiAspNetCore5.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {

        T Create(T model);

        T FindByID(long id);

        T Update(T model);

        void Delete(long id);
        List<T> FindAll();
        bool Exists(long id);



    }
}
