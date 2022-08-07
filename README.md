# TheTreeCommand
Console application for reimplementing the "tree" command from the command-line in Windows.

When started, will output the files and folders within a certain folder. The folders are colored blue, while the files red.

The application should take up to two command line parameters

- if the application is started with no parameters, then it will work in the current folder, and it will output the result to the console.
- if the application is started with a single parameter, then it will treat that parameter as a folder name, and list all the files and folders within it.
- if the application is started with two parameters, then it will treat the first parameter as a folder name, and list all the files and folders within it. The second parameter should be the full name of a file where the output will be stored.

* To use the console application you first need to specify the [RootDirectory](https://github.com/ilijamitev/TheTreeCommand/blob/main/TheTreeComand/TreeComandService/TreeService.cs#:~:text=public%20static%20string-,RootDirectory,-%7B%20get%3B) in the TreeService.cs file (12th line)
