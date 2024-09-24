namespace GPU.Models
{
    public class AllViewModel
    {
        public PersonalStudent? PersonalStudent { get; set; }
        public StudentContactInfo? StudentContactInfo { get; set; }
        public StudentParentInfo? StudentParentInfo { get; set; }
        public Student12Grade? Student12Grade { get; set; }
        public StudentDepartmentInfo? StudentDepartmentInfo { get; set; }
        public IEnumerable<InvoiceInfo>? MyInvoice { get; set; }
        public StudentSupport? StudentSupport { get; set; }
        public InvoiceInfo? Invoice { get; set; }
    }
}
