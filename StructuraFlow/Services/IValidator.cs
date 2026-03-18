using StructuraFlow.Models;

namespace StructuraFlow.Services
{
    public interface IValidator
    {
        List<string> Validate(List<Column> columns, List<Beam> beams, List<Slab> slabs, ValidationRule config);
    }
}
