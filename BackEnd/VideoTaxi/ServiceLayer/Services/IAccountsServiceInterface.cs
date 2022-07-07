using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public interface IAccountsServiceInterface
    {
        Task<RegistrationResult> RegisterUser(Register registerUser);

        Task<LoginResult> LoginUser (Login loginUser);
    }
}
