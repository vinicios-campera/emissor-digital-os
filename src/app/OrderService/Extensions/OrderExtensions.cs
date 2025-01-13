using Xamarin.Essentials;

namespace OrderService.Extensions
{
    public static class OrderExtensions
    {
        public static async void OpenPdfAsync(this byte[] document, string title)
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, title);
                File.WriteAllBytes(filePath, document);
                if (filePath != null)
                {
                    await Launcher.OpenAsync(new OpenFileRequest
                    {
                        File = new ReadOnlyFile(filePath)
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex.Failin();
            }
        }
    }
}