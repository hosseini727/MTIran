using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        Task DeleteAsync(int id);
        Task<List<Book>> GetAllAsync();

    }
}
