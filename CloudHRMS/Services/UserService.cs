using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CloudHRMS.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateAsync(string UserEmail)
        {
             var user = new IdentityUser { UserName = UserEmail, Email = UserEmail };
                    var result = await _userManager.CreateAsync(user, "CloudHRMS@prodev@123");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "EMPLOYEE");
                        return user.Id;
                    }
                    return null;

        }
    }
}