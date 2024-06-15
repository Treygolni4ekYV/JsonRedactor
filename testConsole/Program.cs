using JsonReaderLib;
using JsonReaderLib.Enums;

namespace testConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JsonDirectory jsonDirectory = new JsonDirectory(@"C:\Users\user\Desktop\recipes\packer");

            var resp = jsonDirectory.ChangeFilesItemValue<int>("duration", 12, (resp) =>
            {
                switch (resp.Code)
                {
                    case ResponseCode.Success:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case ResponseCode.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case ResponseCode.Half:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case ResponseCode.Custom:
                    default:
                        break;
                }
                Console.WriteLine(resp.ToString());
                Console.ResetColor();
            });

            Console.WriteLine(resp.Count);
        }
    }
}
