namespace WildArmsModel.EventParsing.Templates
{
    public class ActionTemplate
    {
        public byte Id { get; set; }
        public string Action { get; set; }
        public int ArgumentByteSize { get; set; }
        public bool StringStart { get; set; }
        public bool NullTerminated { get; set; }

        public ActionTemplate(byte id, string action, int argumentByteSize)
        {
            Id = id;
            Action = action;
            ArgumentByteSize = argumentByteSize;
        }
    }
}
