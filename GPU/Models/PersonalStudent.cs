﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GPU.Models
{
    public class PersonalStudent
    {
        [DisplayName("ئاید")]
        public int Id { get; set; }

        [DisplayName("ناو")]
        public  string Name { get; set; }

        [Range(16, 60, ErrorMessage = "تەمەن پێویستە لە نێوان ١٦ بۆ ٦٠ ساڵ بێت")]
        [DisplayName("تەمەن")]
        public  int Age { get; set; }

        [DisplayName("ڕەگەز")]
        public  string Sex { get; set; }


        [DisplayName("ڕەوشی کەسێتی")]
        public  string MartialStatus { get; set; }

        [DisplayName("جۆری خوێن")]
        public  string BloodGroup { get; set; }

        [DisplayName("ئاین")]
        public  string Religion { get; set; }

        [DisplayName("ژ. پێناسی کەسی")]
        public  string IdentityNo { get; set; }

        [DisplayName("نەتەوە")]
        public  string Nationality { get; set; }

        [DisplayName("ژ. فۆرمی بەشە خۆراک")]
        public  string RationingFormNo { get; set; }
    }
}