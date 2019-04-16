using System;
using System.Collections.Generic;

namespace OxfordDictionariesAPI.Model
{
    public class SynonimsAntonymsGroup
    {
        public class Antonym
        {
            public string Id { get; set; }
            public string Language { get; set; }
            public string Text { get; set; }
        }

        public class Example
        {
            public string Text { get; set; }
        }

        public class Synonym
        {
            public string Id { get; set; }
            public string Language { get; set; }
            public string Text { get; set; }
        }

        public class Subsens
        {
            public IList<string> Domains { get; set; }
            public string Id { get; set; }
            public IList<Synonym> Synonyms { get; set; }
            public IList<string> Registers { get; set; }
            public IList<string> Regions { get; set; }
        }

        public class Sens
        {
            public IList<Antonym> Antonyms { get; set; }
            public IList<Example> Examples { get; set; }
            public string Id { get; set; }
            public IList<Subsens> Subsenses { get; set; }
            public IList<Synonym> Synonyms { get; set; }
            public IList<string> Regions { get; set; }
            public IList<string> Registers { get; set; }
        }

        public class Entry
        {
            public string HomographNumber { get; set; }
            public IList<Sens> Senses { get; set; }
        }

        public class LexicalEntry
        {
            public IList<Entry> Entries { get; set; }
            public string Language { get; set; }
            public string LexicalCategory { get; set; }
            public string Text { get; set; }
        }

        public class SynonimsAntonyms
        {
            public string Id { get; set; }
            public string Language { get; set; }
            public IList<LexicalEntry> LexicalEntries { get; set; }
            public string Type { get; set; }
            public string Word { get; set; }
        }

        public class SynonimsAndAntonymsGroup
        {
            public Metadata Metadata { get; set; }
            public IList<SynonimsAntonyms> Results { get; set; }
        }
    }
}
