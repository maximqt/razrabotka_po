using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Xml.Linq;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Вывести информацию о логических дисках");
                Console.WriteLine("2. Создать текстовый файл");
                Console.WriteLine("3. Записать текст в файл");
                Console.WriteLine("4. Прочитать текстовый файл");
                Console.WriteLine("5. Удалить текстовый файл");
                Console.WriteLine("6. Создать JSON файл");
                Console.WriteLine("7. Записать данные в JSON файл");
                Console.WriteLine("8. Прочитать JSON файл");
                Console.WriteLine("9. Удалить JSON файл");
                Console.WriteLine("10. Создать XML файл");
                Console.WriteLine("11. Записать данные в XML файл");
                Console.WriteLine("12. Прочитать XML файл");
                Console.WriteLine("13. Удалить XML файл");
                Console.WriteLine("14. Создать zip архив и добавить файл");
                Console.WriteLine("15. Извлечь файл из zip архива");
                Console.WriteLine("16. Удалить файл или архив"); // Новая команда для удаления любого файла или архива
                Console.WriteLine("0. Выход");
                Console.Write("Введите номер действия: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayDrives();
                        break;
                    case "2":
                        Console.Write("Введите имя текстового файла (без расширения): ");
                        string txtFileName = Console.ReadLine();
                        CreateFile($"{txtFileName}.txt");
                        break;
                    case "3":
                        Console.Write("Введите имя текстового файла для записи (без расширения): ");
                        string txtFileWriteName = Console.ReadLine();
                        WriteToFile($"{txtFileWriteName}.txt");
                        break;
                    case "4":
                        Console.Write("Введите имя текстового файла для чтения (без расширения): ");
                        string txtFileReadName = Console.ReadLine();
                        ReadFile($"{txtFileReadName}.txt");
                        break;
                    case "5":
                        Console.Write("Введите имя текстового файла для удаления (без расширения): ");
                        string txtFileDeleteName = Console.ReadLine();
                        DeleteFile($"{txtFileDeleteName}.txt");
                        break;
                    case "6":
                        Console.Write("Введите имя JSON файла (без расширения): ");
                        string jsonFileName = Console.ReadLine();
                        CreateFile($"{jsonFileName}.json");
                        break;
                    case "7":
                        Console.Write("Введите имя JSON файла для записи (без расширения): ");
                        string jsonFileWriteName = Console.ReadLine();
                        WriteToJsonFile($"{jsonFileWriteName}.json");
                        break;
                    case "8":
                        Console.Write("Введите имя JSON файла для чтения (без расширения): ");
                        string jsonFileReadName = Console.ReadLine();
                        ReadFile($"{jsonFileReadName}.json");
                        break;
                    case "9":
                        Console.Write("Введите имя JSON файла для удаления (без расширения): ");
                        string jsonFileDeleteName = Console.ReadLine();
                        DeleteFile($"{jsonFileDeleteName}.json");
                        break;
                    case "10":
                        Console.Write("Введите имя XML файла (без расширения): ");
                        string xmlFileName = Console.ReadLine();
                        CreateXmlFile($"{xmlFileName}.xml");
                        break;
                    case "11":
                        Console.Write("Введите имя XML файла для записи (без расширения): ");
                        string xmlFileWriteName = Console.ReadLine();
                        WriteToXmlFile($"{xmlFileWriteName}.xml");
                        break;
                    case "12":
                        Console.Write("Введите имя XML файла для чтения (без расширения): ");
                        string xmlFileReadName = Console.ReadLine();
                        ReadFile($"{xmlFileReadName}.xml");
                        break;
                    case "13":
                        Console.Write("Введите имя XML файла для удаления (без расширения): ");
                        string xmlFileDeleteName = Console.ReadLine();
                        DeleteFile($"{xmlFileDeleteName}.xml");
                        break;
                    case "14":
                        CreateZipArchive("archive.zip");
                        break;
                    case "15":
                        Console.Write("Введите имя файла для извлечения из архива (с расширением): ");
                        string fileName = Console.ReadLine();
                        ExtractZipFile("archive.zip", fileName);
                        break;
                    case "16":
                        Console.Write("Введите имя файла или архива для удаления (с расширением): ");
                        string fileOrArchiveName = Console.ReadLine();
                        DeleteFile(fileOrArchiveName);
                        break;
                    case "0":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, выберите снова.");
                        break;
                }
            }
        }

        static void DisplayDrives()
        {
            Console.WriteLine("Список логических дисков:");
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                Console.WriteLine($"Имя: {drive.Name}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
                    Console.WriteLine($"Размер: {drive.TotalSize / (1024 * 1024 * 1024)} ГБ");
                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace / (1024 * 1024 * 1024)} ГБ");
                    Console.WriteLine($"Файловая система: {drive.DriveFormat}");
                }
                Console.WriteLine();
            }
        }

        static void CreateFile(string path)
        {
            using (FileStream fs = File.Create(path))
            {
                Console.WriteLine($"Файл {path} создан.");
            }
        }

        static void WriteToFile(string path)
        {
            Console.WriteLine("Введите строку для записи в файл:");
            string userText = Console.ReadLine();
            File.WriteAllText(path, userText);
            Console.WriteLine($"Текст записан в файл {path}.");
        }

        static void ReadFile(string path)
        {
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                Console.WriteLine($"Содержимое файла {path}:\n{content}");
            }
            else
            {
                Console.WriteLine($"Файл {path} не найден.");
            }
        }

        static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine($"Файл {path} удалён.");
            }
            else
            {
                Console.WriteLine($"Файл {path} не найден для удаления.");
            }
        }

        static void WriteToJsonFile(string path)
        {
            Console.WriteLine("Введите имя для объекта JSON:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите текстовое значение для объекта JSON:");
            string value = Console.ReadLine();

            var obj = new { Name = name, Value = value };
            string json = JsonSerializer.Serialize(obj);
            File.WriteAllText(path, json);
            Console.WriteLine($"Данные записаны в JSON файл {path}.");
        }

        static void CreateXmlFile(string path)
        {
            XDocument doc = new XDocument(
                new XElement("Root",
                    new XElement("Element", "InitialValue")
                ));
            doc.Save(path);
            Console.WriteLine($"XML файл {path} создан.");
        }

        static void WriteToXmlFile(string path)
        {
            if (File.Exists(path))
            {
                XDocument doc = XDocument.Load(path);
                Console.WriteLine("Введите значение для нового элемента XML:");
                string newValue = Console.ReadLine();
                doc.Root.Add(new XElement("NewElement", newValue));
                doc.Save(path);
                Console.WriteLine($"Данные добавлены в XML файл {path}.");
            }
            else
            {
                Console.WriteLine($"XML файл {path} не найден.");
            }
        }

        static void CreateZipArchive(string zipPath)
        {
            Console.WriteLine("Введите полный путь к файлу, который хотите добавить в архив:");
            string fileToAdd = Console.ReadLine();

            if (!File.Exists(fileToAdd))
            {
                Console.WriteLine($"Ошибка: файл {fileToAdd} не найден.");
                return;
            }

            using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
            {
                archive.CreateEntryFromFile(fileToAdd, Path.GetFileName(fileToAdd));
                Console.WriteLine($"Файл {fileToAdd} добавлен в архив {zipPath}.");
            }

            FileInfo zipFileInfo = new FileInfo(zipPath);
            zipFileInfo.Refresh();
            Console.WriteLine($"Размер архива {zipPath}: {zipFileInfo.Length / 1024} КБ");
        }

        static void ExtractZipFile(string zipPath, string fileName)
        {
            if (!File.Exists(zipPath))
            {
                Console.WriteLine($"Ошибка: архив {zipPath} не найден.");
                return;
            }

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                var entry = archive.GetEntry(fileName);
                if (entry != null)
                {
                    entry.ExtractToFile(fileName, true);
                    Console.WriteLine($"Файл {fileName} извлечён из архива {zipPath}.");
                }
                else
                {
                    Console.WriteLine($"Файл {fileName} не найден в архиве {zipPath}.");
                }
            }
        }
    }
}
