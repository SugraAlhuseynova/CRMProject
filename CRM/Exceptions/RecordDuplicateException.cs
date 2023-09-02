namespace CRM.Exceptions
{
    public class RecordDuplicateException : Exception
    {
        public RecordDuplicateException(string msg): base(msg) { }
    }
}
