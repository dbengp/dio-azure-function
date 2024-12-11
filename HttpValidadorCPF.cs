using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ValidadorCPF.Functions
{
    public static class HttpValidadorCPF
    {
        [FunctionName("HttpValidadorCPF")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("função sem servidor Http trigger processando a requisição.");

            string requestBody = await new Stre amReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            if(data == null ){
                return new BadRequestObjectResult("Informe o CPF");
            }

            string cpf = data?.cpf;

            if (IsValidCPF(cpf)){

                responseMessage = "CPF Válido";
                return new OkObjectResult(responseMessage);

            }else{

                return new BadRequestObjectResult("O CPF está em formato inválido");
            }
            
        }

        public static bool IsValidCPF(string cpf)
        {
            if(string.IsNullOrEmpty(cpf))
            return false;
            
            string regexPattern = @"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$";
            bool isValidFormat = Regex.IsMatch(cpf, regexPattern);

            return isValidFormat;
        }
    }
}
