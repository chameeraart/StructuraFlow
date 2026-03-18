using ClosedXML.Excel;
using StructuraFlow.Models;

namespace StructuraFlow.Services
{
    public class ExcelReader : IExcelReader
    {
        public List<Column> ReadColumns(Stream stream)
        {
            var columns = new List<Column>();
            using var workbook = new XLWorkbook(stream);
            var ws = workbook.Worksheet(1);

            foreach (var row in ws.RowsUsed().Skip(1))
            {
                columns.Add(new Column
                {
                    Id = row.Cell(1).GetString(),
                    Height = row.Cell(2).GetDouble(),
                    Width = row.Cell(3).GetDouble()
                });
            }
            return columns;
        }

        public List<Beam> ReadBeams(Stream stream)
        {
            var beams = new List<Beam>();
            using var workbook = new XLWorkbook(stream);
            var ws = workbook.Worksheet(1);

            foreach (var row in ws.RowsUsed().Skip(1))
            {
                double length = 0;

                double.TryParse(row.Cell(2).GetValue<string>(), out length);

                beams.Add(new Beam
                {
                    Id = row.Cell(1).GetString(),
                    Length = length,
                    StartColumn = row.Cell(4).GetString(),
                    EndColumn = row.Cell(5).GetString()
                });
            }
            return beams;
        }

        public List<Slab> ReadSlabs(Stream stream)
        {
            var slabs = new List<Slab>();
            using var workbook = new XLWorkbook(stream);
            var ws = workbook.Worksheet(1);

            foreach (var row in ws.RowsUsed().Skip(1))
            {
                slabs.Add(new Slab
                {
                    Id = row.Cell(1).GetString(),
                    Thickness = row.Cell(2).GetDouble(),
                    Level = row.Cell(3).GetString()
                });
            }
            return slabs;
        }
    }
}
