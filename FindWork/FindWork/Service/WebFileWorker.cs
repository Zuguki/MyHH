using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace FindWork.Service;

public static class WebFileWorker
{
    public static string GetWebFileName(string fileName)
    {
        var webFolder = GetWebFileFolder(fileName);
        CreateFolder(webFolder);
        return webFolder + "/" + Path.GetFileNameWithoutExtension(fileName) + ".jpeg";
    }

    public static async Task UploadAndResizeImage(Stream fileStream, string fileName, int newWidth, int newHeight)
    {
        using var image = await Image.LoadAsync(fileStream);
        
        if (image.Width / (image.Height / newHeight) > newWidth)
            newHeight = (int) (image.Height / (image.Width / (float) newWidth));
        else
            newWidth = (int) (image.Width / (image.Height / (float) newHeight));
            
        image.Mutate(x => x.Resize(newWidth, newHeight, KnownResamplers.Lanczos3));
        await image.SaveAsJpegAsync(fileName, new JpegEncoder {Quality = 75});
    }
    
    public static string GetWebFileFolder(string fileName)
    {
        var md5Hash = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(fileName);
        var hashBytes = md5Hash.ComputeHash(inputBytes);

        var hash = Convert.ToHexString(hashBytes);
        return "./wwwroot/images/" + hash[..2] + "/" + hash[..4];
    }

    public static void CreateFolder(string dir)
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }
}