using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System.Data;

namespace company1.Pages.report
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public DateTime start{ get; set; }
        [BindProperty]
        public DateTime end { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var claim_password = HttpContext.User.Claims.First(c => c.Type == "password");
            var password = claim_password.Value;
            var claim_login = HttpContext.User.Claims.First(c => c.Type == "login");
            var login = claim_login.Value;
            start = start.ToUniversalTime();
            end = end.ToUniversalTime();

            using (NpgsqlConnection connection = new NpgsqlConnection($"Server=127.0.0.1;User Id={login};Password={password};Database=company1;"))
            {
               
                 DataSet ds = new DataSet();
                 DataTable dt = new DataTable();
                 connection.Open();
                 NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM public.report('{name}','{start}','{end}')", connection);
                 cmd.CommandType = CommandType.Text;
                 NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                 adapter.Fill(ds);
                 dt = ds.Tables[0];


                var wb = new XLWorkbook();
                wb.Worksheets.Add(dt);

                string fileName = "Отчет.xlsx";

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                await using (var stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }

            }
            return RedirectToPage("./Index");
        }
    }
}
