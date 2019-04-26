namespace mLetsTatoo.Controls
{
    using Fonts;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    /// <summary>
    /// Implements a simple cross-platform font loader with callbacks that can be used by platform implementations
    /// to register fonts on the device. Pass a list of assemblies to the LoadFonts method where fonts are embedded
    /// as resources and a callback for registering fonts.
    /// </summary>
    public static class FontLoader
    {
        /// <summary>
        /// The initialized flag
        /// </summary>
        private static bool _initialized = false;

        /// <summary>
        /// initializes
        /// </summary>
        public static void LoadFonts(IEnumerable<Assembly> assemblies, Action<string, Stream> registerFont)
        {
            if (_initialized)
                return;

            _initialized = true;

            foreach (var assembly in assemblies)
            {

                if (assembly.IsDynamic)
                    continue;

                // Find all resources ending with ttf 
                foreach (var name in assembly.GetManifestResourceNames())
                {

                    if (name.ToLowerInvariant().EndsWith(".ttf"))
                    {
                        using (var s = assembly.GetManifestResourceStream(name))
                        {
                            var fontName = TTFFont.GetName(s);
                            s.Position = 0;
                            registerFont(Path.GetFileName(fontName), s);
                        }
                    }
                }
            }
        }
    }
}
