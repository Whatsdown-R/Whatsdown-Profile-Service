using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Friend_Service.Views;
using Whatsdown_ProfileService.Data;
using Whatsdown_ProfileService.Logic;
using Whatsdown_ProfileService.Views;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Whatsdown_ProfileService.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        ProfileLogic logic;
        public ProfileController(ProfileContext context)
        {
            logic = new ProfileLogic(context);
        }
        // GET: api/<ProfileController>
        [HttpGet]
        public IActionResult GetUserProfiles(List<String> UserIds)
        {
            IActionResult response;
            List<Profile> profiles;
            try
            {
                profiles = logic.GetProfiles(UserIds);
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
                Profile profile = logic.GetProfileById(id);
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
           
            Console.WriteLine($"Creating Profile with following parameters: {0} , {1} , {2}", profileView.displayName, profileView.gender, profileView.profileId);
            try
            {
                logic.PostProfile(profileView);
                return Ok();
            }catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Unauthorized();
            }
        }

        [HttpGet("contact")]
        public IActionResult GetFriend(string name, string profileId)
        {
            try
            {
                List<PotentialContactView> profiles = logic.GetProfilesByName(name, profileId);
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
