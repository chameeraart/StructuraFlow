using StructuraFlow.Models;

namespace StructuraFlow.Services
{
    public interface IExcelReader
    {
        List<Column> ReadColumns(Stream stream);
        List<Beam> ReadBeams(Stream stream);
        List<Slab> ReadSlabs(Stream stream);
    }
}
