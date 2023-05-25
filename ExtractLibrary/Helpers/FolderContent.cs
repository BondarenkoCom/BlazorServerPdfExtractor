
namespace ExtractLibrary.Helpers
{
    public class FolderContent
    {
        public static string ClearFolderContents(string folderPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                return "Folder contents cleared successfully.";
            }
            catch (Exception e)
            {
                return "Error while clearing folder contents: " + e.Message;
            }
        }
    }
}
