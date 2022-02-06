using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models;

namespace WebApiAspNetCore5.Business
{
    public interface IBooksBusiness
    {

        BooksVO Create(BooksVO Books);

        BooksVO FindByID(long id);

        BooksVO Update(BooksVO Books);

        void Delete(long id);
        List<BooksVO> FindAll();




    }
}
