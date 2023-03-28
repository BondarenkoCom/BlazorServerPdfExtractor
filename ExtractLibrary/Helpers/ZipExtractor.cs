using System.IO.Compression;

namespace SpecFlowPdfReader.Helpers
{
    public class ZipExtractor
    {
        string extractPath = JsonHelper.GetFolderJsonResult();
        public string ExtractZip(string zipResult)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipResult))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string entryPath = Path.Combine(extractPath, entry.FullName);
                    if (string.IsNullOrEmpty(entry.Name))
                    {
                        Directory.CreateDirectory(entryPath);
                    }
                    else
                    {
                        entry.ExtractToFile(entryPath, true);
                    }
                }
            }
            return null;
        }

        public string ExtractZipCopy(string zipResult, string newName)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipResult))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string newEntryName = entry.Name;

                    if (!string.IsNullOrEmpty(newName))
                    {
                        newEntryName = Path.GetFileNameWithoutExtension(newName) + Path.GetExtension(entry.Name);
                    }

                    string entryPath = Path.Combine(extractPath, newEntryName);

                    if (string.IsNullOrEmpty(entry.Name))
                    {
                        Directory.CreateDirectory(entryPath);
                        return entryPath;
                    }
                    else
                    {
                        entry.ExtractToFile(entryPath, true);
                        return entryPath;
                    }
                }
            }
            return null;
        }
    }
}
