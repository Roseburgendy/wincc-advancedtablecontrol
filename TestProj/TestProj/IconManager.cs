using System;
using System.Drawing;
using FontAwesome.Sharp;
using Krypton.Toolkit;

namespace TestProj
{
    /// <summary>
    /// Generates theme-aware button icons using FontAwesome.Sharp.
    /// Icons are rendered as font glyphs at runtime so the colour can be
    /// freely chosen — no SVG or multi-bitmap sets required.
    /// </summary>
    public static class IconManager
    {
        /// <summary>Returns true when the given palette is a dark-mode theme.</summary>
        public static bool IsDark(PaletteMode mode)
        {
            string name = mode.ToString();
            return name.Contains("Black") || name.Contains("Dark");
        }

        /// <summary>
        /// Returns the appropriate icon foreground colour for the theme:
        /// near-white for dark themes, dark-grey for light themes.
        /// </summary>
        public static Color IconColor(PaletteMode mode)
            => IsDark(mode) ? Color.WhiteSmoke : Color.FromArgb(64, 64, 64);

        /// <summary>
        /// Creates a <see cref="Bitmap"/> for <paramref name="icon"/> sized
        /// <paramref name="size"/>×<paramref name="size"/> using the colour that
        /// matches the current <paramref name="mode"/>.
        /// </summary>
        public static Bitmap Make(IconChar icon, PaletteMode mode, int size = 24)
            => icon.ToBitmap(IconColor(mode), size);

        /// <summary>
        /// Convenience overload — reads the current palette from
        /// <see cref="ConfigManager"/> so callers that have no reference to
        /// <see cref="KryptonManager"/> can still obtain a correctly-coloured icon.
        /// </summary>
        public static Bitmap Make(IconChar icon, int size = 24)
        {
            string saved = ConfigManager.Config?.Theme ?? "Microsoft365Blue";
            Enum.TryParse<PaletteMode>(saved, out var mode);
            return Make(icon, mode, size);
        }
    }
}
