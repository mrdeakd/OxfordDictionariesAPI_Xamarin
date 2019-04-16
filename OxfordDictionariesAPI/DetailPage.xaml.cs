using System;
using System.Collections.Generic;
using OxfordDictionariesAPI.Model;
using OxfordDictionariesAPI.ViewModel;
using Xamarin.Forms;

namespace OxfordDictionariesAPI
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(TargetLanguage selectedTargetLanguage, Translation selectedTranslation)
        {
            InitializeComponent();
            BindingContext = new DetailPageViewModel(selectedTargetLanguage, selectedTranslation);
        }
    }
}
