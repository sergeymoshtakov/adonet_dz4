using dz4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Serialization;

namespace dz4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            Contact contact = new Contact
            {
                Name = "John Doe",
                MobilePhone = "123-456-7890",
                AlternativeMobilePhone = "987-654-3210",
                Email = "john.doe@example.com",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
            };

            return View(contact);
        }

        public ActionResult Show(Contact contact)
        {
            SaveToFile(contact, "contacts.xml");
            return View("Test", contact);
        }

        private void SaveToFile(Contact contact, String filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Contact));

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    serializer.Serialize(writer, contact);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving contact to file: {ex.Message}");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
