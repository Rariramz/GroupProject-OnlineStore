using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace Store.Tools
{
    public class ImageConverter
    {
        public static MemoryStream Base64ToImage(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            Image image = Image.Load(bytes);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, PngFormat.Instance);
            return memoryStream;
        }

        public static string ImageToBase64(string file)
        {
            Image image = Image.Load(file);
            return image.ToBase64String(PngFormat.Instance);
        }

        public static string ImageToBase64(IFormFile formFile)
        {
            try
            {
                Stream stream = formFile.OpenReadStream();
                Image image = Image.Load(stream);
                return image.ToBase64String(PngFormat.Instance);
            }
            catch
            {
                return "";
            }
        }
    }
}
