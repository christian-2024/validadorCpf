using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace httpValidaCpf
{
    public static class fnvalidacpf
    {
        [FunctionName("fnvalidacpf")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Iniciando a validação do CPF.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            if (data == null)
            {
                return new BadRequestObjectResult("Por favor, forneça um CPF no corpo da requisição.");
            }

            string cpf = data?.cpf;

            if( ValidateCpf(cpf) == false)
            {
                var responseMessage = "CPF inválido.";
                return new OkObjectResult(responseMessage);
            }
            else
            {
                var responseMessage = "CPF válido.";

            return new OkObjectResult(responseMessage);
        }
    }

        public static bool ValidateCpf(string cpf)
        {
            // Remove non-numeric characters
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Check basic rules
            if (cpf.Length != 11)
                return false;

            // Check if all digits are the same
            if (cpf.Distinct().Count() == 1)
                return false;

            // Calculate first verification digit
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * (10 - i);
            }

            int firstDigit = 11 - (sum % 11);
            if (firstDigit >= 10)
                firstDigit = 0;

            // Calculate second verification digit
            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * (11 - i);
            }

            int secondDigit = 11 - (sum % 11);
            if (secondDigit >= 10)
                secondDigit = 0;

            // Compare calculated digits with the actual digits
            return cpf[9].ToString() == firstDigit.ToString() &&
                   cpf[10].ToString() == secondDigit.ToString();
        }
    }
}

