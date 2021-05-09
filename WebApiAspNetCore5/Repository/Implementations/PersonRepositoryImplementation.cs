using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models;
using WebApiAspNetCore5.Models.Context;

namespace WebApiAspNetCore5.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySqlContext mysqlContext;

        public PersonRepositoryImplementation(MySqlContext mysqlContext)
        {
            this.mysqlContext = mysqlContext;
        }
        public List<Person> FindAll()
        {
            var persons = mysqlContext.Persons.ToList();
            return persons;
        }



        public Person FindByID(long id)
        {
            return mysqlContext.Persons.FirstOrDefault(p => p.id.Equals(id));
        }

        public Person Create(Person person)
        {
            try
            {
                mysqlContext.Add(person);
                mysqlContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

            return person;
        }
        public Person Update(Person person)
        {
            if (!Exists(person.id))
            {
                return new Person();
            }

            var result = mysqlContext.Persons.FirstOrDefault(p => p.id.Equals(person.id));
            if (result != null)
            {
                try
                {
                    mysqlContext.Entry(result).CurrentValues.SetValues(person);
                    mysqlContext.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return person;
        }
        public void Delete(long id)
        {
            var result = mysqlContext.Persons.FirstOrDefault(p => p.id.Equals(id));
            if (result != null)
            {
                try
                {
                    mysqlContext.Persons.Remove(result);
                    mysqlContext.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

        }




        public bool Exists(long id)
        {
            return mysqlContext.Persons.Any(p => p.id.Equals(id));
        }
    }
}
