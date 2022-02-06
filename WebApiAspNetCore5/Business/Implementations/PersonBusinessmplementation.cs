using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiAspNetCore5.Data.Convert.Implementations;
using WebApiAspNetCore5.Data.VO;
using WebApiAspNetCore5.Models;
using WebApiAspNetCore5.Models.Context;
using WebApiAspNetCore5.Repository;

namespace WebApiAspNetCore5.Business.Implementations
{
    public class PersonBusinessmplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _ipersonRepository;
        private readonly PersonConverter _personConverter;


        public PersonBusinessmplementation(IRepository<Person> ipersonRepository)
        {
            this._ipersonRepository = ipersonRepository;
            this._personConverter = new PersonConverter();
        }
        public List<PersonVO> FindAll()
        {
            var persons = _ipersonRepository.FindAll();
            return _personConverter.Parse(persons);
        }



        public PersonVO FindByID(long id)
        {
            return _personConverter.Parse(_ipersonRepository.FindByID(id));
        }

        public PersonVO Create(PersonVO person)
        {
            var pessoaModel = _personConverter.Parse(person);
            var pessoadcionar = _ipersonRepository.Create(pessoaModel);
            return _personConverter.Parse(pessoadcionar);


        }
        public PersonVO Update(PersonVO person)
        {
            var pessoaModel = _personConverter.Parse(person);
            var pessoaUpdate = _ipersonRepository.Update(pessoaModel);
            return _personConverter.Parse(pessoaUpdate);

        }
        public void Delete(long id)
        {
            _ipersonRepository.Delete(id);

        }

    }
}
