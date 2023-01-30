using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TosunlarYapiMarket.Core.Extensions
{
    public static class ImageHelperExtension
    {
        public static string ClearCharacter(string text)
        {
            string newName = Regex.Replace(text, "[^\\w]", "_");
            string unaccentedText = String.Join("", newName.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark));
            return unaccentedText.Replace("ı", "i");
        }

        public static string UploadImage(IFormFile? formFile, string imgPath)
        {
            var extension = Path.GetExtension(formFile?.FileName);
            var newFileName = $"{ClearCharacter($"{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}_{DateTime.Now.Millisecond}_{Path.GetFileNameWithoutExtension(formFile?.FileName)}")}{extension}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\img\\{imgPath}", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formFile?.CopyTo(stream);
            }

            return newFileName;
        }

        public static void DeleteImage(string imageUrl, string imgPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\img\\{imgPath}", imageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
