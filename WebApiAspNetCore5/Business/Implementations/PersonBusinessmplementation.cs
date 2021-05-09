using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models;
using WebApiAspNetCore5.Models.Context;
using WebApiAspNetCore5.Repository;

namespace WebApiAspNetCore5.Business.Implementations
{
    public class PersonBusinessmplementation : IPersonBusiness
    {
        private readonly IPersonRepository _ipersonRepository;

        public PersonBusinessmplementation(IPersonRepository ipersonRepository)
        {
            this._ipersonRepository = ipersonRepository;
        }
        public List<Person> FindAll()
        {
            var persons = _ipersonRepository.FindAll();
            return persons;
        }



        public Person FindByID(long id)
        {
            return _ipersonRepository.FindByID(id);
        }

        public Person Create(Person person)
        {
            return Create(person);
              

        }
        public Person Update(Person person)
        {
            return Update(person);

        }
        public void Delete(long id)
        {
            _ipersonRepository.Delete(id);

        }

    }
}
