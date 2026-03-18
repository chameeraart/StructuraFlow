using StructuraFlow.Models;
using StructuraFlow.Services;
using Xunit;
using System.Collections.Generic;

namespace StructuraFlow.Tests
{
    public class ValidatorTests
    {
        private readonly Validator _validator;

        public ValidatorTests()
        {
            _validator = new Validator();
        }




        [Fact]
        public void Validate_ShouldReturnError_ForDuplicateColumns()
        {
            // Arrange
            var columns = new List<Column>
            {
                new Column { Id = "C101", Height = 300, Width = 500 },
                new Column { Id = "C101", Height = 300, Width = 500 } // duplicate
            };
           
            var beams = new List<Beam>();
            var slabs = new List<Slab>();

            // Act
            var errors = _validator.Validate(columns, beams, slabs, GetDefaultValidationConfig());

            // Assert
            Assert.Contains(errors, e => e.Contains("Duplicate Column ID"));
        }

        [Fact]
        public void Validate_ShouldReturnError_ForNegativeDimensions()
        {
            // Arrange
            var columns = new List<Column>
            {
                new Column { Id = "C102", Height = -300, Width = 500 }
            };
            var beams = new List<Beam>();
            var slabs = new List<Slab>();

            // Act
            var errors = _validator.Validate(columns, beams, slabs, GetDefaultValidationConfig());

            // Assert
            Assert.Contains(errors, e => e.Contains("Negative dimension"));
        }

        [Fact]
        public void Validate_ShouldReturnError_ForBeamStartEndSame()
        {
            // Arrange
            var columns = new List<Column>
            {
                new Column { Id = "C101", Height = 300, Width = 500 }
            };
            var beams = new List<Beam>
            {
                new Beam { Id = "B101", StartColumn = "C101", EndColumn = "C101", Length = 600 }
            };
            var slabs = new List<Slab>();

            // Act
            var errors = _validator.Validate(columns, beams, slabs, GetDefaultValidationConfig());

            // Assert
            Assert.Contains(errors, e => e.Contains("start and end column cannot be same"));
        }



        public ValidationRule GetDefaultValidationConfig()
        {
            var config = new ValidationRule
            {
                CheckDuplicateColumns = true,
                CheckDuplicateBeams = true,
                CheckDuplicateSlabs = true,
                CheckNegativeValues = true,
                CheckMissingFields = true,
                CheckBeamReferences = true,
                CheckBeamStartEndSame = true,

                // Optional numeric defaults
                MinColumnHeight = 250,
                MinColumnWidth = 250,
                MinBeamLength = 500,
                MinSlabThickness = 180
            };

            return config;
        }



    }
}