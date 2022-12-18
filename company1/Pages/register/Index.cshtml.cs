using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System.Data;

namespace company1.Pages.register
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string login1 { get; set; }
        [BindProperty]
        public string password1 { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var claim_password = HttpContext.User.Claims.First(c => c.Type == "password");
            var password = claim_password.Value;
            var claim_login = HttpContext.User.Claims.First(c => c.Type == "login");
            var login = claim_login.Value;

            using (NpgsqlConnection connection = new NpgsqlConnection($"Server=127.0.0.1;User Id={login};Password={password};Database=company1;"))
            {
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand($"CREATE ROLE {login1} WITH  LOGIN NOSUPERUSER NOCREATEDB NOCREATEROLE INHERIT NOREPLICATION CONNECTION LIMIT - 1 PASSWORD '{password1}'; GRANT employees TO {login1}; ", connection);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                

            }
                return RedirectToPage("./Index");
        }
    }
}
