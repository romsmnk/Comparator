using System;
using System.Collections.Generic;
using System.IO;

namespace CompareLibrary
{
    public class Compare
    {
        public string firstDirPath { get; private set; }
        public string secondDirPath { get; private set; }

        public List<ComparedFile> firstDir { get; private set; }
        public List<ComparedFile> secondDir { get; private set; }

        public Dictionary<int, string> status = new Dictionary<int, string>()
        {
            { 0, "None"},
            { 1, "Файл существует только в первой директории" },
            { 2, "Файл существует только во второй директории" },
            { 3, "Файл найден в обеих директориях и имеет одинаковый размер" },
            { 4, "Файл найден в обеих директориях, но имеет разный размер" }


        };

        public Compare(string firstDirPath, string secondDirPath)
        {
            this.firstDirPath = firstDirPath;
            this.secondDirPath = secondDirPath;

            firstDir = GetListsOfFiles(firstDirPath);
            secondDir = GetListsOfFiles(secondDirPath);

            CompareFiles();
        }

        private List<ComparedFile> GetListsOfFiles(string path)
        {
            List<ComparedFile> dir = new List<ComparedFile>();
            if (Directory.Exists(path))
                foreach (var file in Directory.GetFiles(path))
                {
                    FileInfo FileInfo = new FileInfo(file);

                    dir.Add(new ComparedFile()
                    {
                        Name = FileInfo.Name,
                        Size = Convert.ToInt32(FileInfo.Length),
                        LastModified = FileInfo.LastWriteTime,
                        Status = status[0]
                    });
                }
            return dir;
        }

        public void CompareFiles()
        {
            foreach (var firstPath in firstDir)
            {
                foreach (var secondPath in secondDir)
                {
                    if (firstPath.Name == secondPath.Name)
                    {
                        if (firstPath.Size == secondPath.Size)
                        {
                            if (firstPath.Status == status[0])
                                firstPath.Status = status[3];

                            if (secondPath.Status == status[0])
                                secondPath.Status = status[3];
                        }
                        else
                        {
                            if (firstPath.Status == status[0])
                                firstPath.Status = status[4];
                            if (secondPath.Status == status[0])
                                secondPath.Status = status[4];
                        }
                    }
                }
            }

            foreach (var firstPath in firstDir)
                if (firstPath.Status == status[0])
                    firstPath.Status = status[1];

            foreach (var secondPath in secondDir)
                if (secondPath.Status == status[0])
                    secondPath.Status = status[2];
        }

    }
}
