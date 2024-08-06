using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApiTupac.Services
{
    public class SlugHelper
    {
        public static string GenerateSlug(string value)
        {
            // Convierte a minúsculas
            value = value.ToLowerInvariant();

            // Normaliza el texto para eliminar acentos
            value = RemoveDiacritics(value);

            // Reemplaza caracteres no alfanuméricos por guiones
            value = Regex.Replace(value, @"[^a-z0-9\s-]", "");
            value = Regex.Replace(value, @"\s+", " ").Trim();
            value = Regex.Replace(value, @"\s", "-");

            return value;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
