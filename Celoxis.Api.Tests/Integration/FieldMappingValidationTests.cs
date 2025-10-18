using System;
using Celoxis.Api.Models;
using Celoxis.Api.Serialization.Converters;
using FluentAssertions;
using Xunit;
using Xunit.Categories;

namespace Celoxis.Api.Tests.Integration
{
    [IntegrationTest]
    public class FieldMappingValidationTests
    {
        [Fact]
        public void CeloxisProject_ShouldHaveCorrectFieldTypes()
        {
            typeof(CeloxisProject).GetProperty("Budget")?.PropertyType.Should().Be<decimal?>();
            typeof(CeloxisProject).GetProperty("ActualFinish")?.PropertyType.Should().Be<DateTimeOffset?>();
            typeof(CeloxisProject).GetProperty("ActualPercentComplete")?.PropertyType.Should().Be<int?>();
            typeof(CeloxisProject).GetProperty("Alignment")?.PropertyType.Should().Be<int?>();
            typeof(CeloxisProject).GetProperty("Benefit")?.PropertyType.Should().Be<int?>();
        }

        [Fact]
        public void CeloxisTask_ShouldHaveCorrectBooleanFields()
        {
            typeof(CeloxisTask).GetProperty("Milestone")?.PropertyType.Should().Be<bool?>();
            typeof(CeloxisTask).GetProperty("IsManuallyScheduled")?.PropertyType.Should().Be<bool?>();
            typeof(CeloxisTask).GetProperty("IsTimeAllowed")?.PropertyType.Should().Be<bool?>();
            typeof(CeloxisTask).GetProperty("Critical")?.PropertyType.Should().Be<bool?>();
        }

        [Fact]
        public void CeloxisTimeEntry_ShouldHaveCorrectAssociationTypes()
        {
            typeof(CeloxisTimeEntry).GetProperty("User")?.PropertyType.Should().Be<Association<CeloxisUser>>();
            typeof(CeloxisTimeEntry).GetProperty("Task")?.PropertyType.Should().Be<Association<CeloxisTask>>();
            typeof(CeloxisTimeEntry).GetProperty("Project")?.PropertyType.Should().Be<Association<CeloxisProject>>();
        }

        [Fact]
        public void CustomFieldHandling_ShouldWorkCorrectly()
        {
            var model = new CeloxisProject();

            model.SetCustomField("country", "USA");
            model.SetMultiSelectCustomField("regions", "North", "South");

            model.GetCustomField<string>("country").Should().Be("USA");
            model.GetMultiSelectCustomField("regions").Should().BeEquivalentTo(new[] { "North", "South" });
            model.OtherFields.Should().ContainKey("custom_country");
            model.OtherFields.Should().ContainKey("custom_regions");
        }

        [Fact]
        public void CreateProjectRequest_Validation_ShouldWork()
        {
            var request = new CreateProjectRequest();
            var result = request.Validate();

            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(6);
            result.Errors.Should().Contain(e => e.Property == "Name");
            result.Errors.Should().Contain(e => e.Property == "Manager");
        }

        [Fact]
        public void CreateProjectRequest_ValidData_ShouldPassValidation()
        {
            var request = new CreateProjectRequest
            {
                Name = "Test Project",
                Manager = "test.user",
                PlannedStart = DateTimeOffset.Now,
                State = States.Project.Active,
                Type = "Infrastructure",
                Workspace = "Default"
            };

            var result = request.Validate();
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Constants_ShouldHaveCorrectValues()
        {
            States.Project.Active.Should().Be("Active");
            States.Project.Draft.Should().Be("Draft");
            States.TimeEntry.Saved.Should().Be("Saved");
            
            Priorities.Normal.Should().Be("NORMAL");
            Priorities.High.Should().Be("HIGH");
            
            BillingTypes.TimeAndMaterial.Should().Be("TNM");
            BillingTypes.FixedPrice.Should().Be("FIXED_PRICE");
            
            DateFilters.Last7Days.Should().Be("-7d to Today");
            DateFilters.Next30Days.Should().Be("Today to +30d");
        }

        [Fact]
        public void DateFilters_ShouldGenerateCorrectStrings()
        {
            DateFilters.LastNDays(14).Should().Be("-14d to Today");
            DateFilters.NextNDays(5).Should().Be("Today to +5d");
            DateFilters.LastNWeeks(2).Should().Be("-2w to Today");
            DateFilters.NextNMonths(3).Should().Be("Today to +3m");
        }
    }
}
