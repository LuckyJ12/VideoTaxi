using DataLayer;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO;
using ServiceLayer.Models;
using ServiceLayer.Services;

namespace VideoTaxi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

       
        private readonly IAccountsServiceInterface _accountsService;

        public AccountsController(IAccountsServiceInterface accountsService)
        {
          
            _accountsService = accountsService; 
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody]Register registerUser)
        {
            await _accountsService.RegisterUser(registerUser);
            return Ok(_accountsService.RegisterUser(registerUser).Result);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody]Login loginUser)
        {
            await _accountsService.LoginUser(loginUser);
            return Ok(_accountsService.LoginUser(loginUser).Result);
        }

       
    }
}
