using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Supabase;
using GeoTipsBackend.Models.Data.Lessons;
using GeoTipsBackend.Models.Dtos.Lessons;
using static Supabase.Postgrest.Constants.Ordering;
using OperatorEnum = Supabase.Postgrest.Constants.Operator;

[ApiController]
[Route("api/lessons")]
public class LessonsController : ControllerBase
{
    private readonly Supabase.Client _supabase;
    private readonly ILogger<LessonsController> _logger;

    public LessonsController(Supabase.Client supabaseClient, ILogger<LessonsController> logger)
    {
        _supabase = supabaseClient;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons()
    {
        try
        {
            var response = await _supabase
                .From<Lesson>()
                .Order("id", Ascending)
                .Get();

            if (response.Models == null || !response.Models.Any())
                return NotFound("No lessons.");

            var lessonDtos = response.Models.Select(lesson => new LessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Content = lesson.Content,
                ImageUrl = lesson.ImageUrl
            }).ToList();

            return Ok(lessonDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving  list.");
            return StatusCode(500, new { message = "Server error while retrieving lessons.", detail = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<LessonDto>> GetLessonById(int id)
    {
        try
        {
            var response = await _supabase
                .From<Lesson>()
                .Filter("id", OperatorEnum.Equals, id.ToString())
                .Get();

            var lesson = response.Models.FirstOrDefault();

            if (lesson == null)
                return NotFound("Lesson with the given ID was not found.");

            return Ok(new LessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Content = lesson.Content,
                ImageUrl = lesson.ImageUrl
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving lesson with ID {Id}.", id);
            return StatusCode(500, new { message = "Server error while retrieving the lesson.", detail = ex.Message });
        }
    }

    [HttpPost("progress")]
    [Authorize]
    public async Task<IActionResult> SaveProgress([FromBody] LessonProgressDto progress)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User identifier not found.");

        try
        {
            var newProgress = new Progress
            {
                UserId = userId,
                LessonId = progress.LessonId,
                IsCompleted = true
            };

            var response = await _supabase
                .From<Progress>()
                .Upsert(newProgress);

            if (response.Models == null || !response.Models.Any())
                return StatusCode(500, new { message = "Progress failed." });

            return Ok(new { message = "Progress saved});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error {UserId} {LessonId}.", userId, progress.LessonId);
            return StatusCode(500, new { message = "Error saving progress.", detail = ex.Message });
        }
    }

    [HttpGet("user/progress")]
    [Authorize]
    public async Task<IActionResult> GetUserProgress()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User identifier not found.");

        try
        {
            var result = await _supabase
                .From<Progress>()
                .Filter("user_id", OperatorEnum.Equals, userId)
                .Filter("is_completed", OperatorEnum.Equals, "true")
                .Get();

            return Ok(result.Models.Select(p => new { lessonId = p.LessonId }));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving progress for user {UserId}.", userId);
            return StatusCode(500, new { message = "Server error while retrieving progress.", detail = ex.Message });
        }
    }

    [HttpGet("progress/{lessonId}")]
    [Authorize]
    public async Task<IActionResult> GetLessonProgress(int lessonId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User identifier not found.");

        try
        {
            var response = await _supabase
                .From<Progress>()
                .Filter("user_id", OperatorEnum.Equals, userId)
                .Filter("lesson_id", OperatorEnum.Equals, lessonId.ToString())
                .Filter("is_completed", OperatorEnum.Equals, "true")
                .Get();

            bool completed = response.Models != null && response.Models.Any();

            return Ok(new { completed });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving progress status for user {UserId} and lesson {LessonId}.", userId, lessonId);
            return StatusCode(500, new { message = "Server error.", detail = ex.Message });
        }
    }

    [HttpPut("progress/{lessonId}/uncomplete")]
    [Authorize]
    public async Task<IActionResult> UncompleteLessonProgress(int lessonId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User identifier not found.");

        try
        {
            var updateData = new Progress { IsCompleted = false };

            var updateResponse = await _supabase
                .From<Progress>()
                .Filter("lesson_id", OperatorEnum.Equals, lessonId.ToString())
                .Filter("user_id", OperatorEnum.Equals, userId)
                .Update(updateData);

            if (updateResponse.Models != null && updateResponse.Models.Any())
                return Ok(new { message = "Lesson progress marked as incomplete." });
            else
                return NotFound(new { message = "Progress not found for the specified lesson and user." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating progress status for user {UserId} and lesson {LessonId}.", userId, lessonId);
            return StatusCode(500, new { message = "Server error while updating progress.", detail = ex.Message });
        }
    }

    [HttpDelete("progress/{lessonId}")]
    [Authorize]
    public async Task<IActionResult> DeleteLessonProgress(int lessonId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User identifier not found.");

        try
        {
            await _supabase
                .From<Progress>()
                .Filter("lesson_id", OperatorEnum.Equals, lessonId.ToString())
                .Filter("user_id", OperatorEnum.Equals, userId)
                .Delete();

            return Ok(new { message = "Lesson progress successfully deleted." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting progress for user {UserId} and lesson {LessonId}.", userId, lessonId);
            return StatusCode(500, new { message = "Server error while deleting progress.", detail = ex.Message });
        }
    }
}
