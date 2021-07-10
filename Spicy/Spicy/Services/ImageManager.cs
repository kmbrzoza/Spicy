using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy.Services
{
    static class ImageManager
    {
        private static string[] imageExtensions = { ".jpg", ".jpeg" , ".png"};
        public static bool ImageExist(string path)
        {
            if (File.Exists(path))
            {
                foreach(var ext in imageExtensions)
                {
                    if (ext == Path.GetExtension(path))
                        return true;
                }
            }
            return false;
        }
        public static byte[] GetBytesFromImagePath(string imagePath)
        {
            byte[] img = null;
            if (ImageExist(imagePath))
            {
                MemoryStream memoryStream = new MemoryStream();
                using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    fileStream.CopyTo(memoryStream);
                }
                img = memoryStream.ToArray();
            }
            return img;
        }
    }
}
