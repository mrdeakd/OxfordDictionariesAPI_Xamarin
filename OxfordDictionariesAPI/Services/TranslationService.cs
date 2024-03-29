﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OxfordDictionariesAPI.Model;

namespace OxfordDictionariesAPI.Services
{
    public class TranslationService
    {
        private Uri ServerUrl = new Uri("https://od-api.oxforddictionaries.com/api/v1/entries/");

        public TranslationService() { }

        //Async get
        public async Task<List<TranslationGroup>> GetTranslationGroupAsync(SourceLanguage selectedSourceLanguage, TargetLanguage selectedTargetLanguage, string word)
        {
            String param = selectedSourceLanguage.Id + "/" + word.ToLower() + "/translations=" + selectedTargetLanguage.Id;
            return await GetAsync<List<TranslationGroup>>(new Uri(ServerUrl, param));
        }

        //Async call to get result
        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                PreResponse(client);
                HttpResponseMessage response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                TranslationResult result = new TranslationResult();
                try
                {
                    result = JsonConvert.DeserializeObject<TranslationResult>(json);
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
