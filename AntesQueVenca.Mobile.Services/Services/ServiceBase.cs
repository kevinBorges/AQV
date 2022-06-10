using System.Net.Http;

namespace AntesQueVenca.Mobile.Services.Services
{
    public class ServiceBase
    {
        protected HttpClient httpClient;
        protected string UrlServico = "http://api.antesquevenca.com.br/api/";

        public ServiceBase()
        {
            httpClient = new HttpClient();
        }
    }
}
