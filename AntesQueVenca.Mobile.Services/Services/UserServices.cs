using AntesQueVenca.Domain.Entities;
using AntesQueVenca.Domain.Exceptions;
using AntesQueVenca.Domain.JsonConverters;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AntesQueVenca.Mobile.Services.Services
{
    public class UserServices:ServiceBase
    {
        public async Task<User> Add(User user) 
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new CustomConverter());

            var urlFormat = $"{UrlServico}User/PostUser";
            var json = JsonConvert.SerializeObject(user, settings);
            if (json == null)
                throw new ValidationException("Erro ao cadastrar usuário!");

            var response = await httpClient.PostAsync(urlFormat, new StringContent(json, Encoding.UTF8, "application/json"));
            if (response == null || !response.IsSuccessStatusCode)
                throw new ValidationException("Erro ao cadastrar usuário!");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(content);
        }
    }
}
