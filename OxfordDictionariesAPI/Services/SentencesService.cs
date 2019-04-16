using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OxfordDictionariesAPI.Model;

namespace OxfordDictionariesAPI.Services
{
    public class SentencesService
    {
        private Uri ServerUrl = new Uri("https://od-api.oxforddictionaries.com/api/v1/entries/");

        public SentencesService() { }

        //Async get
        public async Task<List<SentecesGroup>> GetSentencesGroupAsync(TargetLanguage selectedTargetLanguage, Translation selectedTranslation)
        {
            String param = selectedTargetLanguage.Id + "/" + selectedTranslation.Text + "/sentences";
            return await GetAsync<List<SentecesGroup>>(new Uri(ServerUrl, param));
        }

        //Async call to get result
        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                PreResponse(client);
                HttpResponseMessage response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                Sentences result = new Sentences();
                try
                {
                    result = JsonConvert.DeserializeObject<Sentences>(json);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
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
