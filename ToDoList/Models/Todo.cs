using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class Todo : INotifyPropertyChanged
    {
        private int _id;
        public int Id 
        { 
            get => _id; 
            set
            {
                if (_id == value) 
                {
                    return;
                }
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        private string _todoname;
        public string TodoName
        {
            get => _todoname;
            set 
            {
                if (_todoname == value) 
                {
                    return;
                }
                _todoname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TodoName)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
