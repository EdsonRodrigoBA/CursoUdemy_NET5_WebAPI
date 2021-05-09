using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models;

namespace WebApiAspNetCore5.Business
{
    public interface IPersonBusiness
    {

        Person Create(Person person);

        Person FindByID(long id);

        Person Update(Person person);

        void Delete(long id);
        List<Person> FindAll();




    }
}
