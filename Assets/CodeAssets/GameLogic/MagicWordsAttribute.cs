using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.CodeAssets.GameLogic
{
    /// <summary>
    /// Refers to anything we want to manufacture tooltips on observing in a card description or something.
    /// </summary>
    public class MagicWordsAttribute : Attribute
    {
        public string Title { get; set; }
        public string Description { get; set; }

        private static List<MagicWordsAttribute> MagicWordsRegistered = new List<MagicWordsAttribute>();

        public static void RegisterMagicWord(MagicWordsAttribute word)
        {
            if (MagicWordsRegistered.Any(item => item.Title == word.Title))
            {
                return;
            }
            MagicWordsRegistered.Add(word);
        }

        public static void RegisterMagicWordsReflectively()
        {
            var types = Assembly
              .GetExecutingAssembly()
              .GetTypes();

            foreach (var t in types)
            {
                var registeredAttributes = t.GetCustomAttributes<MagicWordsAttribute>();
                foreach (var registeredAttribute in registeredAttributes)
                {
                    RegisterMagicWord(registeredAttribute);
                }
            }
        }


        public string FormatMagicWords(List<MagicWordsAttribute> magicWords)
        {
            string value = "";
            foreach(var word in magicWords)
            {
                value += $"<color=green>{word.Title}</color>: {word.Description}\n";
            }
            return value;
        }

        public static List<MagicWordsAttribute> GetApplicableMagicWordsForString(string stringToAnalyze)
        {
            var relevantMagicWords = MagicWordsRegistered
                .Where(item => stringToAnalyze.Contains(item.Title));
            return relevantMagicWords.ToList();
        }

        public static List<MagicWordsAttribute> GetMagicWordsApplicableToCard(AbstractCard card)
        {
            return GetApplicableMagicWordsForString(card.Description());
        }

        public static List<MagicWordsAttribute> GetMagicWordsApplicableToStatusEffect(AbstractStatusEffect effect)
        {
            return GetApplicableMagicWordsForString(effect.Description);
        }

    }
}