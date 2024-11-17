using System.ComponentModel;

namespace KTI_DashBoard.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class InvoiceInfo : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string? _invoiceId;
        private string? _invoiceDate;
        private string? _amount;
        private bool _isFirst;
        private bool _isArchive;
        private int _sid;

        public int id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        [DisplayName("ناوی خوێندکار")]
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        [DisplayName("ژمارەی وەسڵ")]
        public string? InvoiceId
        {
            get => _invoiceId;
            set => SetField(ref _invoiceId, value);
        }

        [DisplayName("بەرواری وەسڵ")]
        public string? InvoiceDate
        {
            get => _invoiceDate;
            set => SetField(ref _invoiceDate, value);
        }

        [DisplayName("بڕ")]
        public string? Amount
        {
            get => _amount;
            set => SetField(ref _amount, value);
        }

        public bool isFirst
        {
            get => _isFirst;
            set => SetField(ref _isFirst, value);
        }

        public bool isArchive
        {
            get => _isArchive;
            set => SetField(ref _isArchive, value);
        }

        public int SID
        {
            get => _sid;
            set => SetField(ref _sid, value);
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
