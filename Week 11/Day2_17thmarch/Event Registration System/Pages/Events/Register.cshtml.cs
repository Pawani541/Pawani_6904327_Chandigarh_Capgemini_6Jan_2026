using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventRegistrationRP.Models;
using System.Collections.Generic;

namespace EventRegistrationRP.Pages.Events
{
    public class RegisterModel : PageModel
    {
        public static List<EventRegistration> registrations = new List<EventRegistration>();

        [BindProperty]
        public EventRegistration Registration { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Registration.Id = registrations.Count + 1;
                registrations.Add(Registration);

                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}