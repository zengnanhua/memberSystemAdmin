using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AdminSystem.Domain.Exceptions;

namespace AdminSystem.Infrastructure.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void AddUser(Zmn_Ac_User user)
        {
          
            if (user.IsTransient())
            {
                _context.Zmn_Ac_Users
                   .Add(user);
            }
        }
        /// <summary>
        /// 更加id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Zmn_Ac_User> GetUserByIdAsync(int id)
        {
            var zmn_ac_users = await _context.Zmn_Ac_Users.FindAsync(id);
            if (zmn_ac_users != null)
            {
                await _context.Entry(zmn_ac_users)
                    .Collection(i => i.UserRoleList).LoadAsync();
                await _context.Entry(zmn_ac_users).Collection(i => i.PermissionList).LoadAsync();
                await _context.Entry(zmn_ac_users).Reference(i => i.Address).LoadAsync();
            }
            return zmn_ac_users;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="zmd_ac_user"></param>
        public  void Update(Zmn_Ac_User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
        /// <summary>
        /// 根据手机号码查询用户
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Zmn_Ac_User GetUserByPhone(string phone)
        {
            return _context.Zmn_Ac_Users.Where(c => c.Phone == phone).FirstOrDefault();
        }
        /// <summary>
        /// 根据用户名查询用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Zmn_Ac_User GetUserByUserName(string userName)
        {
            return _context.Zmn_Ac_Users.Where(c => c.UserName == userName).FirstOrDefault();
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        public void Delete(Zmn_Ac_User user)
        {
            _context.Entry(user).State = EntityState.Deleted;
        }

    }
}
