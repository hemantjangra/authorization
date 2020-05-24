namespace Authorization.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Repository.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/user/")]
    public class IdentityController : Controller
    {
        private readonly IUserRepository _userRepository;
        
        public IdentityController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody]Login loginDetails)
        {
            try
            {
                var result = await _userRepository.RegisterUser(loginDetails);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}