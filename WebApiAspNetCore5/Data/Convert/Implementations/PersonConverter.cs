using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Data.Convert.Contracts;
using WebApiAspNetCore5.Data.VO;
using WebApiAspNetCore5.Models;

namespace WebApiAspNetCore5.Data.Convert.Implementations
{
    public class PersonConverter : IParse<PersonVO, Person>, IParse<Person, PersonVO>
    {
        public Person Parse(PersonVO origem)
        {
            if (origem == null)
            {
                return null;
            }

            Person person = new Person();
            person.id = origem.id;
            person.firstname = origem.firstname;
            person.lastname = origem.lastname;
            person.address = origem.address;


            return person;
        }

        public List<Person> Parse(List<PersonVO> origem)
        {
            if (origem == null)
            {
                return null;
            }
            return origem.Select(item => Parse(item)).ToList();
        }

        public PersonVO Parse(Person origem)
        {
            if (origem == null)
            {
                return null;
            }
            PersonVO personVO = new PersonVO();
            personVO.id = origem.id;
            personVO.firstname = origem.firstname;
            personVO.lastname = origem.lastname;
            personVO.address = origem.address;


            return personVO;
        }

        public List<PersonVO> Parse(List<Person> origem)
        {
            if (origem == null)
            {
                return null;
            }
            return origem.Select(item => Parse(item)).ToList();
        }
    }
}
