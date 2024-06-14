using JsonReaderLib.Enums;

namespace JsonReaderLib.Models
{
    public struct ActionResponse
    {
        public readonly string FilePath;
        public readonly ResponseCode Code;
        public readonly string Description;

        public ActionResponse(string filePath, ResponseCode code, string description = "")
        {
            FilePath = filePath;
            Code = code;
            Description = description;
        }

        public override string ToString()
        {
            if (Description == "")
            {
                return $"{FilePath} - {Code}";
            }
            return $"{FilePath} - {Code} Description: {Description}";
        }
    }
}
