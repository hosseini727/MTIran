using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interface;
using Infrastructure.Repository;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MTIran.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanBookController : ControllerBase
    {
        #region Feild And Contructor
        private readonly ILoanBookRepository _loanBookRepository;
        private readonly ApplicationDbContext _applicationDbContext;
        public LoanBookController(ILoanBookRepository loanBookRepository, ApplicationDbContext applicationDbContext)
        {
            _loanBookRepository = loanBookRepository;
            _applicationDbContext = applicationDbContext;
        }
        #endregion 


        [HttpPost("[action]")]
        public async Task<ActionResult> AddLendingBook(LoanBookViewModel value)
        {
            try
            {
                LoanBook book = new LoanBook()
                {
                    MemberId = value.MemberId,
                    BookId = value.BookId,
                    loanDate = DateTime.Now,
                    IsReturn = false,
                };

                await _loanBookRepository.AddLendingBook(book);
                return Ok("عملیات با موفقیت انجامشد");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }                      
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ReturningBookLibrary(LoanBookViewModel value)
        {
            try
            {
                LoanBook book = new LoanBook()
                {
                    MemberId = value.MemberId,
                    BookId = value.BookId,
                };

                await _loanBookRepository.ReturningBook(book);
                return Ok("عملیات با موفقیت انجامشد");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<ReportBookViewModel>> ReportBorrowedBooks()
        {
            try
            {
                var listBook = await _loanBookRepository.BorrowedBooks();

                return Ok(listBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
