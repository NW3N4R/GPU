namespace KTI_DashBoard.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class employeesModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private bool _stuList;
        private string _stuListContent;
        private bool _archList;
        private string _archListContent;

        public int id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public bool StuList
        {
            get => _stuList;
            set => SetField(ref _stuList, value);
        }

        public string stuListContent
        {
            get => _stuListContent;
            set => SetField(ref _stuListContent, value);
        }

        public bool ArchList
        {
            get => _archList;
            set => SetField(ref _archList, value);
        }

        public string ArchListContent
        {
            get => _archListContent;
            set => SetField(ref _archListContent, value);
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
