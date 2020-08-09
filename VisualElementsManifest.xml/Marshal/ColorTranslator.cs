using System;
using System.Drawing;

namespace VisualElementsManifest.Marshal {

    public static class ColorTranslator {

        public static Color FromHtml(string colorString) {
            if (!colorString.StartsWith("#")) {
                throw new ArgumentException($"Hexadecimal RGB color value {colorString} does not start with #.");
            } else if (colorString.Length == 4) {
                var fullLengthColorString = $"#{colorString[1]}{colorString[1]}{colorString[2]}{colorString[2]}{colorString[3]}{colorString[3]}";
                return FromHtml(fullLengthColorString);
            } else if (colorString.Length != 7) {
                throw new ArgumentException($"Hexadecimal RGB color value {colorString} is not 7 characters long (including the leading #).");
            }

            int[] rgb = new int[3];
            for (int i = 0; i < rgb.Length; i++) {
                string hexByte = colorString.Substring(2 * i + 1, 2);

                try {
                    rgb[i] = Convert.ToInt32(hexByte, 16);
                } catch (Exception e) when (!(e is OutOfMemoryException)) {
                    throw new ArgumentException($"Hexadecimal RGB color value {colorString} has invalid hex byte {hexByte}", e);
                }
            }

            return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
        }

        public static string ToHtml(Color color) => $"#{color.R:X2}{color.G:X2}{color.B:X2}";

    }

}