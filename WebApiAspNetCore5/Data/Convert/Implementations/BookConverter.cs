using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Data.Convert.Contracts;
using WebApiAspNetCore5.Data.VO;
using WebApiAspNetCore5.Models;

namespace WebApiAspNetCore5.Data.Convert.Implementations
{
    public class BookConverter : IParse<BooksVO, Books>, IParse<Books, BooksVO>
    {
        public Books Parse(BooksVO origem)
        {
            if (origem == null)
            {
                return null;
            }

            Books books = new Books();
            books.id = origem.id;
            books.titulo = origem.titulo;
            books.descricao = origem.descricao;
            books.autor = origem.autor;
            books.data_publicacao = origem.data_publicacao;



            return books;
        }

        public List<Books> Parse(List<BooksVO> origem)
        {
            if (origem == null)
            {
                return null;
            }
            return origem.Select(item => Parse(item)).ToList();
        }

        public BooksVO Parse(Books origem)
        {
            if (origem == null)
            {
                return null;
            }

            BooksVO booksVO = new BooksVO();
            booksVO.id = origem.id;
            booksVO.titulo = origem.titulo;
            booksVO.descricao = origem.descricao;
            booksVO.autor = origem.autor;
            booksVO.data_publicacao = origem.data_publicacao;



            return booksVO;
        }

        public List<BooksVO> Parse(List<Books> origem)
        {
            if (origem == null)
            {
                return null;
            }
            return origem.Select(item => Parse(item)).ToList();
        }
    }
}
