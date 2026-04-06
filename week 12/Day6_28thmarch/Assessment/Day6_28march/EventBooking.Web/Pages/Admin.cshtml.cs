using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventBooking.Web.Pages;

public class AdminModel : PageModel
{
    public void OnGet()
    {
        // Auth is handled client-side via JWT in localStorage
    }
}
