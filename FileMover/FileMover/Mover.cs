using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMover
{
    public class Mover
    {
        public void MoveZipedFile()
        {
            var moveBegin = ConfigurationManager.AppSettings["Begin"];
            var movEnd = ConfigurationManager.AppSettings["End"];
            var rootDirectories = Directory.GetDirectories(moveBegin).Where(x => x.Contains("Partner_"));
            var filesToMove = new List<string>();
            foreach (var rootDirectory in rootDirectories)
            {
                var list = Directory.GetFiles(rootDirectory).Where(x => x.EndsWith(".zip")).ToList();
                if (list.Count != 0) filesToMove.AddRange(list);
            }
            if (filesToMove.Count == 0) return;
            var moveFolderName = string.Join("", filesToMove.First().Split('.')[1].Take(10));
            if (!Directory.GetDirectories(movEnd).Any(x => x.Contains(moveFolderName))) Directory.CreateDirectory($@"{movEnd}\{moveFolderName}");
            foreach (var fileToMove in filesToMove)
            {
                if (!fileToMove.Contains(moveFolderName)) continue;
                var destinationFile = $@"{movEnd}\{moveFolderName}\{fileToMove.Split(new[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).Last()}";
                File.Move(fileToMove, destinationFile);
            }
        }

        public void ZipAndMove()
        {
            var currentDir = ConfigurationManager.AppSettings["Begin"];
            var toMove = ConfigurationManager.AppSettings["End"];
            var extension = "zip";
            var fileNames = Helper.GetFileNames(currentDir);
            foreach (var fileName in fileNames)
            {
                var fileSize = Helper.GetFileSize(fileName);
                if (double.Parse(fileSize.Split(' ').First()) > 10 && fileSize.Split(' ').Last() == "MB")
                {
                    var destFileName = fileName.Replace(currentDir, string.Empty);
                    var tempDirectory = Helper.GetTempDirectory();
                    var extensionMover = Path.GetExtension(fileName);
                    var name = Path.GetFileNameWithoutExtension(fileName);
                    var toMoveTemp = $@"{tempDirectory}\{name}{extensionMover}";
                    File.Move(fileName, toMoveTemp);
                    ZipFile.CreateFromDirectory(tempDirectory, $@"{toMove}{destFileName}.{extension}", CompressionLevel.Optimal,
                        true, Encoding.UTF8);
                    File.Delete(toMoveTemp);
                    Directory.Delete(tempDirectory);
                }
            }
        }
    }
}
