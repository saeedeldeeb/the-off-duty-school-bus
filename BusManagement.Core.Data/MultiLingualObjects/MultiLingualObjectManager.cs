using System.Globalization;
using Microsoft.IdentityModel.Tokens;

namespace BusManagement.Core.Data.MultiLingualObjects;

public class MultiLingualObjectManager
{
    private const int MaxCultureFallbackDepth = 5;

    public Task<TTranslation> FindTranslationAsync<TMultiLingual, TTranslation>(
        TMultiLingual multiLingual,
        string culture = null,
        bool fallbackToParentCultures = true
    )
        where TMultiLingual : IMultiLingualObject<TTranslation>
        where TTranslation : class, IObjectTranslation
    {
        culture ??= CultureInfo.CurrentCulture.Name;

        if (multiLingual.Translations.IsNullOrEmpty())
        {
            return Task.FromResult<TTranslation>(null);
        }

        var translation = multiLingual.Translations.FirstOrDefault(pt => pt.Language == culture);
        if (translation != null)
        {
            return Task.FromResult(translation);
        }

        if (!fallbackToParentCultures)
            return Task.FromResult<TTranslation>(null);
        translation = GetTranslationBasedOnCulturalRecursive(
            CultureInfo.CurrentUICulture.Parent,
            multiLingual.Translations,
            0
        );

        return Task.FromResult(translation);
    }

    private TTranslation GetTranslationBasedOnCulturalRecursive<TTranslation>(
        CultureInfo culture,
        ICollection<TTranslation> translations,
        int currentDepth
    )
        where TTranslation : class, IObjectTranslation
    {
        if (
            culture == null
            || culture.Name.IsNullOrEmpty()
            || translations.IsNullOrEmpty()
            || currentDepth > MaxCultureFallbackDepth
        )
        {
            return null;
        }

        var translation = translations.FirstOrDefault(pt =>
            pt.Language.Equals(culture.Name, StringComparison.OrdinalIgnoreCase)
        );
        return translation
            ?? GetTranslationBasedOnCulturalRecursive(
                culture.Parent,
                translations,
                currentDepth + 1
            );
    }
}
