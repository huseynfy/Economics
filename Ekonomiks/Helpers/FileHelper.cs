using System.IO;

namespace FrontToBack.Helpers
{
    public static class FileHelper
    {
        public static bool DeleteImg(string root,string folder,string filename)
        {
            string path = Path.Combine(root, folder, filename);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
