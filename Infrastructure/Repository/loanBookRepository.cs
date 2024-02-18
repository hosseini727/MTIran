using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interface;
using Infrastructure.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class loanBookRepository : ILoanBookRepository
    {
        private readonly ApplicationDbContext _context;
        public loanBookRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        #region AddLendingBook
        /// <summary>
        /// امانت دادن کتاب 
        /// </summary>
        /// <param name="loanBook"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task AddLendingBook(LoanBook loanBook)
        {
            var isValid =  _context.LoanBooks
                         .Include(x => x.Book)
                         .FirstOrDefault(s => !s.IsReturn);

            if (isValid == null )
            {

                var findBook =  _context.Books.Find(loanBook.BookId);
                if (findBook != null)
                {
                     await _context.AddAsync(loanBook);
                     _context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("این کتاب به امانت رفته است و در کتابخانه نمیباشد");
                }
            }   
            else if (isValid != null  && isValid.IsReturn == true) 
            {
                var findBook = _context.Books.Find(loanBook.BookId);
                if (findBook != null)
                {
                    await _context.AddAsync(loanBook);
                    _context.SaveChanges();
                }
                
            }
            else
            {
                throw new InvalidOperationException("این کتاب به امانت رفته است و در کتابخانه نمیباشد");
            }
        }
        #endregion

        #region ReturningBook
        /// <summary>
        /// برگشت کتاب به کتابخانه 
        /// </summary>
        /// <param name="loanBook"></param>
        /// <returns></returns>
        public async Task ReturningBook(LoanBook loanBook)
        {
            var isValid = _context.LoanBooks.Where(s => s.BookId == loanBook.BookId).FirstOrDefault();
            isValid.IsReturn = true; 
            _context.SaveChangesAsync();
          
        }
        #endregion

        #region BorrowedBooks
        /// <summary>
        /// گزارش دو هفته اخیر کتابهای امتنت داده شده
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReportBookViewModel>> BorrowedBooks()
        {
            var twoWeeksAgo = DateTime.Today.AddDays(-14);
            var overdueLoanBooks = await _context.LoanBooks
                .Where(s => !s.IsReturn && s.loanDate >= twoWeeksAgo)
                .Include(lb => lb.Book).Include(cd=>cd.Member)  
                .ToListAsync();
            
            var reportBookViewModels = overdueLoanBooks.Select(item => new ReportBookViewModel
            {
                FullName = $"{item.Member.FirstName} {item.Member.LastName}",
                NCode = item.Member.NationalCode,
                PhoneNumber = item.Member.PhoneNumber,
                BookNo = item.Book.BookNo,
                Title = item.Book.Title,
                loanDate = item.loanDate
            }).ToList();

            return reportBookViewModels;
        }
        #endregion
    }
}
