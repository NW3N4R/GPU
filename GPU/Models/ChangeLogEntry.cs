namespace GPU.Models
{
    public class ChangeLogEntry
    {
        public string TableName { get; set; }
        public string ChangeType { get; set; }  // "I" for insert, "U" for update, "D" for delete
        public int RecordId { get; set; }
    }
}
