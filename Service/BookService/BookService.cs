
using Domain.Entities;
using Infrastructure.Context;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BookService
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _DbContext;

        public async Task<bool> AddBook(Book model)
        {
            await _DbContext.AddAsync(model);
            return true;
        }

        //public async Task<bool> AddBook(Book model)
        //{
        //   await _DbContext.AddAsync <Book>(model);
        //}
    }
}
