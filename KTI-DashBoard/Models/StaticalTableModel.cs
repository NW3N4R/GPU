﻿using System.ComponentModel;

namespace KTI_DashBoard.Models
{
    public class StaticalTableModel
    {
        [DisplayName("ناو")]
        public string? Name { get; set; }

        [DisplayName("ڕەوشی کەسێتی")]
        public string? MartialStatus { get; set; }

        [DisplayName("لە دایک بوون")]
        public int Age { get; set; }

        [DisplayName("ئاین")]
        public string? Religion { get; set; }

        [DisplayName("نەتەوە")]
        public string? Nationality { get; set; }

        [DisplayName("پارێزگا")]
        public string? Province { get; set; }

        [DisplayName("جۆری خوێندن")]
        public string? EducationType { get; set; }

        [DisplayName("ساڵی دەرچوون")]
        public string? SchoolGraduation { get; set; }

        [DisplayName("بەش")]
        public string? Department { get; set; }

        [DisplayName("جۆری وەرگرتن")]
        public string? AcceptanceType { get; set; }

        [DisplayName("بەشە ناوخۆی؟")]
        public string? ResidenceType { get; set; }

        [DisplayName("ساڵی دەستپێک")]
        public string? StartingYear { get; set; }

        [DisplayName("قۆناغ")]
        public int Stage { get; set; }
    }
}
