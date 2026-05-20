using System;
using UnityEngine.Localization;

namespace Reacative.Infrastructure.Localization
{
    public static class LocalizationParser
    {
        public static string ToLocalizationKey(this LocalizedString localizedReference)
        {
            return
                $"{localizedReference.TableReference.TableCollectionName}:{localizedReference.TableEntryReference}";
        }

        public static LocalizedString ToLocalizedString(string localizationKey)
        {
            string[] parts = localizationKey.Split(":");
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid localization key");
            }

            var table = parts[0];
            var key = parts[1];
            
            return new LocalizedString(table, key);
        }
    }
}