using System.ComponentModel;

namespace KTI_DashBoard.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class PersonalStudent : INotifyPropertyChanged
    {
        private int _id;
        private string? _name;
        private int _age;
        private string? _sex;
        private string? _martialStatus;
        private string? _bloodGroup;
        private string? _religion;
        private string? _identityNo;
        private string? _nationality;
        private string? _rationingFormNo;

        [DisplayName("ئاید")]
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        [DisplayName("ناوی خوێندکار")]
        public string? Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        [DisplayName("ساڵی لەدایک بوون")]
        public int Age
        {
            get => _age;
            set => SetField(ref _age, value);
        }

        [DisplayName("ڕەگەز")]
        public string? Sex
        {
            get => _sex;
            set => SetField(ref _sex, value);
        }

        [DisplayName("ڕەوشی کەسێتی")]
        public string? MartialStatus
        {
            get => _martialStatus;
            set => SetField(ref _martialStatus, value);
        }

        [DisplayName("جۆری خوێن")]
        public string? BloodGroup
        {
            get => _bloodGroup;
            set => SetField(ref _bloodGroup, value);
        }

        [DisplayName("ئاین")]
        public string? Religion
        {
            get => _religion;
            set => SetField(ref _religion, value);
        }

        [DisplayName("ژ. پێناسی کەسی")]
        public string? IdentityNo
        {
            get => _identityNo;
            set => SetField(ref _identityNo, value);
        }

        [DisplayName("نەتەوە")]
        public string? Nationality
        {
            get => _nationality;
            set => SetField(ref _nationality, value);
        }

        [DisplayName("ژ. فۆرمی بەشە خۆراک")]
        public string? RationingFormNo
        {
            get => _rationingFormNo;
            set => SetField(ref _rationingFormNo, value);
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
