using WildArmsModel.Model.Areas;

namespace WildArmsModel.EventParsing
{
    public interface IEventParser
    {
        void PrintEvents(AreaObject area, string outputFile);
    }
}
