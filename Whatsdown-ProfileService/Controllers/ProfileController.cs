using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Friend_Service.Views;
using Whatsdown_ProfileService.Data;
using Whatsdown_ProfileService.Logic;
using Whatsdown_ProfileService.Views;
using Whatsdown_ProfileService.caching;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Whatsdown_ProfileService.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        ProfileLogic logic;
        private readonly ILogger<ProfileController> _logger;
        private readonly IMemoryCache mCache;
        public ProfileController(ProfileContext context, ILoggerFactory logFactory, IMemoryCache memoryCache)
        {
            _logger = logFactory.CreateLogger<ProfileController>();
            logic = new ProfileLogic(context, memoryCache, logFactory.CreateLogger<ProfileLogic>());
        }
        // GET: api/<ProfileController>
        [HttpGet]
        public IActionResult GetUserProfiles(List<String> UserIds)
        {

            _logger.LogInformation("GetUserProfiles() method called");
            try
            {
                List<Profile>  profiles = logic.GetProfiles(UserIds);
                _logger.LogInformation("GetUserProfiles() method succesfull");
                return Ok(new { profiles = profiles });
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }

        }

        // GET api/<ProfileController>/5
        [HttpGet("{id}")]
        public IActionResult GetProfile(string id)
        {
            try
            {
                _logger.LogInformation($"CreateProfile() method called with parameter id: {id}");
                Profile profile = logic.GetProfileById(id);
                _logger.LogInformation("CreateProfile() method succesfull");
                return Ok(new { profile = profile });
            }catch(ArgumentException ex)
            {
                Console.WriteLine(ex);
                return BadRequest("Profile does not exist");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Unauthorized();
            }
        }


        // POST api/<ProfileController>
        [HttpPost]
        public IActionResult CreateProfile([FromBody] PostProfileView profileView)
        {
            _logger.LogInformation("CreateProfile() method called");
            _logger.LogDebug($"Creating Profile with following parameters: {profileView.displayName} , {profileView.gender} , {profileView.profileId}");

            try
            {
                logic.PostProfile(profileView);
                _logger.LogInformation("CreateProfile() method Succesfull");
                return Ok();
            }catch(ArgumentException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return Unauthorized();
            }
        }

     //This is the one
        [HttpGet("contact")]
        public IActionResult GetFriend(string name, string profileId)
        {
            try
            {
                _logger.LogInformation("GetFriend() method called");
                List<PotentialContactView> profiles = logic.GetProfilesByName(name, profileId);
 
                _logger.LogInformation("GetFriend() method succesfull");
                return Ok(new { profiles = profiles });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Unauthorized();
            }

        }

        // PUT api/<ProfileController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProfileController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
