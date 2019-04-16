using System;
using System.Collections.Generic;

namespace OxfordDictionariesAPI.Model
{
    public class SentenceTranslation
    {
        public IList<string> Domains { get; set; }
        public IList<GrammaticalFeature> GrammaticalFeatures { get; set; }
        public string Language { get; set; }
        public IList<Note> Notes { get; set; }
        public IList<string> Regions { get; set; }
        public IList<string> Registers { get; set; }
        public string Text { get; set; }
    }

    public class Sentence
    {
        public IList<string> Definitions { get; set; }
        public IList<string> Domains { get; set; }
        public IList<Note> Notes { get; set; }
        public IList<string> Regions { get; set; }
        public IList<string> Registers { get; set; }
        public IList<string> SenseIds { get; set; }
        public string Text { get; set; }
        public IList<SentenceTranslation> Translations { get; set; }
    }

    public class SentenceLexicalEntry
    {
        public IList<GrammaticalFeature> GrammaticalFeatures { get; set; }
        public string Language { get; set; }
        public string LexicalCategory { get; set; }
        public IList<Sentence> Sentences { get; set; }
        public string Text { get; set; }
    }

    public class SentecesGroup
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public IList<SentenceLexicalEntry> LexicalEntries { get; set; }
        public string Type { get; set; }
        public string Word { get; set; }
    }

    public class Sentences
    {
        public Metadata Metadata { get; set; }
        public IList<SentecesGroup> Results { get; set; }
    }
}
