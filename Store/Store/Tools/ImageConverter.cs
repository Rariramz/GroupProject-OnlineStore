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
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        public static string ImageToBase64(string file)
        {
            Image image = Image.Load(file);
            MemoryStream stream = new MemoryStream();
            image.SaveAsPng(stream);
            return Convert.ToBase64String(stream.ToArray());
        }

        public static string ImageToBase64(IFormFile formFile)
        {
            try
            {
                Stream stream = formFile.OpenReadStream();
                Image image = Image.Load(stream);
                MemoryStream saveStream = new MemoryStream();
                image.SaveAsPng(saveStream);
                return Convert.ToBase64String(saveStream.ToArray());
            }
            catch
            {
                return "";
            }
        }

        public static void SaveToDisk(string base64, string path)
        {
            MemoryStream stream = Base64ToImage(base64);
            byte[] bytes = stream.ToArray();

            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileStream.Write(bytes);
            fileStream.Close();
        }
    }
}
