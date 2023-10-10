using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/userprofiles")]
[ApiController]
public class UserProfileController : ControllerBase
{
    private readonly List<UserProfile> _userProfiles = new List<UserProfile>();

    // GET: api/userprofiles
    [HttpGet]
    public ActionResult<IEnumerable<UserProfile>> Get()
    {
        return _userProfiles;
    }

    // GET: api/userprofiles/1
    [HttpGet("{id}")]
    public ActionResult<UserProfile> Get(int id)
    {
        var userProfile = _userProfiles.FirstOrDefault(u => u.Id == id);
        if (userProfile == null)
        {
            return NotFound();
        }
        return userProfile;
    }

    // POST: api/userprofiles
    [HttpPost]
    public ActionResult<UserProfile> Post(UserProfile userProfile)
    {
        userProfile.Id = _userProfiles.Count + 1;
        userProfile.CreatedAt = DateTime.Now;
        _userProfiles.Add(userProfile);
        return CreatedAtAction(nameof(Get), new { id = userProfile.Id }, userProfile);
    }

    // PUT: api/userprofiles/1
    [HttpPut("{id}")]
    public IActionResult Put(int id, UserProfile updatedProfile)
    {
        var existingProfile = _userProfiles.FirstOrDefault(u => u.Id == id);
        if (existingProfile == null)
        {
            return NotFound();
        }

        existingProfile.Username = updatedProfile.Username;
        existingProfile.Email = updatedProfile.Email;
        existingProfile.PasswordHash = updatedProfile.PasswordHash;

        return NoContent();
    }

    // DELETE: api/userprofiles/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var userProfile = _userProfiles.FirstOrDefault(u => u.Id == id);
        if (userProfile == null)
        {
            return NotFound();
        }

        _userProfiles.Remove(userProfile);
        return NoContent();
    }
}
