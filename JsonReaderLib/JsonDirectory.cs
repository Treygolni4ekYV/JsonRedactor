using JsonReaderLib.Enums;
using JsonReaderLib.Models;

namespace JsonReaderLib
{
    public class JsonDirectory
    {
        public readonly string FullDirPath;

        private List<JsonFile> _files = new();
        public List<JsonFile> Files { get { return _files; } }

        public JsonDirectory(string dirPath)
        {
            FullDirPath = Path.GetFullPath(dirPath);
            UpdateFiles();
        }

        //фильтрация и конвертация только json файлов
        private List<JsonFile> ExtractJsonPatches(List<string> filePatches)
        {
            List<JsonFile> files = new();
            foreach (var patch in filePatches)
            {
                if (Path.GetExtension(patch) == ".json")
                {
                    files.Add(new JsonFile(patch));
                }
            }
            return files;
        }

        //Нахождение всех путей файлов подходящих под требования замены
        private List<string> SearchFiles(string dir)
        {
            List<string> filesDirs = new();

            filesDirs.AddRange(Directory.GetFiles(dir));

            //проверка на входящие директории
            string[] innerDirs = Directory.GetDirectories(dir);
            if (innerDirs.Length == 0)
            {
                return filesDirs;
            }
            else
            {
                //рекурсивный вызов функции
                foreach (var innerDir in innerDirs)
                {
                    filesDirs.AddRange(SearchFiles(innerDir));
                }
            }
            return filesDirs;
        }

        //обновление файлов в деректории
        public void UpdateFiles()
        {
            _files.AddRange(
                ExtractJsonPatches(
                    SearchFiles(FullDirPath).ToList()));
        }

        
        public List<JsonFile> SearchFilesWithItem(string itemName, ActionNotification? notification = null)
        {
            List<JsonFile> respFiles = new();
            foreach (var file in Files)
            {
                try
                {
                    if (file.ContainItem(itemName))
                    {
                        respFiles.Add(file);
                        if (notification != null)
                        {
                            notification.Invoke(new ActionResponse(file.FullPath, ResponseCode.Success));
                        }
                    }
                    else
                    {
                        if (notification != null)
                        {
                            notification.Invoke(new ActionResponse(file.FullPath, ResponseCode.Half, "File don't consist item"));
                        }
                    }
                }
                catch (Exception exception)
                {
                    if (notification != null)
                    {
                        notification.Invoke(new ActionResponse(file.FullPath, ResponseCode.Error, exception.Message));
                    }
                    continue;
                }
                
            }
            return respFiles;
        }

        //Изменение значение поля у всех файлов
        public List<JsonFile> ChangeFilesItemValue<T>(string itemName, T newValue, ActionNotification? notification = null)
        {
            List<JsonFile> changedFiles = new();

            foreach (var file in _files)
            {
                try
                {
                    if (file.ContainItem(itemName))
                    {
                        if(file.ChangeItemValue<T>(itemName, newValue))
                        {
                            if (notification != null)
                            {
                                notification.Invoke(new ActionResponse(file.FullPath, ResponseCode.Success));
                            }
                        }
                        else
                        {
                            if (notification != null)
                            {
                                notification.Invoke(new ActionResponse(file.FullPath, ResponseCode.Error, "Can't change item value"));
                            }
                        }
                    }
                    else
                    {
                        if (notification != null)
                        {
                            notification.Invoke(new ActionResponse(file.FullPath, ResponseCode.Half, "File don't consist item"));
                        }
                    }

                }
                catch (Exception ex)
                {
                    if(notification != null)
                    {
                        notification.Invoke(new ActionResponse(file.FullPath, ResponseCode.Error, ex.Message));
                    }
                    continue;
                }
            }

            return changedFiles;
        }

    }
}

public delegate void ActionNotification (ActionResponse resp);
public delegate void BoolNotification (bool result, ActionResponse resp);
