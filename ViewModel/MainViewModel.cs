using GalaSoft.MvvmLight.Command;
using LeaderSteel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LeaderSteel.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        private Page Mater = new Material();
        private Page Transp = new Transport();
        private Page WareHous = new Warehouse();
        private Page Corp = new Corpus();
        private Page Acco = new Accounts();
        private Page Start = new Start();

        public Page Page
        {
            get => Start;
            set => Set(ref Start, value);
        }

        public ICommand Maters
        {
            get
            {
                return new RelayCommand(() => Page = Mater);
            }
        }

        public ICommand Transps
        {
            get
            {
                return new RelayCommand(() => Page = Transp);
            }
        }

        public ICommand WareHouss
        {
            get
            {
                return new RelayCommand(() => Page = WareHous);
            }
        }

        public ICommand Corps
        {
            get
            {
                return new RelayCommand(() => Page = Corp);
            }
        }

        public ICommand Accou
        {
            get
            {
                return new RelayCommand(() => Page = Acco);
            }
        }
    }
}
