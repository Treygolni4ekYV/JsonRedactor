using JsonReaderLib;

namespace testConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JsonDirectory jsonDirectory = new JsonDirectory(@"C:\Users\user\Desktop\recipes\packer");

            var resp = jsonDirectory.SearchFilesWithItem("asd", (result, resp) =>
            {
                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine(resp.ToString());
                Console.ResetColor();
            });

            Console.WriteLine(resp.Count);
        }
    }
}
