using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interface;
using Infrastructure.Repository;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MTIran.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        #region Feild And Contructor
        private readonly IBookRepository _bookRepository;
        private readonly ApplicationDbContext _applicationDbContext;

        public BooksController(IBookRepository bookRepository, ApplicationDbContext applicationDbContext)
        {
            _bookRepository = bookRepository;
            _applicationDbContext = applicationDbContext;
        }
        #endregion 

        [HttpPost("[action]")]
        public async Task<ActionResult> AddBook(BookViewModel value)
        {
            try
            {
                Book book = new Book()
                {
                    BookNo = value.BookNo,
                    Title = value.Title,
                };

                await _bookRepository.AddAsync(book);
                return Ok("عملیات با موفقیت انجامشد");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("[action]")]
        public async Task<ActionResult> RemoveBook(int id)
        {
            try
            {
                await _bookRepository.DeleteAsync(id);
                return Ok("عملیات با موفقیت انجامشد");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllBooks()
        {
            try
            {
                var users = await _bookRepository.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
