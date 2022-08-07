using System;
using System.IO;
using System.Linq;
using System.Text;

namespace HW7._8
{
    class Repository
    {
        Worker[] _worker;
        string _path;
        int currentID;

        public Repository(string path)
        {
            _path = path;
            _worker = new Worker[2];
            currentID = _worker.Length - 1;
        }

        private void Resize(bool flag)
        {
            if (flag)
                Array.Resize(ref this._worker, this._worker.Length * 2);
        }


        public Worker[] GetAllWorkers()
        {
            using (StreamReader reader = new StreamReader(_path, Encoding.UTF8))
            {
                string line;
                int tempCount = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] note = line.Split("#");
                    Resize(tempCount >= this._worker.Length);

                    if (_worker[tempCount] == null)
                    {
                        _worker[tempCount] = new Worker(
                        Convert.ToInt32(note[0]),
                        Convert.ToDateTime(note[1]),
                        note[2],
                        Convert.ToInt32(note[3]),
                        Convert.ToInt32(note[4]),
                        Convert.ToDateTime(note[5]),
                        note[6]);                    
                        tempCount++;
                        currentID = tempCount;
                    }

                }                    

                reader.Dispose();
            }
            return _worker;
        }

        public void AddWorker(Worker worker)
        {
            Resize(currentID >= this._worker.Length);
            _worker[currentID] = worker;
            WriteToFile(worker);
            currentID++;
        }

        private void WriteToFile(Worker worker)
        {
            using (StreamWriter streamWriter = new StreamWriter(_path, true, Encoding.UTF8))
            {
                if (worker != null)
                {
                    streamWriter.Write(Convert.ToString(worker.Id));
                    streamWriter.Write("#");

                    streamWriter.Write(Convert.ToString(worker.AddNote));
                    streamWriter.Write("#");

                    streamWriter.Write(worker.Fio);
                    streamWriter.Write("#");

                    streamWriter.Write(Convert.ToString(worker.Age));
                    streamWriter.Write("#");

                    streamWriter.Write(Convert.ToString(worker.Growth));
                    streamWriter.Write("#");

                    streamWriter.Write(Convert.ToString(worker.BDay));
                    streamWriter.Write("#");

                    streamWriter.Write(worker.FromBDay);
                    streamWriter.Write('\n');
                }
                streamWriter.Dispose();
            }

        }

        public Worker GetWorkerById(int id)
        {
            return _worker[id - 1];
        }

        public void DeleteWorker(int id)
        {
            bool flagIn = false;

            for (int i = 0; i < _worker.Length; i++)
            {
                if (_worker[i] != null && id == _worker[i].Id)
                {
                    _worker[i] = null;
                    flagIn = true;
                }
            }

            if (flagIn)
            {
                File.WriteAllText(_path, String.Empty);

                foreach (var item in _worker)
                {
                    WriteToFile(item);
                }

            }
            else
                Console.WriteLine("Данной записи нет!!!");

            _worker = null;
            _worker = new Worker[2];


            GetAllWorkers();

        }

        public Worker[] GetWorkerBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            var tempWorker = _worker.Where(worker => worker != null).OrderBy(worker => worker.AddNote);
            Worker[] resultWorker = new Worker[tempWorker.Count()];
            int tempCount = 0;

            foreach (var item in tempWorker)
            {
                if (item.AddNote >= dateFrom && item.AddNote <= dateTo)
                {
                    resultWorker[tempCount] = item;
                    tempCount++;
                }
            }

            return resultWorker;
        }

        public void EditToWorker(int id)
        {
            var tempWorker = _worker.Where(worker => worker != null).OrderBy(worker => worker.Id);
            int tempCount = 0;

            File.WriteAllText(_path, String.Empty);

            foreach (var item in tempWorker)
            {
                if (id == item.Id)
                {
                    item.Fio = Console.ReadLine();

                    _worker[tempCount] = item;
                }                
                WriteToFile(_worker[tempCount]);

                tempCount++;
            }
        }
    }
}