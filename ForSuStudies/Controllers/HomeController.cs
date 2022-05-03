using ForSuStudies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ForSuStudies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            string conStr = "";
            ISunNaHee sunNaHee = null;

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Users_son";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", MySqlDbType.VarChar).Value = "2b3b1cd5-06ba-4ab9-ac72-319e5944b504";


                    await connection.OpenAsync();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        sunNaHee = new ISunNaHee
                        {
                            Email = reader["Email"].ToString(),
                            Username = reader["UserName"].ToString()
                        };
                    }
                    await connection.CloseAsync();
                }
            }
            return View(sunNaHee);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
