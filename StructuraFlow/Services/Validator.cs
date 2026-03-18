using StructuraFlow.Models;

namespace StructuraFlow.Services
{
    public class Validator : IValidator
    {
        public List<string> Validate(
    List<Column> columns,
    List<Beam> beams,
    List<Slab> slabs,
    ValidationRule config) // <-- Add config
        {
            var errors = new List<string>();

            // 1. DUPLICATE IDs
            if (config.CheckDuplicateColumns)
            {
                var duplicateColumns = columns.GroupBy(x => x.Id).Where(g => g.Count() > 1);
                foreach (var d in duplicateColumns)
                    errors.Add($"Duplicate Column ID: {d.Key}");
            }

            if (config.CheckDuplicateBeams)
            {
                var duplicateBeams = beams.GroupBy(x => x.Id).Where(g => g.Count() > 1);
                foreach (var d in duplicateBeams)
                    errors.Add($"Duplicate Beam ID: {d.Key}");
            }

            if (config.CheckDuplicateSlabs)
            {
                var duplicateSlabs = slabs.GroupBy(x => x.Id).Where(g => g.Count() > 1);
                foreach (var d in duplicateSlabs)
                    errors.Add($"Duplicate Slab ID: {d.Key}");
            }

            // 2. MISSING FIELDS
            if (config.CheckMissingFields)
            {
                foreach (var c in columns)
                {
                    if (string.IsNullOrWhiteSpace(c.Id))
                        errors.Add("Column ID is missing");

                    if (c.Height == 0)
                        errors.Add($"Column {c.Id} height is missing");

                    if (c.Width == 0)
                        errors.Add($"Column {c.Id} width is missing");
                }

                foreach (var b in beams)
                {
                    if (string.IsNullOrWhiteSpace(b.Id))
                        errors.Add("Beam ID is missing");

                    if (string.IsNullOrWhiteSpace(b.StartColumn))
                        errors.Add($"Beam {b.Id} start column is missing");

                    if (string.IsNullOrWhiteSpace(b.EndColumn))
                        errors.Add($"Beam {b.Id} end column is missing");

                    if (b.Length == 0)
                        errors.Add($"Beam {b.Id} length is missing");
                }

                foreach (var s in slabs)
                {
                    if (string.IsNullOrWhiteSpace(s.Id))
                        errors.Add("Slab ID is missing");

                    if (string.IsNullOrWhiteSpace(s.Level))
                        errors.Add($"Slab {s.Id} level is missing");

                    if (s.Thickness == 0)
                        errors.Add($"Slab {s.Id} thickness is missing");
                }
            }

            // 3. NEGATIVE VALUES
            if (config.CheckNegativeValues)
            {
                foreach (var c in columns)
                {
                    if (c.Height < 0 || c.Width < 0)
                        errors.Add($"Negative dimension in Column {c.Id}");
                }

                foreach (var b in beams)
                {
                    if (b.Length < 0)
                        errors.Add($"Negative length in Beam {b.Id}");
                }

                foreach (var s in slabs)
                {
                    if (s.Thickness < 0)
                        errors.Add($"Negative thickness in Slab {s.Id}");
                }
            }

            // 4. BEAM REFERENCES
            if (config.CheckBeamReferences)
            {
                foreach (var b in beams)
                {
                    if (!columns.Any(c => c.Id == b.StartColumn))
                        errors.Add($"Beam {b.Id} start column not found: {b.StartColumn}");

                    if (!columns.Any(c => c.Id == b.EndColumn))
                        errors.Add($"Beam {b.Id} end column not found: {b.EndColumn}");
                }
            }

            // 5. LOGICAL ERRORS
            if (config.CheckBeamStartEndSame)
            {
                foreach (var b in beams)
                {
                    if (b.StartColumn == b.EndColumn)
                        errors.Add($"Beam {b.Id} start and end column cannot be same");
                }
            }

            // 6. CUSTOM NUMERIC THRESHOLDS
            foreach (var c in columns)
            {
                if (c.Height < config.MinColumnHeight)
                    errors.Add($"Column {c.Id} height {c.Height} is below minimum {config.MinColumnHeight}");
                if (c.Width < config.MinColumnWidth)
                    errors.Add($"Column {c.Id} width {c.Width} is below minimum {config.MinColumnWidth}");
            }

            foreach (var b in beams)
            {
                if (b.Length < config.MinBeamLength)
                    errors.Add($"Beam {b.Id} length {b.Length} is below minimum {config.MinBeamLength}");
            }

            foreach (var s in slabs)
            {
                if (s.Thickness < config.MinSlabThickness)
                    errors.Add($"Slab {s.Id} thickness {s.Thickness} is below minimum {config.MinSlabThickness}");
            }

            return errors;
        }

    }
}
