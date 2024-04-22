using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.BLL.Utilities.CustomExceptions;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.ntier.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class FriendController : ControllerBase
{
    private readonly IFriendService _friendService;

    public FriendController(IFriendService friendService)
    {
        _friendService = friendService;
    }

    [HttpGet("getfriends")]
    public async Task<IActionResult> GetFriends()
    {
        try
        {
            return Ok(await _friendService.GetFriendsAsync());
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong");
        }
    }


    [HttpPost("addfriend")]
    public async Task<IActionResult> AddFriend(FriendToAddDTO friendToAddDTO)
    {
        try
        {
            return Ok(await _friendService.AddFriendAsync(friendToAddDTO));
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong");
        }
    }


    [HttpDelete("deletefriend")]
    public async Task<IActionResult> DeleteFriend(int friendId)
    {
        try
        {
            await _friendService.DeleteFriendAsync(friendId);
            return Ok();
        }
        catch (UserNotFoundException)
        {
            return NotFound("Friend not found");
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong");
        }
    }
}
