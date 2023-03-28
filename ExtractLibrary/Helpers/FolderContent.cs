
namespace ExtractLibrary.Helpers
{
    public class FolderContent
    {
        public static string ClearFolderContents(string folderPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);

                // Delete all files in the directory
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                // Delete all subdirectories in the directory
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true); // 'true' parameter indicates to delete the subdirectory and its contents recursively
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
