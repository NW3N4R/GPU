namespace KTI_DashBoard.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class UsersAccessToDepsModel : INotifyPropertyChanged
    {
        private int _id;
        private int _usrid;
        private int _depid;
        private string _empName;
        private string _depName;

        public int id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        public int usrid
        {
            get => _usrid;
            set => SetField(ref _usrid, value);
        }

        public int depid
        {
            get => _depid;
            set => SetField(ref _depid, value);
        }

        public string EmpName
        {
            get => _empName;
            set => SetField(ref _empName, value);
        }

        public string DepName
        {
            get => _depName;
            set => SetField(ref _depName, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

}
