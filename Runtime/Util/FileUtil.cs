using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Com.BaiZe.SharpToolSet
{
    public class FileMeta
    {
        public string path;
        public string name;
        public bool isDir;
        public int depth;

        public FileMeta(string path, string name, bool isDir, int depth)
        {
            this.path = path;
            this.name = name;
            this.isDir = isDir;
            this.depth = depth;
        }
    }

    public class FileUtil
    {
        // 递归创建文件
        public static void DepCreateFile(string fullPath)
        {
            var p = fullPath.PathFormat();
            var steps = p.Split('/');
            int stepNum = steps.Length;
            string path = string.Empty;
            for (int i = 0; i < stepNum; ++i)
            {
                if (i == stepNum - 1)
                {
                    File.Create(fullPath).Dispose();
                    break;
                }
                path += steps[i];
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += '/';
            }
        }

        // 递归查找文件夹
        public static List<FileMeta> FindDirsWithDep(string dir)
        {
            return DepFindFileSystem(dir, includeDir: true, includeFile: false);
        }

        // 递归查找文件，包含所在文件夹
        public static List<FileMeta> FindFilesWithDepIncludeDir(string dir)
        {
            return DepFindFileSystem(dir, includeDir: true);
        }

        // 递归查找文件
        public static List<FileMeta> FindFilesWithDep(string dir)
        {
            return DepFindFileSystem(dir);
        }

        // 递归查找某类型文件,包含所在文件夹
        public static List<FileMeta> FindFilesByTypeWithDepIncludeDir(string dir, string extension)
        {
            return DepFindFileSystem(dir, extension, true);
        }

        // 递归查找某类型文件
        public static List<FileMeta> FindFilesByTypeWithDep(string dir, string extension)
        {
            return DepFindFileSystem(dir, extension);
        }

        private static List<FileMeta> FindFilesInDir(string dir)
        {
            return FindFiles(dir);

        }

        private static List<FileMeta> FindFilesInDirByType(string dir, string extension)
        {
            return FindFiles(dir, extension);
        }

        // 查找当前文件夹下某类型的文件
        public static List<FileMeta> FindFiles(string dir, string extension = null, int depth = 0)
        {
            List<FileMeta> listFileResult = new List<FileMeta>();
            var files = Directory.GetFiles(dir);
            foreach (var file in files)
            {
                string filePath = Path.GetFullPath(file).PathFormat();
                string fileName = filePath.FileNameWithExtension();
                string suffix = filePath.Extension();
                if (extension == null || suffix == extension)
                {
                    listFileResult.Add(new FileMeta(filePath, fileName, false, depth));
                }
            }
            return listFileResult;
        }

        private static List<FileMeta> DepFindFileSystem(string dir, string extension = null, bool includeDir = false, bool includeFile = true)
        {
            List<FileMeta> listMatchResult = DepFinding(dir, extension, includeDir, includeFile, 0);
            return listMatchResult;
        }

        private static List<FileMeta> DepFinding(string dir, string extension, bool includeFile, bool includeDir, int dep)
        {
            var listMatchResult = new List<FileMeta>();
            string dirName = dir.FileName();
            if (!includeFile)
            {
                if (includeDir) listMatchResult.Add(new FileMeta(dir, dirName, true, dep));
            }
            else
            {
                var listFileResult = FindFiles(dir, extension, dep + 1);
                listMatchResult.AddRange(listFileResult);
            }

            var directories = Directory.GetDirectories(dir);
            foreach (var d in directories)
            {
                var matchResult = DepFinding(d, extension, includeDir, includeFile, dep + 1);
                if (includeDir && matchResult.Count > 0) listMatchResult.Add(new FileMeta(d, d.FileName(), true, dep + 1));
                listMatchResult.AddRange(matchResult);
            }
            return listMatchResult;
        }
    }
}