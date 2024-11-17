namespace KTI_DashBoard.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class UsersModel : INotifyPropertyChanged
    {
        private int _id;
        private string _empName;
        private string _userName;
        private string _password;
        private int _empId;
        private bool _canDelete = false;

        public int id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        public string EmpName
        {
            get => _empName;
            set => SetField(ref _empName, value);
        }

        public string UserName
        {
            get => _userName;
            set => SetField(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => SetField(ref _password, value);
        }

        public int EMPID
        {
            get => _empId;
            set => SetField(ref _empId, value);
        }

        public bool CanDelete
        {
            get => _canDelete;
            set => SetField(ref _canDelete, value);
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
