using System;
using System.Collections.Generic;

namespace OxfordDictionariesAPI.Model
{
    public class GrammaticalFeature
    {
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public class CrossReference
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public class Translation
    {
        public string Language { get; set; }
        public string Text { get; set; }
        public IList<string> Registers { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Translation translation &&
                   Text == translation.Text;
        }
    }

    public class Note
    {
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public class SubsensExample
    {
        public string Text { get; set; }
        public IList<SubsensExampleTranslation> Translations { get; set; }
        public IList<SubsensExampleNote> Notes { get; set; }
    }

    public class SubsensExampleNote
    {
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public class SubsensExampleTranslation
    {
        public string Language { get; set; }
        public string Text { get; set; }
    }

    public class SensExampleTranslation
    {
        public string Language { get; set; }
        public string Text { get; set; }
    }

    public class SensExample
    {
        public string Text { get; set; }
        public IList<SensExampleTranslation> Translations { get; set; }
    }

    public class SubsensNote
    {
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public class SubsensTranslation
    {
        public string Language { get; set; }
        public string Text { get; set; }
    }

    public class Subsens
    {
        public IList<SubsensExample> Examples { get; set; }
        public string Id { get; set; }
        public IList<SubsensNote> Notes { get; set; }
        public IList<SubsensTranslation> Translations { get; set; }
    }

    public class Sens
    {
        public IList<string> CrossReferenceMarkers { get; set; }
        public IList<CrossReference> CrossReferences { get; set; }
        public IList<SensExample> Examples { get; set; }
        public string Id { get; set; }
        public IList<Note> Notes { get; set; }
        public IList<Translation> Translations { get; set; }
        public IList<Subsens> Subsenses { get; set; }
        public IList<string> Registers { get; set; }
    }

    public class Entry
    {
        public IList<GrammaticalFeature> GrammaticalFeatures { get; set; }
        public string HomographNumber { get; set; }
        public IList<Sens> Senses { get; set; }
    }

    public class LexicalEntry
    {
        public IList<Entry> Entries { get; set; }
        public string Language { get; set; }
        public string LexicalCategory { get; set; }
        public string Lext { get; set; }
    }

    public class TranslationGroup
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public IList<LexicalEntry> LexicalEntries { get; set; }
        public string Type { get; set; }
        public string Word { get; set; }
    }

    public class TranslationResult
    {
        public Metadata Metadata { get; set; }
        public IList<TranslationGroup> Results { get; set; }
    }
}
