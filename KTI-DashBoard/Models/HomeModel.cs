using System.ComponentModel;

namespace KTI_DashBoard.Models
{
    public class HomeModel
    {
        public int RowCount { get; set; }
       
        [DisplayName("خوێندکاری تەکنەلۆژیای زانیاری")]
        public int ITCount { get; set; }

        [DisplayName("خوێندکاری پەرستاری")]
        public int NurseCount { get; set; }

        [DisplayName("خوێندکاری تەکنیکی ڤیتەرنەری")]
        public int VitCount { get; set; }

        [DisplayName("خوێندکاری پڕۆژە کشتوکاڵیەکان")]
        public int FarmingCount { get; set; }

        [DisplayName("خوێندکاری ڕوو پێوی")]
        public int MeasureMentCount { get; set; }

        [DisplayName("خوێندکاری کارگێڕی کار")]
        public int AdminiCount { get; set; }

        [DisplayName("خوێندکار لە خوێندندایە")]
        public int StudentListCount { get; set; }

        [DisplayName("خوێندکارانی ساڵانی پێشوو")]
        public int ArchiveCount { get; set; }

        [DisplayName("خوێندکاری نێر")]
        public int MaleCount { get; set; }

        [DisplayName("خوێندکاری مێ")]
        public int FemaleCount { get; set; }

        [DisplayName("خوێندکاری زانکۆلاین")]
        public int ZankoLine { get; set; }

        [DisplayName("خوێندکاری پاراڵێڵ")]
        public int Parallel { get; set; }

        [DisplayName("خوێندکاری ئێواران")]
        public int Evening { get; set; }

        [DisplayName("گەرمیان")]
        public int Garmian { get; set; }

        [DisplayName("سلێمانی")]
        public int Slemani { get; set; }

        [DisplayName("هەولێر")]
        public int Hawler { get; set; }

        [DisplayName("کەرکووک")]
        public int Karkuk { get; set; }

        [DisplayName("دیالە")]
        public int Diala { get; set; }
    }
}
