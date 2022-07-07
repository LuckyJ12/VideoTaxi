using DataLayer.Models;
using ServiceLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLayer.DTO;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AccountsService : IAccountsServiceInterface
    {
        
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServiceInterface _tokenService;
        private readonly IRegisterUserRepositoryInterface _registerUserRepo;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountsService(
            UserManager<AppUser> userManager, 
            ITokenServiceInterface tokenService,
            IRegisterUserRepositoryInterface registerUserRepository,
            RoleManager<IdentityRole> roleManager
            )
            
        {
            
            _userManager = userManager;
            _tokenService = tokenService;
            _registerUserRepo = registerUserRepository; 
            _roleManager = roleManager; 

        }


        public async Task<RegistrationResult> RegisterUser(Register registerUser)
        {
            var userRole = "RegisteredUser";
            if (registerUser != null)
            {
                var user = new AppUser { 
                    SecurityStamp = Guid.NewGuid().ToString(), 
                    UserName = registerUser.Email, 
                    Email = registerUser.Email 
                };
                var result = await _userManager.CreateAsync(user, registerUser.Password);
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                if (result.Succeeded)
                {
                    await _roleManager.CreateAsync(new
                 IdentityRole(userRole));
                    await _userManager.AddToRoleAsync(user, userRole);
                   await _registerUserRepo.SaveToDb();
                }
            }
            return new RegistrationResult() { RegistrationSuccess = "Registration was succesfull" };
        }


        public async Task<LoginResult> LoginUser(Login loginUser)
        {

            var user = await _userManager.FindByNameAsync(loginUser.Email);
            var checkUserPass = await _userManager.CheckPasswordAsync(user, loginUser.Password);
            if (user == null || !checkUserPass) {
                return new LoginResult{ LoginFailure = "Login failed "};
            }
            var secToken = await _tokenService.GetTokenAsync(user);

            var jwt = new JwtSecurityTokenHandler().WriteToken(secToken);

            return new LoginResult{ LoginFailure = "", LoginSuccess = "Login was succesfull", Token = jwt};
    }
    }
}
