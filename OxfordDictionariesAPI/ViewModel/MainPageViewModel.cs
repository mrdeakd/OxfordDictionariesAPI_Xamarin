using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Android.Content.Res;
using OxfordDictionariesAPI.Model;
using OxfordDictionariesAPI.Services;
using Xamarin.Forms;

namespace OxfordDictionariesAPI.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly MainPage mainPage;

        public String Word { get; set; } = "";

        //UI
        private bool loadingIndicator = false;
        private bool listViewVisible = false;
        private bool enabledButton = false;
        public Boolean LoadingIndicator { get => loadingIndicator; set => LoadingIndicatorFunction(value); }
        public Boolean ListViewVisible { get => listViewVisible; set => ListViewVisibleFunction(value); }
        public Boolean EnabledButton { get => enabledButton; set => EnabledButtonFunction(value); }

        //Selected Items
        private SourceLanguage selectedSourceLanguage = new SourceLanguage();
        private TargetLanguage selectedTargetLanguage = new TargetLanguage();
        private Translation selectedTranslation = new Translation();
        public SourceLanguage SelectedSourceLanguage { get => selectedSourceLanguage; set => SetSelectedSourceLanguage(value); }
        public TargetLanguage SelectedTargetLanguage { get => selectedTargetLanguage; set => SetSelectedTargetLanguage(value); }
        public Translation SelectedTranslation { get => selectedTranslation; set => OpenNewPage(value); }

        //Results of API calls
        public ObservableCollection<LanguageGroup> Languages { get; set; } = new ObservableCollection<LanguageGroup>();
        public ObservableCollection<SourceLanguage> SourceLanguages { get; set; } = new ObservableCollection<SourceLanguage>();
        public ObservableCollection<TargetLanguage> TargetLanguages { get; set; } = new ObservableCollection<TargetLanguage>();
        public ObservableCollection<Translation> Translation { get; set; } = new ObservableCollection<Translation>();

        //Translate Button Click
        public ICommand TranslateButtonCommand { get; private set; }

        public MainPageViewModel(MainPage mainPage)
        {
            this.mainPage = mainPage;
            TranslateButtonCommand = new Command(TranslateWord);
            GetLanguagesAsync();
        }

        private void LoadingIndicatorFunction(bool value)
        {
            loadingIndicator = value;
            OnPropertyChanged("LoadingIndicator");
        }

        private void ListViewVisibleFunction(bool value)
        {
            listViewVisible = value;
            OnPropertyChanged("ListViewVisible");
        }

        private void EnabledButtonFunction(bool value)
        {
            enabledButton = value;
            OnPropertyChanged("EnabledButton");
        }

        //Load the proper TargetLanguages to the other list
        private void SetSelectedSourceLanguage(SourceLanguage value)
        {
            selectedSourceLanguage = value;
            EnabledButton = false;
            TargetLanguages.Clear();
            foreach (var item in Languages)
            {
                if (item.SourceLanguage.Equals(value) && item.TargetLanguage != null)
                    TargetLanguages.Add(item.TargetLanguage);
            }
        }

        private void SetSelectedTargetLanguage(TargetLanguage value)
        {
            EnabledButton |= selectedSourceLanguage != null;
            selectedTargetLanguage = value;
        }

        //New page with the details of the selected word
        private void OpenNewPage(Translation value)
        {
            selectedTranslation = value;
            if (value != null)
                mainPage.Navigation.PushAsync(new DetailPage(selectedTargetLanguage, selectedTranslation));
        }

        //The process of Translate
        private async void TranslateWord()
        {
            PreTranslateWord();
            TranslationService transervice = new TranslationService();
            var tran = await transervice.GetTranslationGroupAsync(selectedSourceLanguage, selectedTargetLanguage, Word);
            Translation.Clear();
            AddItemsToTranslation(tran);
            PostTranslateWord();
        }

        //Avoiding problems with the result
        private void AddItemsToTranslation(List<TranslationGroup> tran)
        {
            if (tran != null)
            {
                foreach (var tranitem in tran)
                {
                    if (tranitem.LexicalEntries != null)
                    {
                        foreach (var lexicalitem in tranitem.LexicalEntries)
                        {
                            if (lexicalitem.Entries != null)
                            {
                                foreach (var entriesitem in lexicalitem.Entries)
                                {
                                    if (entriesitem.Senses != null)
                                    {
                                        foreach (var sensesitem in entriesitem.Senses)
                                        {
                                            if (sensesitem.Translations != null)
                                            {
                                                foreach (var transitem in sensesitem.Translations)
                                                {
                                                    if (!Translation.Contains(transitem))
                                                        Translation.Add(transitem);
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
            else{
                mainPage.DisplayAlert("Figyelmeztetés", "Nem találunk a keresett szóhoz fordítást !", "OK");
            }
        }

        //Post settings
        private void PostTranslateWord()
        {
            LoadingIndicator = false;
            ListViewVisible = true;
        }

        //Pre settings
        private void PreTranslateWord()
        {
            ListViewVisible = false;
            LoadingIndicator = true;
        }

        //Get the languages with API call
        private async void GetLanguagesAsync()
        {
            LanguagesService lanservice = new LanguagesService();
            var lan = await lanservice.GetLanguageGroupAsync();
            foreach (var langitem in lan)
            {
                Languages.Add(langitem);
                if (!SourceLanguages.Contains(langitem.SourceLanguage) && langitem.TargetLanguage != null)
                    SourceLanguages.Add(langitem.SourceLanguage);
            }
        }
    }
}
