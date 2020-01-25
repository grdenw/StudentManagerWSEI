using System;
using System.IO;

namespace StudentManager.Const
{
    public static class PathConst
    {
        public static string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Students.csv");
    }
}
