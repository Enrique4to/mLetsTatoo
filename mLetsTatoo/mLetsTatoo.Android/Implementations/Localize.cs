[assembly: Xamarin.Forms.Dependency(typeof(mLetsTatoo.Droid.Implementations.Localize))]
namespace mLetsTatoo.Droid.Implementations
{
    using System.Globalization;
    using System.Threading;
    using Helpres;
    using Interfaces;
    public class Localize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "es-MX";
            var androidLocale = Java.Util.Locale.Default;
            netLanguage = AndroitToDotnetLanguaje(androidLocale.ToString().Replace("_","-"));
            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e1)
            {
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    ci = new System.Globalization.CultureInfo(fallback);
                }
                catch (CultureNotFoundException e2)
                {
                    ci = new System.Globalization.CultureInfo("es-MX");
                }
            }
            return ci;
        }
        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
        string AndroitToDotnetLanguaje(string androidLanguage)
        {
            var netlanguage = androidLanguage;
            switch (androidLanguage)
            {
                case "ms-BN":
                case "ms-MY":
                case "ms-SG":
                    netlanguage = "ms";
                    break;
                case "in-ID":
                    netlanguage = "id-ID";
                    break;
                case "gsw-CH":
                    netlanguage = "de-CH";
                    break;
            }
            return netlanguage;
        }
        string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            var netLanguage = platCulture.LanguageCode;
            switch (platCulture.LanguageCode)
            {
                case "gsw":
                    netLanguage = "de-CH";
                    break;
            }
            return netLanguage;
        }
    }
}