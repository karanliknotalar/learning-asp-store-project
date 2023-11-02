using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Infrastructure.Extensions;

public static class ControllerExtension
{
    public static void AddModelStateError(this Controller controller, IEnumerable<IdentityError> errors)
    {
        var identityErrors = errors as IdentityError[] ?? errors.ToArray();
        
        if (identityErrors.Any())
        {
            foreach (var error in identityErrors)
            {
                controller.ModelState.AddModelError(
                    error.Code,
                    $"Error Code: {error.Code} - Error Description: {error.Description}"
                );
            }
        }
    }
}