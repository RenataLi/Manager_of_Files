using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class Program
    {

        public static void menu()
        {
            
            Console.WriteLine("Команды, которые Вы можете выбрать:" + Environment.NewLine +
                "'drives'- Показать диски" + Environment.NewLine +
                "'сhangedrive'- Сменить диск" + Environment.NewLine +
                 "'сhangedir'- Сменить папку" + Environment.NewLine +
                "'showdir'-просмотр файлов и директорий." + Environment.NewLine +
                "'go'-переход по директориям и файлам." + Environment.NewLine +
                "'openfile'-вывод текста из файла на экран в выбранной кодировке." + Environment.NewLine +
                "'copy'- копирование файла." + Environment.NewLine +
                "'move'- перемещение файла в выбранную директорию." + Environment.NewLine +
                "'deletefile'-удаление файла." + Environment.NewLine +
                "'create'- создание файла в выбранной кодировке." + Environment.NewLine +
                "'plus'-конкатенация файлов" + Environment.NewLine
                );
        }


        public static void showAllDrives()
        {
            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine($"Диск {drives[i]}");
            }
        }

        public static void showDirectory(DirectoryInfo dir)
        {
            Console.WriteLine("Папки: ");
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                Console.WriteLine($"{d.Name}");
            }
            Console.WriteLine();
            Console.WriteLine("Файлы: ");
            foreach (FileInfo f in dir.GetFiles())
            {
                Console.WriteLine($"{f.Name}");
            }
        }

        public static void changeDir()
        {
            string input;
            while (true)
            {
                Console.WriteLine("Введите название директории");
                Console.WriteLine("Для возврата назад введите '..'");
                Console.Write("> ");
                input = Console.ReadLine();
                DirectoryInfo d = null;
                if (input == "..")
                {
                    d = currentPath.Parent;
                }
                else
                {
                    d = currentPath.GetDirectories().Where(o => o.Name.CompareTo(input) == 0).FirstOrDefault();
                }
                if (d != null)
                {
                    currentPath = d;
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка выбора диска!");
                }
            }
        }

        public static void changeDrive()
        {
            String input;
            while (true)
            {
                Console.WriteLine("Введите букву диска: ");
                input = Console.ReadLine();
                DriveInfo d = drives.Where(o => o.Name.Contains(input)).FirstOrDefault();
                if (d != null)
                {
                    currentPath = d.RootDirectory;
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка выбора диска!");
                }
            }
        }

        public static void openFile()
        {
            String input;
            while (true)
            {
                Console.WriteLine("Введите название файла: ");
                Console.Write("> ");
                input = Console.ReadLine();


                FileInfo f = currentPath.GetFiles().Where(o => o.Name.CompareTo(input) == 0).FirstOrDefault();
                if (f != null)
                {
                    Console.WriteLine("Выберите кодировку,в которой будет выведен текст: ");
                    Console.WriteLine("0 - UTF-8");
                    Console.WriteLine("1- UTF-32");
                    Console.WriteLine("2- ASCII");
                    Console.WriteLine("3- UTF-7");
                    string num = Console.ReadLine();
                    switch (num)
                    {
                        case "0":
                            Console.WriteLine($"{File.ReadAllText(f.FullName, Encoding.UTF8)}");
                            break;
                        case "1":
                            Console.WriteLine($"{File.ReadAllText(f.FullName, Encoding.UTF32)}");
                            break;
                        case "2":
                            Console.WriteLine($"{File.ReadAllText(f.FullName, Encoding.ASCII)}");
                            break;
                        case "3":
                            Console.WriteLine($"{File.ReadAllText(f.FullName, Encoding.UTF7)}");
                            break;
                        default:
                            Console.WriteLine("Пожалуйста,введите верную кодировку.");
                            break;
                    }



                    break;


                }
                else
                {
                    Console.WriteLine("Файл отсутствует!");
                }
            }
        }

        static DriveInfo[] drives;
        static DirectoryInfo currentPath;
        static FileInfo currentFile;

        
        static void Main(string[] args)
        {
            bool isRun = true;
            drives = DriveInfo.GetDrives();
            string input;
            showAllDrives();
            changeDrive();
            Console.WriteLine("Для выбора команд введите 'help'.");
            Console.WriteLine("");
            while (isRun)
            {
                Console.WriteLine($"{currentPath.FullName}");
                Console.Write("> ");
                input = Console.ReadLine();

                switch (input)
                {
                    case "help":
                        menu();
                        break;
                    case "changedrive":
                        changeDrive();
                        break;
                    case "changedir":
                        changeDir();
                        break;
                    case "showdir":
                        showDirectory(currentPath);
                        break;
                    case "openfile":
                        openFile();
                        break;
                    case "drives":
                        showAllDrives();
                        break;
                    case "selectfile":
                        break;
                    case "copy":
                        break;
                    case "move":
                        break;
                    case "deletefile":
                        break;
                    default:
                        Console.WriteLine("Команда не распознана!");
                        break;
                }
            }
        }
    }
}
