using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {

        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        #region AddAsync
        public async Task AddAsync(Book book)
        {
            var res =  _context.Members.ToList();
            await _context.AddAsync(book);
            _context.SaveChanges();
        }
        #endregion 

        #region DeleteAsync
        public async Task DeleteAsync(int id)
        {
            var res = _context.Books.Find(id);
             _context.Remove(res);
            _context.SaveChangesAsync();
        }
        #endregion

        #region GetAllAsync
        public Task<List<Book>> GetAllAsync()
        {
            var res = _context.Books.ToList();
            return Task.FromResult(res);
        }
        #endregion
    }

   
}
