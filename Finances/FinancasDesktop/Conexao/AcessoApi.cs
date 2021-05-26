using FinancasDesktop.Classes;
using FinancasDesktop.Entities;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace FinancasDesktop.Conexao
{
    public static class AcessoApi
    {
        public static List<T> Get<T>(string endpoint)
        {
            HttpClientHandler handler = new HttpClientHandler();
            var url = "http://localhost:7300/api/v1/" + endpoint;

            try
            {
                using (var client = new HttpClient(handler))
                {   
                    var retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(5, i => TimeSpan.FromSeconds(5));
                    using (var response = retryPolicy.ExecuteAsync(() => client.GetAsync(url)))
                    {
                        if (response.Result.IsSuccessStatusCode)
                        {
                            var dados = response.Result.Content.ReadAsStringAsync().Result;
                            var retorno = JsonConvert.DeserializeObject<CommandResult>(dados);

                            if (!retorno.Success)
                            {
                                LogEx.GeraLogSimples(retorno.Data.ToString());
                            }

                            return JsonConvert.DeserializeObject<List<T>>(retorno.Data.ToString());
                        }
                        else if (response.Result.StatusCode == HttpStatusCode.NotFound)
                        {
                            LogEx.GeraLogSimples($"NotFound");
                            return null;
                        }
                        else
                        {
                            var dados = response.Result.Content.ReadAsStringAsync().Result;
                            var retorno = JsonConvert.DeserializeObject<CommandResult>(dados);

                            LogEx.GeraLogSimples($"Status: { retorno.Success}\nMensagem {retorno.Message}");
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.GerarLog();
                return null;
            }

        }        
        

    }
}
