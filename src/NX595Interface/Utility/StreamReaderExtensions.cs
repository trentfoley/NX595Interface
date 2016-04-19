using System.IO;

namespace NX595Interface.Utility
{
    public static class StreamReaderExtensions
    {
        public static void SkipLines(this StreamReader reader, int count)
        {
            for (int i = 0; i < count; i++) reader.ReadLine();
        }
    }
}
