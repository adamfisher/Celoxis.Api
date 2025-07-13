using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Categories;

namespace Celoxis.Api.Tests
{
    public class QueryBuilderTests
    {
        #region Where Tests

        [Fact]
        [UnitTest]
        public void Where_SingleCondition_AddsToFilter()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Where("name", "Test CeloxisProject")
                .Build();

            // Assert
            result.Filter.Should().NotBeNull();
            result.Filter.Should().ContainKey("name");
            result.Filter!["name"].Should().Be("Test CeloxisProject");
        }

        [Fact]
        [UnitTest]
        public void Where_MultipleConditions_AddsAllToFilter()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Where("state", "Active")
                .Where("budget", ">50000")
                .Where("priority", "High")
                .Build();

            // Assert
            result.Filter.Should().NotBeNull();
            result.Filter.Should().HaveCount(3);
            result.Filter!["state"].Should().Be("Active");
            result.Filter!["budget"].Should().Be(">50000");
            result.Filter!["priority"].Should().Be("High");
        }

        [Fact]
        [UnitTest]
        public void Where_OverwriteExistingKey_UsesLatestValue()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Where("state", "Active")
                .Where("state", "Completed")
                .Build();

            // Assert
            result.Filter.Should().NotBeNull();
            result.Filter!["state"].Should().Be("Completed");
        }

        [Fact]
        [UnitTest]
        public void Where_NestedProperty_AddsWithDotNotation()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Where("project.state", "Active")
                .Where("project.manager.name", "John Doe")
                .Build();

            // Assert
            result.Filter.Should().NotBeNull();
            result.Filter!["project.state"].Should().Be("Active");
            result.Filter!["project.manager.name"].Should().Be("John Doe");
        }

        #endregion

        #region WhereStartsWith Tests

        [Fact]
        [UnitTest]
        public void WhereStartsWith_ValidValue_AddsWithPrefix()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .WhereStartsWith("name", "Test")
                .Build();

            // Assert
            result.Filter.Should().NotBeNull();
            result.Filter!["name"].Should().Be("^Test");
        }

        [Fact]
        [UnitTest]
        public void WhereStartsWith_MultipleFields_AddsAllWithPrefix()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .WhereStartsWith("name", "API")
                .WhereStartsWith("code", "PROJ")
                .Build();

            // Assert
            result.Filter.Should().NotBeNull();
            result.Filter!["name"].Should().Be("^API");
            result.Filter!["code"].Should().Be("^PROJ");
        }

        #endregion

        #region WhereIn Tests

        [Fact]
        [UnitTest]
        public void WhereIn_MultipleValues_AddsArrayFilter()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .WhereIn("state", "Active", "Draft", "OnHold")
                .Build();

            // Assert
            result.Filter.Should().NotBeNull();
            result.Filter!["state"].Should().BeOfType<string[]>();
            
            var values = result.Filter!["state"] as string[];
            values.Should().NotBeNull();
            values.Should().HaveCount(3);
            values.Should().Contain(new[] { "Active", "Draft", "OnHold" });
        }

        [Fact]
        [UnitTest]
        public void WhereIn_SingleValue_StillCreatesArray()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .WhereIn("type", "Infrastructure")
                .Build();

            // Assert
            result.Filter.Should().NotBeNull();
            var values = result.Filter!["type"] as string[];
            values.Should().NotBeNull();
            values.Should().HaveCount(1);
            values![0].Should().Be("Infrastructure");
        }

        [Fact]
        [UnitTest]
        public void WhereIn_EmptyValues_CreatesEmptyArray()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .WhereIn("state")
                .Build();

            // Assert
            result.Filter.Should().NotBeNull();
            var values = result.Filter!["state"] as string[];
            values.Should().NotBeNull();
            values.Should().BeEmpty();
        }

        #endregion

        #region OrderBy Tests

        [Fact]
        [UnitTest]
        public void OrderBy_SingleField_AddsSortField()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .OrderBy("plannedStart")
                .Build();

            // Assert
            result.Sort.Should().Be("plannedStart");
        }

        [Fact]
        [UnitTest]
        public void OrderBy_SingleFieldDescending_AddsSortFieldWithDesc()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .OrderBy("budget", descending: true)
                .Build();

            // Assert
            result.Sort.Should().Be("budget/desc");
        }

        [Fact]
        [UnitTest]
        public void OrderBy_MultipleFields_CombinesWithComma()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .OrderBy("plannedStart")
                .OrderBy("priority", descending: true)
                .OrderBy("name")
                .Build();

            // Assert
            result.Sort.Should().Be("plannedStart,priority/desc,name");
        }

        [Fact]
        [UnitTest]
        public void OrderBy_NestedField_UsesDoNotation()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .OrderBy("project.name")
                .OrderBy("project.plannedStart", descending: true)
                .Build();

            // Assert
            result.Sort.Should().Be("project.name,project.plannedStart/desc");
        }

        #endregion

        #region Expand Tests

        [Fact]
        [UnitTest]
        public void Expand_SingleAssociation_AddsExpansion()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Expand("manager")
                .Build();

            // Assert
            result.Expand.Should().NotBeNull();
            result.Expand.Should().HaveCount(1);
            result.Expand.Should().Contain("manager");
        }

        [Fact]
        [UnitTest]
        public void Expand_MultipleAssociations_AddsAll()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Expand("manager", "clients", "tasks")
                .Build();

            // Assert
            result.Expand.Should().NotBeNull();
            result.Expand.Should().HaveCount(3);
            result.Expand.Should().Contain(new[] { "manager", "clients", "tasks" });
        }

        [Fact]
        [UnitTest]
        public void Expand_CalledMultipleTimes_AccumulatesAssociations()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Expand("manager")
                .Expand("clients", "tasks")
                .Expand("assignments")
                .Build();

            // Assert
            result.Expand.Should().NotBeNull();
            result.Expand.Should().HaveCount(4);
            result.Expand.Should().Contain(new[] { "manager", "clients", "tasks", "assignments" });
        }

        [Fact]
        [UnitTest]
        public void Expand_EmptyCall_CreatesEmptyList()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Expand()
                .Build();

            // Assert
            result.Expand.Should().BeNullOrEmpty();
        }

        #endregion

        #region Page Tests

        [Fact]
        [UnitTest]
        public void Page_ValidPageNumber_SetsPage()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Page(3)
                .Build();

            // Assert
            result.Page.Should().Be(3);
        }

        [Fact]
        [UnitTest]
        public void Page_CalledMultipleTimes_UsesLatestValue()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Page(1)
                .Page(5)
                .Page(2)
                .Build();

            // Assert
            result.Page.Should().Be(2);
        }

        #endregion

        #region Complex Query Tests

        [Fact]
        [UnitTest]
        public void Build_ComplexQuery_GeneratesCorrectParameters()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Where("state", States.Project.Active)
                .Where("budget", ">100000")
                .WhereIn("type", "Infrastructure", "Implementation")
                .WhereStartsWith("name", "API")
                .Where("manager.name", "John Doe")
                .OrderBy("plannedStart", descending: true)
                .OrderBy("budget")
                .Expand("manager", "clients", "tasks")
                .Page(3)
                .Build();

            // Assert
            // Filter
            result.Filter.Should().NotBeNull();
            result.Filter.Should().HaveCount(5);
            result.Filter!["state"].Should().Be(States.Project.Active);
            result.Filter!["budget"].Should().Be(">100000");
            result.Filter!["name"].Should().Be("^API");
            result.Filter!["manager.name"].Should().Be("John Doe");
            
            var types = result.Filter!["type"] as string[];
            types.Should().NotBeNull();
            types.Should().HaveCount(2);
            types.Should().Contain(new[] { "Infrastructure", "Implementation" });
            
            // Sort
            result.Sort.Should().Be("plannedStart/desc,budget");
            
            // Expand
            result.Expand.Should().NotBeNull();
            result.Expand.Should().HaveCount(3);
            result.Expand.Should().Contain(new[] { "manager", "clients", "tasks" });
            
            // Page
            result.Page.Should().Be(3);
        }

        [Fact]
        [UnitTest]
        public void Build_EmptyQuery_ReturnsEmptyParameters()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder.Build();

            // Assert
            result.Filter.Should().BeNull();
            result.Sort.Should().BeNull();
            result.Expand.Should().BeNull();
            result.Page.Should().BeNull();
        }

        [Fact]
        [UnitTest]
        public void Build_OnlyFilter_OtherParametersAreNull()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var result = builder
                .Where("state", "Active")
                .Build();

            // Assert
            result.Filter.Should().NotBeNull();
            result.Sort.Should().BeNull();
            result.Expand.Should().BeNull();
            result.Page.Should().BeNull();
        }

        #endregion

        #region Fluent Interface Tests

        [Fact]
        [UnitTest]
        public void AllMethods_ReturnQueryBuilder_ForChaining()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act & Assert
            builder.Where("field", "value").Should().BeSameAs(builder);
            builder.WhereStartsWith("field", "value").Should().BeSameAs(builder);
            builder.WhereIn("field", "value1", "value2").Should().BeSameAs(builder);
            builder.OrderBy("field").Should().BeSameAs(builder);
            builder.Expand("association").Should().BeSameAs(builder);
            builder.Page(1).Should().BeSameAs(builder);
        }

        #endregion
    }

    public class DateFiltersTests
    {
        [Fact]
        [UnitTest]
        public void Range_ValidDates_CreatesCorrectDateRange()
        {
            // Arrange
            var from = new DateTime(2025, 1, 1);
            var to = new DateTime(2025, 3, 31);

            // Act
            var result = DateFilters.Range(from, to);

            // Assert
            result.Should().Be("2025-01-01 to 2025-03-31");
        }

        [Fact]
        [UnitTest]
        public void Range_SameDate_CreatesValidRange()
        {
            // Arrange
            var date = new DateTime(2025, 1, 15);

            // Act
            var result = DateFilters.Range(date, date);

            // Assert
            result.Should().Be("2025-01-15 to 2025-01-15");
        }

        [Fact]
        [UnitTest]
        public void LastNDays_PositiveNumber_CreatesCorrectFilter()
        {
            // Act & Assert
            DateFilters.LastNDays(7).Should().Be("-7d to Today");
            DateFilters.LastNDays(30).Should().Be("-30d to Today");
            DateFilters.LastNDays(1).Should().Be("-1d to Today");
        }

        [Fact]
        [UnitTest]
        public void NextNDays_PositiveNumber_CreatesCorrectFilter()
        {
            // Act & Assert
            DateFilters.NextNDays(7).Should().Be("Today to +7d");
            DateFilters.NextNDays(30).Should().Be("Today to +30d");
            DateFilters.NextNDays(1).Should().Be("Today to +1d");
        }

        [Fact]
        [UnitTest]
        public void Constants_HaveCorrectValues()
        {
            // Assert
            DateFilters.Today.Should().Be("Today");
            DateFilters.Yesterday.Should().Be("Yesterday");
            DateFilters.Tomorrow.Should().Be("Tomorrow");
            DateFilters.ThisWeek.Should().Be("This Week");
            DateFilters.LastWeek.Should().Be("Last Week");
            DateFilters.NextWeek.Should().Be("Next Week");
            DateFilters.ThisMonth.Should().Be("This Month");
            DateFilters.LastMonth.Should().Be("Last Month");
            DateFilters.NextMonth.Should().Be("Next Month");
            DateFilters.ThisQuarter.Should().Be("This Quarter");
            DateFilters.LastQuarter.Should().Be("Last Quarter");
            DateFilters.NextQuarter.Should().Be("Next Quarter");
            DateFilters.ThisYear.Should().Be("This Year");
            DateFilters.LastYear.Should().Be("Last Year");
            DateFilters.NextYear.Should().Be("Next Year");
        }
    }
}
