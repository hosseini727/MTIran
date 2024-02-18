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
    public class MemberController : ControllerBase
    {
        #region Feild And Contructor

        private readonly IMemberRepository _memberRepository;
        private readonly ApplicationDbContext _applicationDbContext;

        public MemberController(IMemberRepository memberRepository, ApplicationDbContext applicationDbContext)
        {
            _memberRepository = memberRepository;
            _applicationDbContext = applicationDbContext;
        }
        #endregion



        [HttpPost("[action]")]
        public async Task<ActionResult> AddMember(MemberViewModel value)
        {
            try
            {
                Member member = new Member()
                {
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    NationalCode = value.NCode,
                    PhoneNumber = value.PhoneNumber
                };

                await _memberRepository.Add(member);
                return Ok("عملیات با موفقیت انجامشد");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> RemoveMember(int id)
        {
            try
            {
                await _memberRepository.DeleteAsync(id);
                return Ok("عملیات با موفقیت انجامشد");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
