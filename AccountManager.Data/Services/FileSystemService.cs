using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AccountManager.Interfaces.Accounts;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AccountManager.Data.Services
{
    internal static class FileSystemService
    {
        public static void CheckRoot(string rootPath)
        {
            if (!Directory.Exists(rootPath))
            {
                throw new DirectoryNotFoundException();
            }
        }

        public static string CreatePath(string filename, string rootPath)
        {
            CheckRoot(rootPath);

            var path = Path.Combine(rootPath, filename.Substring(0,3).ToLower());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }


        public static StreamWriter OpenFileForWriting(string fileName, string rootPath) 
        {
            var path = CreatePath(fileName, rootPath);
            return new StreamWriter(Path.Combine(path, $"{fileName}.json"), append:false);
        }

        public static StreamReader OpenFileForReading(string fileName, string rootPath)
        {
            var path = CreatePath(fileName, rootPath);
            return new StreamReader(Path.Combine(path, $"{fileName}.json"));
        }

        public static void WriteData(string data, string fileName, string rootPath)
        {
            var writer = OpenFileForWriting(fileName, rootPath);
            writer.Write(data);

            writer.Close();
            writer.Dispose();
        }

        public static string ReadData(string fileName, string rootPath)
        {
            var reader = OpenFileForReading(fileName, rootPath);
            var data = reader.ReadToEnd();

            reader.Close();
            reader.Dispose();

            return data;
        }

        public static TOutput Find<TOutput>(string fileName, Func<string, TOutput> mapper, string rootPath) where TOutput : class, new()
        {
            var data = ReadData(fileName, rootPath);

            return JsonSerializer.Deserialize<TOutput>(data);
        }

        public static void Update<TInput>(string fileName, TInput data, string rootPath) where TInput : class, new()
        {
            WriteData(JsonSerializer.Serialize<TInput>(data), fileName, rootPath);
        }
    }
}
