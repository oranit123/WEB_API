using DamoFullPrj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamoFullPrj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
     



        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] LoginData ld)
        {
            try
            {
                User user = DBServices.Login(ld.emailLogin, ld.passLogin);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound($"Parent with email = {ld.emailLogin} and pass not found!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddParent([FromBody] User user)
        {
            try
            {
                bool isAdded = DBServices.AddParent(user);
                if (isAdded)
                {
                    return CreatedAtAction(nameof(AddParent), user);
                }
                else
                {
                    return BadRequest("Failed to add the parent.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditParent([FromBody] User user)
        {
            try
            {
                bool isEdited = DBServices.EditParent(user);
                if (isEdited)
                {
                    return Ok("Parent updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update the parent.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteParent(string id)
        {
            try
            {
                bool isDeleted = DBServices.DeleteParent(id);
                if (isDeleted)
                {
                    return Ok("Parent deleted successfully.");
                }
                else
                {
                    return NotFound($"Parent with id = {id} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllParents()
        {
            try
            {
                List<User> parents = DBServices.GetAllParents();
                return Ok(parents);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}
