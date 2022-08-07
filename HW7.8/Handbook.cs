using System;
using System.IO;

namespace HW7._8
{
    class Handbook
    {
        private Repository repository;
        private Worker[] worker;
        private int currentID;
        private string path = @"HandBook.txt";
        private FileInfo fileInfo;

        public Handbook()
        {
            fileInfo = new FileInfo(path);

            if (!File.Exists(path))
                File.Create(path);

            repository = new Repository(path);

            if (fileInfo.Length != 0)
                worker = repository.GetAllWorkers();

            for (int i = 0; i < worker.Length; i++)
            {
                if (worker[i] != null)
                {
                    currentID = worker[i].Id + 1;
                }
            }
        }

        public void Start()
        {
            int selection;


            do
            {
                Console.Clear();
                Console.Write(Menu());

                selection = int.Parse(Console.ReadLine());

                switch (selection)
                {
                    case 1:
                        Console.Clear();

                        worker = repository.GetAllWorkers();

                        if (worker == null)
                        {
                            Console.WriteLine("Файл пустой");
                        }
                        else
                        {
                            Console.WriteLine($"{"ID",5} {"Дата записи",20} {"ФИО",35} {"Возраст",10} {"Рост",10} {"Дата рождения",25} {"Родной город",25}");

                            foreach (var item in worker)
                            {
                                if (item != null && File.Exists(path))
                                {
                                    Console.WriteLine($"{item.Id, 5} {item.AddNote, 20} {item.Fio, 35} {item.Age, 10} {item.Growth, 10} {item.BDay.ToShortDateString(), 25} {item.FromBDay, 25}");
                                }
                            }
                        }
                        Console.ReadKey();
                        break; // просмотр всех записей
                    case 2:
                        Console.Clear();
                        int tempID;

                        Console.Write("Введите ID записи которую показать: ");
                        tempID = int.Parse(Console.ReadLine());

                        Console.WriteLine($"{"ID",5} {"Дата записи",20} {"ФИО",35} {"Возраст",10} {"Рост",10} {"Дата рождения",25} {"Родной город",25}");

                        if (tempID <= worker.Length - 1 && repository.GetWorkerById(tempID) != null)
                        {
                            Console.WriteLine($"{repository.GetWorkerById(tempID).Id, 5} " +
                                $"{repository.GetWorkerById(tempID).AddNote, 20} " +
                                $"{repository.GetWorkerById(tempID).Fio, 35} " +
                                $"{repository.GetWorkerById(tempID).Age, 10} " +
                                $"{repository.GetWorkerById(tempID).Growth, 10} " +
                                $"{repository.GetWorkerById(tempID).BDay.ToShortDateString(), 25} " +
                                $"{repository.GetWorkerById(tempID).FromBDay, 25}");
                        }
                        else
                            Console.WriteLine("Такого пользователя нет!!!");

                        Console.ReadKey();
                        break; //Просмотр одной записи по ID
                    case 3:
                        Console.Clear();
                        string fio, fromTo;
                        int age, growth;
                        string bDay;

                        Console.Write("Введите Фио: ");
                        fio = Console.ReadLine();

                        Console.Write("Введите возраст: ");
                        age = int.Parse(Console.ReadLine());

                        Console.Write("Введите рост: ");
                        growth = int.Parse(Console.ReadLine());

                        Console.Write("Введите дату рождения(хх.хх.хххх): ");
                        bDay = Console.ReadLine();

                        Console.Write("Введите город рождения: ");
                        fromTo = Console.ReadLine();

                        repository.AddWorker(new Worker(currentID,
                                                        DateTime.Now,
                                                        fio,
                                                        age,
                                                        growth,
                                                        DateTime.Parse(bDay),
                                                        fromTo));

                        break; //Созадние новой записи 
                    case 4:
                        Console.Clear();

                        Console.Write("Номер записи которую надо удалить: ");
                        tempID = int.Parse(Console.ReadLine());

                        repository.DeleteWorker(tempID);

                        break; //Удаление записи по ID
                    case 5:
                        Console.Clear();
                        string dateFrom, dateTo;

                        Console.Write("Введите дату с которой начать отбор: ");
                        dateFrom = Console.ReadLine();

                        Console.Write("Введите дату окончания отбора: ");
                        dateTo = Console.ReadLine();

                        Console.WriteLine($"{"ID",5} {"Дата записи",20} {"ФИО",35} {"Возраст",10} {"Рост",10} {"Дата рождения",25} {"Родной город",25}");
                        foreach (var item in repository.GetWorkerBetweenTwoDates(DateTime.Parse(dateFrom), DateTime.Parse(dateTo)))
                        {
                            if (item != null)
                            {
                                Console.WriteLine($"{item.Id} {item.AddNote} {item.Fio} {item.Age} {item.Growth} {item.BDay} {item.FromBDay}");
                            }
                        }
                        Console.ReadKey();

                        break; //Загрузка записей в диапозоне дат
                    case 6:
                        Console.Clear();

                        Console.Write("Введите в какой записи хотите поменять ФИО: ");
                        tempID = int.Parse(Console.ReadLine());

                        Console.Write("Введите новое ФИО: ");
                        repository.EditToWorker(tempID); 
                        
                        break;
                }
            } while (selection != 0);

        }

        private string Menu()
        {
            string info =
                @"1. Просмотр всех записей
2. Просмотр одной записи по ID
3. Создание новой записи
4. Удаление записи по ID
5. Загрузка записей в диапозоне дат
6. Изменение ФИО по ID
0. Выход
Введите номер операции: ";

            return info;
        }
    }
}
