namespace OtoServisSatis.WebUI.Utils
{
    public class FileHelper
    {
        public static async Task<String> FileLoaderAsync(IFormFile formFile, String filePath = "/Img/")
        {
            var fileName = "";
            if (formFile != null && formFile.Length>0) 
            {
                fileName=formFile.FileName;
                string directory = Directory.GetCurrentDirectory() + "/wwwroot/" + filePath + fileName; //uygulamamın çalıştığı konumu getiriyor hazır metot
                using var stream = new FileStream(directory,FileMode.Create); //bilgisayardan seçilen resmi sunucuya bir akışla yolluyoruz
                await formFile.CopyToAsync(stream); //bilgisayarımızdan sunucuya kopyalama yaptık
            }
            return fileName;
        }
    }
}
