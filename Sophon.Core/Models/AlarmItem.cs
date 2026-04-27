namespace Sophon.Core
{
    public class AlarmItem
    {
        public string AlarmCode { get; set; }
        public string Content { get; set; }
        public AlarmLevel Level { get; set; }
    }

    public enum AlarmLevel
    {
        Info,
        Warning,
        Error,
        Critical
    }
}