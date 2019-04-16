using System;
using OxfordDictionariesAPI.Services;
using OxfordDictionariesAPI.Model;
using System.Collections.ObjectModel;
using static OxfordDictionariesAPI.Model.SynonimsAntonymsGroup;

namespace OxfordDictionariesAPI.ViewModel
{
    public class DetailPageViewModel : BaseViewModel
    {
        //UI
        private bool isLoading = true;
        private bool mainGrid = false;
        public Boolean IsLoading { get => isLoading; set => IsLoadingFunction(value); }
        public Boolean MainGrid { get => mainGrid; set => MainGridFunction(value); }

        //Selected Items
        public TargetLanguage SelectedTargetLanguage { get; set; } = new TargetLanguage();
        public Translation SelectedTranslation { get; set; } = new Translation();

        //Results of API calls
        public ObservableCollection<Sentence> Sentences { get; set; } = new ObservableCollection<Sentence>();
        public ObservableCollection<Antonym> Antonyms { get; set; } = new ObservableCollection<Antonym>();
        public ObservableCollection<Synonym> Synonyms { get; set; } = new ObservableCollection<Synonym>();

        public DetailPageViewModel(TargetLanguage selectedTargetLanguage, Translation selectedTranslation)
        {
            this.SelectedTargetLanguage = selectedTargetLanguage;
            this.SelectedTranslation = selectedTranslation;
            GetSentencesAsync();
        }

        private void IsLoadingFunction(bool value)
        {
            isLoading = value;
            OnPropertyChanged("IsLoading");
        }

        private void MainGridFunction(bool value)
        {
            mainGrid = value;
            OnPropertyChanged("MainGrid");
        }

        //Get the sentences with API call
        private async void GetSentencesAsync()
        {
            SentencesService sentenceservice = new SentencesService();
            var sentences = await sentenceservice.GetSentencesGroupAsync(SelectedTargetLanguage, SelectedTranslation);
            AddItemsToSentences(sentences);
            GetSynonymsAntonymsAsync();
        }

        //Get the synonyms and antonyms with API call
        private async void GetSynonymsAntonymsAsync()
        {
            SynonimsAntonymsService synoservice = new SynonimsAntonymsService();
            var synonimsantonyms = await synoservice.GetSynonimsAntonymsGroupAsync(SelectedTargetLanguage, SelectedTranslation);
            AddItemsToSynonymsAndAntonyms(synonimsantonyms);
            IsLoading = false;
            MainGrid = true;
        }

        //Avoiding problems with the result
        private void AddItemsToSentences(System.Collections.Generic.List<SentecesGroup> sentences)
        {
            if (sentences != null)
            {
                foreach (var sentenceitem in sentences)
                {
                    if (sentenceitem.LexicalEntries != null)
                    {
                        foreach (var lexicalitem in sentenceitem.LexicalEntries)
                        {
                            if (lexicalitem.Sentences != null)
                            {
                                foreach (var sentitem in lexicalitem.Sentences)
                                {
                                    Sentences.Add(sentitem);
                                }
                            }

                        }
                    }
                }
            }
        }

        //Avoiding problems with the result
        private void AddItemsToSynonymsAndAntonyms(System.Collections.Generic.List<SynonimsAntonyms> synonimsantonyms)
        {
            if (synonimsantonyms != null)
            {
                foreach (var item in synonimsantonyms)
                {
                    if (item.LexicalEntries != null)
                    {
                        foreach (var lexicalitem in item.LexicalEntries)
                        {
                            if (lexicalitem.Entries != null)
                            {
                                foreach (var entrytitem in lexicalitem.Entries)
                                {
                                    if (entrytitem.Senses != null)
                                    {
                                        foreach (var sensestitem in entrytitem.Senses)
                                        {
                                            if (sensestitem.Antonyms != null)
                                            {
                                                foreach (var antonymitem in sensestitem.Antonyms)
                                                {
                                                    Antonyms.Add(antonymitem);
                                                }
                                            }
                                            if (sensestitem.Synonyms != null)
                                            {
                                                foreach (var synonimitem in sensestitem.Synonyms)
                                                {
                                                    Synonyms.Add(synonimitem);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
