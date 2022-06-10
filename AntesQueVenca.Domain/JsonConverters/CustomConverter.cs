using AntesQueVenca.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace AntesQueVenca.Domain.JsonConverters
{
    public class CustomConverter : JsonConverter<Person>
    {
        public override Person ReadJson(JsonReader reader, Type objectType, Person existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(PhysicalPerson))
                return new PhysicalPerson();
            else
                return new LegalPerson();
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] Person value, JsonSerializer serializer)
        {
            var json = JsonConvert.SerializeObject(value);
            writer.WriteValue(json);
        }

        //protected override Person Create(Type objectType, JObject jObject)
        //{
        //    if (objectType == typeof(PessoaFisica))
        //        return new PessoaFisica();
        //    else
        //        return new PessoaJuridica();
        //}
    }
}
