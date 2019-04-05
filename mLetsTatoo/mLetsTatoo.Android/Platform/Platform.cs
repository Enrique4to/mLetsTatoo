

namespace mLetsTatoo.Droid.Platform
{
    using Android.Content;
    public class Platform
    {
        internal static Context Context = null;

        public static void Init(Context context)
        {
            Context = context;
        }
    }
}