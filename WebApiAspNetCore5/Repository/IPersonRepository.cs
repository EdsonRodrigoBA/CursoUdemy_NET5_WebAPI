using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models;

namespace WebApiAspNetCore5.Repository
{
    public interface IPersonRepository
    {

        Person Create(Person person);

        Person FindByID(long id);

        Person Update(Person person);

        void Delete(long id);
        List<Person> FindAll();
        bool Exists(long id);



    }
}
