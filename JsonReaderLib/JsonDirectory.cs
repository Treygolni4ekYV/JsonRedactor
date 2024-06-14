
namespace JsonReaderLib
{
    public class JsonDirectory
    {
        public readonly string FullDirPath;
        private List<string> FilesPaths = new();

        public JsonDirectory(string dirPath)
        {
            FullDirPath = Path.GetFullPath(dirPath);
            FilesPaths.AddRange(
                ExtractJsonPatches(
                    Directory.GetFileSystemEntries(FullDirPath).ToList<string>())
                );
        }

        //Достаем только json aайлы
        private List<string> ExtractJsonPatches(List<string> filePatches)
        {
            List<string> jsonPatches = new();
            foreach (var patch in filePatches)
            {
                if (Path.GetExtension(patch) == ".json")
                {
                    jsonPatches.Add(patch);
                }
            }
            return jsonPatches;
        }

    }
}
