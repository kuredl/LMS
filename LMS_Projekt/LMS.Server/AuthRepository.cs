using LMS_Server.DAL;
using LMS_Server.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LMS_Server {
    public class AuthRepository : IDisposable {
        private ApplicationContext _ctx;

        public ApplicationContext ctx { get { return _ctx; } }

        private UserManager<ApplicationUser> _userManager;

        public AuthRepository() {
            _ctx = new ApplicationContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel) {
            ApplicationUser user = new ApplicationUser {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password) {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose() {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}