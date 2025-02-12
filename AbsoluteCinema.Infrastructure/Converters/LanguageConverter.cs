using AbsoluteCinema.Domain.Enums;

namespace AbsoluteCinema.Infrastructure.Converters
{
    public class LanguageConverter
    {
        private static readonly Dictionary<string, MovieLanguageEnum> LanguageMap = new()
        {
            { "en", MovieLanguageEnum.English },
            { "es", MovieLanguageEnum.Spanish },
            { "fr", MovieLanguageEnum.French },
            { "de", MovieLanguageEnum.German },
            { "ru", MovieLanguageEnum.Russian },
            { "zh", MovieLanguageEnum.Chinese },
            { "ja", MovieLanguageEnum.Japanese },
            { "ko", MovieLanguageEnum.Korean },
            { "it", MovieLanguageEnum.Italian },
            { "ar", MovieLanguageEnum.Arabic },
            { "pt", MovieLanguageEnum.Portuguese },
            { "hi", MovieLanguageEnum.Hindi },
            { "uk", MovieLanguageEnum.Ukrainian },
            { "tr", MovieLanguageEnum.Turkish }
        };

        public static MovieLanguageEnum ConvertToLanguage(string? originalLanguage)
        {
            if (string.IsNullOrEmpty(originalLanguage))
                return MovieLanguageEnum.Unknown;

            return LanguageMap.TryGetValue(originalLanguage, out var language)
                ? language
                : MovieLanguageEnum.Unknown;
        }
    }
}
