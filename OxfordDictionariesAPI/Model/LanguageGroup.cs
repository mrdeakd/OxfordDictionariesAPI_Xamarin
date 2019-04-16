using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OxfordDictionariesAPI.Model
{
    public class Metadata
    {
        public string Provider { get; set; }
    }

    public class SourceLanguage
    {
        public string Id { get; set; }
        public string Language { get; set; }

        public override bool Equals(Object obj)
        {
            return Id.Equals((obj as SourceLanguage).Id) && Language.Equals((obj as SourceLanguage).Language);
        }
    }


    public class TargetLanguage
    {
        public string Id { get; set; }
        public string Language { get; set; }
    }

    public class LanguageGroup
    {
        public string Region { get; set; }
        public string Source { get; set; }
        public SourceLanguage SourceLanguage { get; set; }
        public TargetLanguage TargetLanguage { get; set; }
        public string Type { get; set; }
    }

    public class Language
    {
        public Metadata Metadata { get; set; }
        public IList<LanguageGroup> Results { get; set; }
    }
}
