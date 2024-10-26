import os
import json
import xml.etree.ElementTree as ET
import zipfile
import psutil

def display_drives():
    print("Список логических дисков:")
    partitions = psutil.disk_partitions()
    for partition in partitions:
        print(f"Имя: {partition.device}")
        try:
            usage = psutil.disk_usage(partition.mountpoint)
            print(f"Метка тома: {partition.opts}")
            print(f"Размер: {usage.total / (1024 * 1024 * 1024):.2f} ГБ")
            print(f"Свободное пространство: {usage.free / (1024 * 1024 * 1024):.2f} ГБ")
            print(f"Файловая система: {partition.fstype}")
        except PermissionError:
            print("Нет доступа к информации о диске.")
        print()

def create_file(path):
    with open(path, 'w') as f:
        print(f"Файл {path} создан.")

def write_to_file(path):
    user_text = input("Введите строку для записи в файл: ")
    with open(path, 'w') as f:
        f.write(user_text)
    print(f"Текст записан в файл {path}.")

def read_file(path):
    if os.path.exists(path):
        with open(path, 'r') as f:
            content = f.read()
        print(f"Содержимое файла {path}:\n{content}")
    else:
        print(f"Файл {path} не найден.")

def delete_file(path):
    if os.path.exists(path):
        os.remove(path)
        print(f"Файл {path} удалён.")
    else:
        print(f"Файл {path} не найден для удаления.")

def write_to_json_file(path):
    name = input("Введите имя для объекта JSON: ")
    value = input("Введите текстовое значение для объекта JSON: ")
    obj = {"Name": name, "Value": value}
    with open(path, 'w') as f:
        json.dump(obj, f)
    print(f"Данные записаны в JSON файл {path}.")

def create_xml_file(path):
    root = ET.Element("Root")
    element = ET.SubElement(root, "Element")
    element.text = "InitialValue"
    tree = ET.ElementTree(root)
    tree.write(path)
    print(f"XML файл {path} создан.")

def write_to_xml_file(path):
    if os.path.exists(path):
        tree = ET.parse(path)
        root = tree.getroot()
        new_value = input("Введите значение для нового элемента XML: ")
        ET.SubElement(root, "NewElement", text=new_value)
        tree.write(path)
        print(f"Данные добавлены в XML файл {path}.")
    else:
        print(f"XML файл {path} не найден.")

def create_zip_archive(zip_path):
    file_to_add = input("Введите полный путь к файлу, который хотите добавить в архив: ")
    if not os.path.exists(file_to_add):
        print(f"Ошибка: файл {file_to_add} не найден.")
        return
    with zipfile.ZipFile(zip_path, 'w') as zip_file:
        zip_file.write(file_to_add, os.path.basename(file_to_add))
        print(f"Файл {file_to_add} добавлен в архив {zip_path}.")

def extract_zip_file(zip_path, file_name):
    if not os.path.exists(zip_path):
        print(f"Ошибка: архив {zip_path} не найден.")
        return
    with zipfile.ZipFile(zip_path, 'r') as zip_file:
        try:
            zip_file.extract(file_name)
            print(f"Файл {file_name} извлечён из архива {zip_path}.")
        except KeyError:
            print(f"Файл {file_name} не найден в архиве {zip_path}.")

def main():
    while True:
        print("\nВыберите действие:")
        print("1. Вывести информацию о логических дисках")
        print("2. Создать текстовый файл")
        print("3. Записать текст в файл")
        print("4. Прочитать текстовый файл")
        print("5. Удалить текстовый файл")
        print("6. Создать JSON файл")
        print("7. Записать данные в JSON файл")
        print("8. Прочитать JSON файл")
        print("9. Удалить JSON файл")
        print("10. Создать XML файл")
        print("11. Записать данные в XML файл")
        print("12. Прочитать XML файл")
        print("13. Удалить XML файл")
        print("14. Создать zip архив и добавить файл")
        print("15. Извлечь файл из zip архива")
        print("16. Удалить файл или архив")
        print("0. Выход")
        choice = input("Введите номер действия: ")

        if choice == "1":
            display_drives()
        elif choice == "2":
            txt_file_name = input("Введите имя текстового файла (без расширения): ")
            create_file(f"{txt_file_name}.txt")
        elif choice == "3":
            txt_file_write_name = input("Введите имя текстового файла для записи (без расширения): ")
            write_to_file(f"{txt_file_write_name}.txt")
        elif choice == "4":
            txt_file_read_name = input("Введите имя текстового файла для чтения (без расширения): ")
            read_file(f"{txt_file_read_name}.txt")
        elif choice == "5":
            txt_file_delete_name = input("Введите имя текстового файла для удаления (без расширения): ")
            delete_file(f"{txt_file_delete_name}.txt")
        elif choice == "6":
            json_file_name = input("Введите имя JSON файла (без расширения): ")
            create_file(f"{json_file_name}.json")
        elif choice == "7":
            json_file_write_name = input("Введите имя JSON файла для записи (без расширения): ")
            write_to_json_file(f"{json_file_write_name}.json")
        elif choice == "8":
            json_file_read_name = input("Введите имя JSON файла для чтения (без расширения): ")
            read_file(f"{json_file_read_name}.json")
        elif choice == "9":
            json_file_delete_name = input("Введите имя JSON файла для удаления (без расширения): ")
            delete_file(f"{json_file_delete_name}.json")
        elif choice == "10":
            xml_file_name = input("Введите имя XML файла (без расширения): ")
            create_xml_file(f"{xml_file_name}.xml")
        elif choice == "11":
            xml_file_write_name = input("Введите имя XML файла для записи (без расширения): ")
            write_to_xml_file(f"{xml_file_write_name}.xml")
        elif choice == "12":
            xml_file_read_name = input("Введите имя XML файла для чтения (без расширения): ")
            read_file(f"{xml_file_read_name}.xml")
        elif choice == "13":
            xml_file_delete_name = input("Введите имя XML файла для удаления (без расширения): ")
            delete_file(f"{xml_file_delete_name}.xml")
        elif choice == "14":
            create_zip_archive("archive.zip")
        elif choice == "15":
            file_name = input("Введите имя файла для извлечения из архива (с расширением): ")
            extract_zip_file("archive.zip", file_name)
        elif choice == "16":
            file_or_archive_name = input("Введите имя файла или архива для удаления (с расширением): ")
            delete_file(file_or_archive_name)
        elif choice == "0":
            print("Выход из программы.")
            break
        else:
            print("Неверный выбор. Пожалуйста, выберите снова.")

if __name__ == "__main__":
    main()
