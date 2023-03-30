using BankSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BankSystem.Controllers
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
          string jsonFile = @"C:\Users\anujy\source\repos\BankSystem\DB.json";

            List<UserAccountModel>? items = new List<UserAccountModel>();
            string newJson = String.Empty;
            using (StreamReader r = new StreamReader(jsonFile))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<UserAccountModel>>(json);
            }

            return View("CustomerAccount", items);
        }

        public IActionResult CreateAccountView()
        {
            return View("CreateCustomerAccount");
        }

        public IActionResult CreateCustomerAccount(UserAccountModel userAccountModel)
        {
            
            List<UserAccountModel> items = new List<UserAccountModel>();
            string jsonFile = @"C:\Users\anujy\source\repos\BankSystem\DB.json";
            string newJson = String.Empty;
            using (StreamReader r = new StreamReader(jsonFile))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<UserAccountModel>>(json);
                if (items != null)
                {
                    items.Add(userAccountModel);
                }
                newJson = Newtonsoft.Json.JsonConvert.SerializeObject(items,
                               Newtonsoft.Json.Formatting.Indented);
            }
            using (StreamWriter writer = new StreamWriter(jsonFile))
            {
                writer.Write(newJson);
            }

            return View("CustomerAccount", items);
        }

        public IActionResult DeleteCustomerAccountView(UserAccountModel userAccountModel)
        {
            return View("DeleteCustomerAccount", userAccountModel);
        }

        public IActionResult DeleteCustomerAccount(int AccountNumber)
        {
            List<UserAccountModel>? items = new List<UserAccountModel>();
            string jsonFile = @"C:\Users\anujy\source\repos\BankSystem\DB.json";
            string newJson = String.Empty;
            using (StreamReader r = new StreamReader(jsonFile))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<UserAccountModel>>(json);
                var itemsToDeleted = items.FirstOrDefault(obj =>obj.AccountNumber == AccountNumber);
                items.Remove(itemsToDeleted);
                newJson = Newtonsoft.Json.JsonConvert.SerializeObject(items,
                               Newtonsoft.Json.Formatting.Indented);
            }
            using (StreamWriter writer = new StreamWriter(jsonFile))
            {
                writer.Write(newJson);
            }
            return View("CustomerAccount", items);
        }

        public IActionResult DepositAmountView()
        {
            return View("DepositAmount");
        }

        public IActionResult DepositAmount(int AccountNumber, int DepositAmount)
        {
            List<UserAccountModel>? items = new List<UserAccountModel>();
            string jsonFile = @"C:\Users\anujy\source\repos\BankSystem\DB.json";
            string newJson = String.Empty;
            if (DepositAmount <= 10000) {
                using (StreamReader r = new StreamReader(jsonFile))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<UserAccountModel>>(json);
                    var RecordforDeposit = items.FirstOrDefault(obj => obj.AccountNumber == AccountNumber);
                    items.Remove(RecordforDeposit);
                    RecordforDeposit.AccountBalance = RecordforDeposit.AccountBalance + DepositAmount;
                    items.Add(RecordforDeposit);
                    newJson = Newtonsoft.Json.JsonConvert.SerializeObject(items,
                                   Newtonsoft.Json.Formatting.Indented);
                }
                using (StreamWriter writer = new StreamWriter(jsonFile))
                {
                    writer.Write(newJson);
                }
                return View("CustomerAccount", items);
            }
            else
            {
                using (StreamReader r = new StreamReader(jsonFile))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<UserAccountModel>>(json);
                }
                return View("CustomerAccount", items);
            }
            
        }


        public IActionResult WithdrawAmountView()
        {
            return View("WithdrawAmount");
        }

        public IActionResult WithdrawalAmount(int AccountNumber, int WithdrawalAmount)
        {
            List<UserAccountModel>? items = new List<UserAccountModel>();
            string jsonFile = @"C:\Users\anujy\source\repos\BankSystem\DB.json";
            string newJson = String.Empty;
            using (StreamReader r = new StreamReader(jsonFile))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<UserAccountModel>>(json);
                var Recordforwithdrawal = items.FirstOrDefault(obj => obj.AccountNumber == AccountNumber);
                double percentagewithdrawal = ((double) WithdrawalAmount / Recordforwithdrawal.AccountBalance) * 100;
                int percentage = Convert.ToInt32(Math.Round(percentagewithdrawal, 0));
                if (percentagewithdrawal <= 90)
                {
                    items.Remove(Recordforwithdrawal);
                    Recordforwithdrawal.AccountBalance = Recordforwithdrawal.AccountBalance - WithdrawalAmount;
                    items.Add(Recordforwithdrawal);
                }
                newJson = Newtonsoft.Json.JsonConvert.SerializeObject(items,
                               Newtonsoft.Json.Formatting.Indented);
            }
            using (StreamWriter writer = new StreamWriter(jsonFile))
            {
                writer.Write(newJson);
            }
            return View("CustomerAccount", items);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}