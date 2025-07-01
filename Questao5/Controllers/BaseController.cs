using Microsoft.AspNetCore.Mvc;

namespace Questao5.Controllers;

public class BaseController : Controller
{
    [NonAction]
    public new IActionResult Response(object result = null, IReadOnlyCollection<string> notifications = null)
    {
        if (notifications == null || !notifications.Any())
        {
            try
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            catch
            {
                return BadRequest(new
                {
                    success = false,
                    errors = new[] { "Internal Server Error." }
                });
            }
        }
        else
        {
            return BadRequest(new
            {
                success = false,
                errors = notifications
            });
        }
    }
}
