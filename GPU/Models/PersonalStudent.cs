﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GPU.Models
{
    public class PersonalStudent
    {
       
        [DisplayName("ئایدی")]
        public int Id { get; set; }

        [DisplayName("ناوی خوێندکار")]
        public string? Name { get; set; }

        [DisplayName("ساڵی لەدایک بوون")]
        public int Age { get; set; }

        [DisplayName("ڕەگەز")]
        public string? Sex { get; set; }

        [DisplayName("ڕەوشی کەسێتی")]
        public string? MartialStatus { get; set; }

        [DisplayName("جۆری خوێن")]
        public string? BloodGroup { get; set; }

        [DisplayName("ئاین")]
        public string? Religion { get; set; }

        [DisplayName("ژ. پێناسی کەسی")]
        public string? IdentityNo { get; set; }

        [DisplayName("نەتەوە")]
        public string? Nationality { get; set; }

        [DisplayName("ژ. فۆرمی بەشە خۆراک")]
        public string? RationingFormNo { get; set; }

        [DisplayName("وێنەی کەسی")]
        public IFormFile? PersonalPicture { get; set; }
    }
}
