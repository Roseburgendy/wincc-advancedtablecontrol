using System.Globalization;
using System.Resources;

namespace TestProj
{
    /// <summary>
    /// Provides localised UI strings based on <see cref="ConfigManager.Config.Language"/>.
    /// Neutral (default) culture = zh-CN; English satellite = en-US.
    /// </summary>
    public static class LangHelper
    {
        private static readonly ResourceManager _rm =
            new ResourceManager("TestProj.Properties.Lang", typeof(LangHelper).Assembly);

        /// <summary>
        /// Returns the translated string for <paramref name="key"/>.
        /// Falls back to the key itself if the string is missing.
        /// </summary>
        public static string Get(string key)
        {
            try
            {
                string lang = ConfigManager.Config?.Language ?? "zh-CN";
                var cult = new CultureInfo(lang);
                return _rm.GetString(key, cult) ?? key;
            }
            catch
            {
                return key;
            }
        }
    }
}
