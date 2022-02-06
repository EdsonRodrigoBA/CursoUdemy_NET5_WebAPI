using System.Collections.Generic;
using WebApiAspNetCore5.Data.VO;

namespace WebApiAspNetCore5.Business
{
    public interface IPersonBusiness
    {

        PersonVO Create(PersonVO person);

        PersonVO FindByID(long id);

        PersonVO Update(PersonVO person);

        void Delete(long id);
        List<PersonVO> FindAll();




    }
}
