using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catch_Wpf.ViewModel
{
    public class Field : ViewModelBase
    {
        private string _color;
        public int X { get; set; }
        public int Y { get; set; }
        public int Number { get; set; }
        public String Color
        {
            get { return _color; }
            set { _color = value; OnPropertyChanged(); }
        }
        public DelegateCommand? StepCommand { get; set; }
    }
}
