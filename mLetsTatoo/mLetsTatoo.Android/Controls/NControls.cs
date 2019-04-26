namespace mLetsTatoo.Droid.Controls
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Android.Graphics;
    using mLetsTatoo.Controls;

    /// <summary>
    /// NControls.
    /// </summary>
    public static class NControls
    {
        /// <summary>
        /// The typefaces.
        /// </summary>
        public static readonly Dictionary<string, Typeface> Typefaces = new Dictionary<string, Typeface>();

        /// <summary>
        /// Init this instance  
        /// </summary>
        public static void Init()
        {
            FontLoader.LoadFonts(AppDomain.CurrentDomain.GetAssemblies(), (fontName, s) => {

                fontName = fontName.ToLowerInvariant();

                // Copy stream
                var fontFilename = System.IO.Path.Combine(System.Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal), fontName);

                if (!File.Exists(fontFilename))
                {
                    using (var fs = new FileStream(fontFilename, FileMode.CreateNew))
                    {
                        s.CopyTo(fs);
                    }
                }

                Typefaces[fontName] = Typeface.CreateFromFile(fontFilename);
            });
        }
    }
}