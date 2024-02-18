using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        public MemberRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        #region AddAsync
        public async Task Add(Member member)
        {
            await _context.AddAsync(member);
            _context.SaveChanges();
        }
        #endregion

        #region
        public async Task DeleteAsync(int id)
        {
            var res = _context.Members.Find(id);
            _context.Remove(res);
            _context.SaveChangesAsync();
        }
        #endregion

        #region
        public Task<List<Member>> GetAllAsync()
        {
            var res = _context.Members.ToList();
            return Task.FromResult(res);
        }
        #endregion
    }

}
