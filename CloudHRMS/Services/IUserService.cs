using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CloudHRMS.Services
{
    public interface IUserService
    {
        Task<string> CreateAsync(string UserEmail);
    }
}