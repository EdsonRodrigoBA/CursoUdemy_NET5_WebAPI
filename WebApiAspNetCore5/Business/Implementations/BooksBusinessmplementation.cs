using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiAspNetCore5.Data.Convert.Implementations;
using WebApiAspNetCore5.Models;
using WebApiAspNetCore5.Models.Context;
using WebApiAspNetCore5.Repository;

namespace WebApiAspNetCore5.Business.Implementations
{
    public class BooksBusinessmplementation : IBooksBusiness
    {
        private readonly IRepository<Books> _iBooksRepository;
        private readonly BookConverter _bookConverter;


        public BooksBusinessmplementation(IRepository<Books> iBooksRepository)
        {
            this._iBooksRepository = iBooksRepository;
            this._bookConverter = new BookConverter();
        }
        public List<BooksVO> FindAll()
        {
            var Bookss = _iBooksRepository.FindAll();
            return _bookConverter.Parse(Bookss);
        }



        public BooksVO FindByID(long id)
        {
            return _bookConverter.Parse(_iBooksRepository.FindByID(id));
        }

        public BooksVO Create(BooksVO Books)
        {
            try
            {

                var pessoaModel = _bookConverter.Parse(Books);
                var modelCriado = _iBooksRepository.Create(pessoaModel);
              

                return _bookConverter.Parse(modelCriado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public BooksVO Update(BooksVO Books)
        {
            var Model = _bookConverter.Parse(Books);
            var modelCriado = _iBooksRepository.Update(Model);


            return _bookConverter.Parse(modelCriado);

        }
        public void Delete(long id)
        {
            _iBooksRepository.Delete(id);

        }

    }
}
