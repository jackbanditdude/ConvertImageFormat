using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertImageFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            bool deleteAfterConversion = CheckDeleteAfterConversion();

            foreach (string item in args)
            {
                using (var image = new MagickImage(item))
                {
                    string imageName = image.FileName;
                    Console.WriteLine(Path.GetExtension(imageName).ToLower());
                    if (new List<string>(){ ".jpg", ".jpeg" }.Contains(Path.GetExtension(imageName).ToLower()))
                        continue;

                    string imageJpgName = Path.ChangeExtension(imageName, "jpg");
                    //string imageJpgName = imageName.Replace(".heic", ".jpg");
                    Console.WriteLine(imageName);
                    image.Format = MagickFormat.Jpg;
                    image.Write(imageJpgName);

                    if (deleteAfterConversion)
                        File.Delete(imageName);
                }
            }

            Console.WriteLine("...done");
            Console.ReadLine();
        }

        private static bool CheckDeleteAfterConversion()
        {
            Console.WriteLine("Delete images after conversion (n/y)? ");
            string response = Console.ReadLine(); // TODO: use ReadKey to not require enter

            if (!CheckDeleteAfterResponse(response))
                return CheckDeleteAfterConversion();

            if (response.ToLower() == "n")
                return false;
            else if (response.ToLower() == "y")
                return true;

            return false;
        }

        private static bool CheckDeleteAfterResponse(string response)
        {
            string lowerResponse = response.ToLower();

            if (lowerResponse == "y" || lowerResponse == "n")
                return true;
            else
                return false;
        }
    }
}
