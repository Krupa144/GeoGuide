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
            {
                _logger.LogInformation("No lessons found in the database.");
                return NotFound("Brak lekcji do wyświetlenia.");
            }

            _logger.LogInformation("Successfully retrieved {Count} lessons.", response.Models.Count);

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
            _logger.LogError(ex, "Error retrieving lessons.");
            return StatusCode(500, new
            {
                message = "Wystąpił błąd podczas pobierania lekcji.",
                detail = ex.Message,
                innerException = ex.InnerException?.Message
            });
        }
    }

    [HttpPost("progress")]
    [Authorize]
    public async Task<IActionResult> SaveProgress([FromBody] LessonProgressDto progress)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        _logger.LogInformation("Attempting to save progress for UserId: {UserId} and LessonId: {LessonId}", userId ?? "[null]", progress.LessonId);

        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("Unauthorized access attempt: User ID not found in claims after authorization.");
            return Unauthorized("Identyfikator użytkownika nie został znaleziony po pomyślnej autoryzacji. Spróbuj zalogować się ponownie.");
        }

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
            {
                _logger.LogError("Upsert operation for progress returned no models, or failed silently.");
                return StatusCode(500, new { message = "Błąd: Zapis postępu nie powiódł się lub nie zwrócił danych.", detail = "Brak zwróconych modeli po Upsert." });
            }

            _logger.LogInformation("Progress saved/updated successfully for user {UserId} and lesson {LessonId}.", userId, progress.LessonId);
            return Ok(new { message = "Postęp zapisany pomyślnie!" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving progress for user {UserId} and lesson {LessonId}.", userId, progress.LessonId);
            return StatusCode(500, new
            {
                message = "Wystąpił błąd podczas zapisywania postępu.",
                detail = ex.Message,
                innerException = ex.InnerException?.Message
            });
        }
    }
}