using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OxfordDictionariesAPI.Model;

namespace OxfordDictionariesAPI.Services
{
    public class LanguagesService
    {
        private Uri ServerUrl = new Uri("https://od-api.oxforddictionaries.com/api/v1/languages");

        public LanguagesService() { }

        //Async get
        public async Task<List<LanguageGroup>> GetLanguageGroupAsync()
        {
            return await GetAsync <List<LanguageGroup>> (ServerUrl);
        }

        //Async call to get result
        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                PreResponse(client);
                HttpResponseMessage response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                Language result = JsonConvert.DeserializeObject<Language>(json);
                T retvalue = (T)result.Results;
                return retvalue;
            }
        }

        //Headers need to be set before call
        private static void PreResponse(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("app_id", "e76af6ba");
            client.DefaultRequestHeaders.Add("app_key", "7674d04a8f3508da02727fd518b940a8");
        }
    }
}
