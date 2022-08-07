using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeComandService
{
    public class TreeService
    {
        // It is best to use the route to the Desktop as the RootDirectory, because some directories like C:, C:\Users etc. will throw System.UnauthorizedAccessException.
        public static string RootDirectory { get; private set; } = @"C:\Users\User\Desktop\"; //here goes the route
        internal DirectoryInfo _directoryInfo = new(RootDirectory);
        internal string _tree = string.Empty;
        internal string _folderName = string.Empty;

        internal void ShowFolderTree(List<FileSystemInfo> filesAndFolders, string path = "", bool last = false, bool first = true)
        {
            if (first)
            {
                HelperService.ShowRootFolder($"{_folderName}\n");
                HelperService.ShowPath("|\n");
                _tree += $"{_folderName}\n|\n";
                path += "";
            }
            else path += last ? "    " : "|   ";

            for (int i = 0; i < filesAndFolders.Count; i++)
            {
                if (filesAndFolders[i].GetType().Name == "DirectoryInfo")
                {
                    DirectoryInfo directory = (DirectoryInfo)filesAndFolders[i];
                    List<FileSystemInfo> list = directory.GetFileSystemInfos().ToList();
                    if (i == filesAndFolders.Count - 1)
                    {
                        HelperService.ShowPath($"{path}\\---");
                        _tree += $"{path}\\---";
                    }
                    else
                    {
                        HelperService.ShowPath($"{path}+---");
                        _tree += $"{path}+---";
                    }
                    HelperService.ShowDirectories($"{filesAndFolders[i].Name}\n");
                    _tree += $"{filesAndFolders[i].Name}\n";
                    ShowFolderTree(list, path, i == filesAndFolders.Count - 1, false);
                }
                else
                {
                    if (i == filesAndFolders.Count - 1)
                    {
                        HelperService.ShowPath($"{path}\\---");
                        _tree += $"{path}\\---";
                    }
                    else
                    {
                        HelperService.ShowPath($"{path}+---");
                        _tree += $"{path}+---";
                    }
                    HelperService.ShowFiles($"{filesAndFolders[i].Name}\n");
                    _tree += $"{filesAndFolders[i].Name}\n";
                }
            }
        }

        internal bool CheckIfFolderExists(string folderName)
        {
            DirectoryInfo[] folderInfo = _directoryInfo.GetDirectories(folderName, SearchOption.AllDirectories);
            if (folderInfo.Length != 0) return true;
            return false;
        }

        internal DirectoryInfo GetFolder(string folderName)
        {
            DirectoryInfo folderInfo = _directoryInfo.GetDirectories(folderName, SearchOption.AllDirectories).First();
            _folderName = folderInfo.Name;
            return folderInfo;
        }

        internal void CreateFile(string fileName)
        {
            string fullPath = $@"..\..\..\..\{fileName}.txt";
            if (File.Exists(fullPath)) HelperService.ThrowException($"\n\n\tTHE FILE {fileName} ALREADY EXISTS!\n\n");
            Console.WriteLine($"\nSuccessfully saved tree in file {fileName}.txt\n");
            File.WriteAllText(fullPath, _tree);
        }

        public void StartApp()
        {
            try
            {
                string inputOne = Console.ReadLine();
                string inputTwo = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputOne) && string.IsNullOrWhiteSpace(inputTwo))
                {
                    string currentDirectory = Directory.GetCurrentDirectory();
                    DirectoryInfo currentDirInfo = new(currentDirectory);
                    _folderName = currentDirInfo.Name;
                    List<FileSystemInfo> currentDirInfoList = currentDirInfo.EnumerateFileSystemInfos().ToList();
                    if (currentDirInfoList.Count == 0) HelperService.ThrowException("THE CHOOSEN FOLDER IS EMPTY!");
                    ShowFolderTree(currentDirInfoList);
                }
                else if (string.IsNullOrWhiteSpace(inputOne) && !string.IsNullOrWhiteSpace(inputTwo))
                {
                    if (!CheckIfFolderExists(inputTwo)) HelperService.ThrowException("THE CHOOSEN FOLDER DOESN'T EXIST!");
                    DirectoryInfo folderInfo = GetFolder(inputTwo);
                    List<FileSystemInfo> folderInfoList = folderInfo.EnumerateFileSystemInfos().ToList();
                    if (folderInfoList.Count == 0) HelperService.ThrowException("THE CHOOSEN FOLDER IS EMPTY!");
                    ShowFolderTree(folderInfoList);
                }
                else if (!string.IsNullOrWhiteSpace(inputOne) && string.IsNullOrWhiteSpace(inputTwo))
                {
                    if (!CheckIfFolderExists(inputOne)) HelperService.ThrowException("THE CHOOSEN FOLDER DOESN'T EXIST!");
                    DirectoryInfo folderInfo = GetFolder(inputOne);
                    List<FileSystemInfo> folderInfoList = folderInfo.EnumerateFileSystemInfos().ToList();
                    if (folderInfoList.Count == 0) HelperService.ThrowException("THE CHOOSEN FOLDER IS EMPTY!");
                    ShowFolderTree(folderInfoList);
                }
                else
                {
                    if (!CheckIfFolderExists(inputOne)) HelperService.ThrowException("THE CHOOSEN FOLDER DOESN'T EXIST!");
                    DirectoryInfo folderInfo = GetFolder(inputOne);
                    List<FileSystemInfo> folderInfoList = folderInfo.EnumerateFileSystemInfos().ToList();
                    ShowFolderTree(folderInfoList);
                    CreateFile(inputTwo);
                }
            }
            catch (Exception ex)
            {
                HelperService.ShowErrorMessage(ex.Message);
            }
        }

    }
}