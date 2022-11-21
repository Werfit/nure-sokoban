using System;

namespace Sokoban.Utils
{
    public class Assets
    {
        public static string GetAbsolutePath(string path)
        {
            string absolutePath = String.Format("../../../{0}", path);
            return Path.GetFullPath(absolutePath);
        }
    }
}

