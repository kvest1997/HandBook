using System;

namespace HW7._8
{
    class Worker
    {
        private int _ID;
        private string _FIO;
        private int _age;
        private int _growth;
        private DateTime _bDay;
        private string _fromBDay;
        private DateTime _addNote;

        public Worker()
        {

        }

        public Worker(int id, DateTime addNote, string fio, int age, int growth, DateTime bDay, string fromBDay)
        {
            this._ID = id;
            this._FIO = fio;
            this._age = age;
            this._growth = growth;
            this._bDay = bDay;
            this._fromBDay = fromBDay;
            this._addNote = addNote;
        }

        public int Id
        {
            get => _ID;
            set => _ID = value;
        }

        public string Fio
        {
            get => _FIO;
            set => _FIO = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public int Growth
        {
            get => _growth;
            set => _growth = value;
        }

        public DateTime BDay
        {
            get => _bDay;
            set => _bDay = value;
        }

        public string FromBDay
        {
            get => _fromBDay;
            set => _fromBDay = value;
        }

        public DateTime AddNote
        {
            get => _addNote;
            set => _addNote = value;
        }

    }
}
