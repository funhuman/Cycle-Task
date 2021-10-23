using System;
using System.IO;

namespace CycleTask
{
    public static class FileHelper
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public static readonly string File_Path = Environment.CurrentDirectory + @"\healthAide.setting";

        /// <summary>
        /// 保存字符串到文件函数
        /// </summary>
        /// <param name="str">保存的字符串</param>
        /// <param name="filePath">保存的路径</param>
        /// <param name="isAdd">是否追加保存</param>
        /// <returns>是否保存成功</returns>
        public static bool SaveAddFile(string str,
            string filePath = "", bool isAdd = true)
        {
            if (filePath == "")
            {
                filePath = File_Path;
            }
            try
            {
                using (var sw = new StreamWriter(filePath, isAdd))
                {
                    sw.WriteLine(str);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 保存字符串数组到文件函数
        /// </summary>
        /// <param name="strs">保存的字符串数组</param>
        /// <param name="filePath">保存的路径</param>
        /// <param name="isAdd">是否追加保存</param>
        /// <returns>是否保存成功</returns>
        public static bool SaveAddFile(string[] strs,
            string filePath = "", bool isAdd = true)
        {
            if (filePath == "")
            {
                filePath = File_Path;
            }
            try
            {
                using (var sw = new StreamWriter(filePath, isAdd))
                {
                    foreach (var s in strs)
                    {
                        sw.WriteLine(s);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 从文件中读取字符串
        /// </summary>
        /// <param name="line">读取文件的行数</param>
        /// <param name="filePath">读取文件的路径</param>
        /// <returns>是否保存成功</returns>
        public static string[] loadFile(int line, string filePath = "")
        {
            if (filePath == "")
            {
                filePath = File_Path;
            }
            try
            {
                var value = new string[line];
                using (var sr = new StreamReader(filePath))
                {
                    for (var i = 0; i < line; i++)
                    {
                        value[i] = sr.ReadLine();
                    }
                }
                return value;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
