using System.IO;

namespace Com.BaiZe.SharpToolSet
{
    public static class PathExtension
    {
        // 路径格式化；/ 替换 \\ 
        public static string PathFormat(this string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            string tempPath = path.Replace("\\", "/");
            return tempPath;
        }

        // 获取根目录
        // x/xx/xxx.cs => x
        public static string RootDir(this string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            string tempPath = path.PathFormat();
            int index = tempPath.IndexOf('/');
            tempPath = tempPath.Substring(0, index < 0 ? 0 : index);
            return tempPath;
        }

        // 获取当前路径的父目录
        public static string DirectoryPath(this string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            string tempPath = path.PathFormat();
            tempPath = tempPath.TrimEnd('/');
            int index = tempPath.LastIndexOf('/');
            return tempPath.Substring(0, index < 0 ? 0 : index);
        }

        public static string Directory(this string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            string tempPath = path.PathFormat();
            tempPath = tempPath.TrimEnd('/');
            int lastIndex = tempPath.LastIndexOf('/');
            lastIndex = lastIndex < 0 ? 0 : lastIndex;
            string parentDir = tempPath.Substring(0, lastIndex);
            int index = parentDir.LastIndexOf('/');
            index = index < 0 ? 0 : index;
            return tempPath.Substring(index, lastIndex);
        }

        // 获取路径后缀文件名
        // x/xx/xxx.cs => xxx.cs
        public static string FileNameWithExtension(this string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            string tempPath = path.PathFormat();
            int index = tempPath.LastIndexOf('/');
            tempPath = tempPath.Substring(index + 1);
            return tempPath;
        }

        public static string FileName(this string path)
        {
            string fileNameWithExtension = path.FileNameWithExtension();
            int suffixIndex = fileNameWithExtension.LastIndexOf('.');
            return fileNameWithExtension.Substring(0, suffixIndex < 0 ? fileNameWithExtension.Length : suffixIndex);
        }

        // 路径扩展名
        public static string Extension(this string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            int index = path.LastIndexOf('.');
            string tempPath = path.Substring(index + 1);
            return tempPath;
        }
    }
}