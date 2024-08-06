using Api.Endpoints.Availability.Extensions;
using Api.Endpoints.Availability.Models.Domain;

using Moq;

using Newtonsoft.Json.Linq;

using NUnit.Framework;

using Shouldly;

namespace Api.Test.Endpoints.Availability.Extensions;

[TestFixture]
public class ConverterExtensionsTests
{
    private Mock<Facility> _mockFacility;
    private WeeklySchedule _weeklySchedule;
    private DaySchedule _dayScheduleWithWorkPeriod;
    private DaySchedule _dayScheduleWithoutWorkPeriod;

    [SetUp]
    public void SetUp()
    {
        _mockFacility = new Mock<Facility>();
        _dayScheduleWithWorkPeriod = new DaySchedule { WorkPeriod = new WorkPeriod { StartHour = 9, EndHour = 17, LunchStartHour = 12, LunchEndHour = 13 } };
        _dayScheduleWithoutWorkPeriod = new DaySchedule();

        _weeklySchedule = new WeeklySchedule
        {
            Facility = _mockFacility.Object,
            SlotDurationMinutes = 30
        };

        _weeklySchedule.Days.Add("Monday", JObject.FromObject(_dayScheduleWithWorkPeriod));
        _weeklySchedule.Days.Add("Tuesday", JObject.FromObject(_dayScheduleWithoutWorkPeriod));
    }

    [Test]
    public void ToWeekDaySchedule_ShouldNotBeNull()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        weekDaySchedule.ShouldNotBeNull();
    }

    [Test]
    public void ToWeekDaySchedule_ShouldHaveCorrectFacility()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        weekDaySchedule.Facility.ShouldBe(_weeklySchedule.Facility);
    }

    [Test]
    public void ToWeekDaySchedule_ShouldHaveCorrectSlotDurationMinutes()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        weekDaySchedule.SlotDurationMinutes.ShouldBe(_weeklySchedule.SlotDurationMinutes);
    }

    [Test]
    public void ToWeekDaySchedule_ShouldContainMonday()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        weekDaySchedule.Days.ShouldContainKey("Monday");
    }

    [Test]
    public void ToWeekDaySchedule_ShouldHaveDayScheduleForMonday()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        weekDaySchedule.Days["Monday"].ShouldBeOfType<DaySchedule>();
    }

    [Test]
    public void ToWeekDaySchedule_ShouldHaveWorkPeriodForMonday()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        weekDaySchedule.Days["Monday"]?.WorkPeriod.ShouldNotBeNull();
    }

    [Test]
    public void ToWeekDaySchedule_ShouldContainTuesday()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        weekDaySchedule.Days.ShouldContainKey("Tuesday");
    }

    [Test]
    public void ToWeekDaySchedule_ShouldHaveDayScheduleForTuesday()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        weekDaySchedule.Days["Tuesday"].ShouldBeOfType<DaySchedule>();
    }

    [Test]
    public void ToWeekDaySchedule_ShouldNotHaveWorkPeriodForTuesday()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        weekDaySchedule.Days["Tuesday"]?.WorkPeriod.ShouldBeNull();
    }

    [Test]
    public void FilterDays_ShouldNotBeNull()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        var filteredSchedule = weekDaySchedule.FilterDays();
        filteredSchedule.ShouldNotBeNull();
    }

    [Test]
    public void FilterDays_ShouldHaveCorrectFacility()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        var filteredSchedule = weekDaySchedule.FilterDays();
        filteredSchedule?.Facility.ShouldBe(weekDaySchedule.Facility);
    }

    [Test]
    public void FilterDays_ShouldHaveCorrectSlotDurationMinutes()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        var filteredSchedule = weekDaySchedule.FilterDays();
        filteredSchedule?.SlotDurationMinutes.ShouldBe(weekDaySchedule.SlotDurationMinutes);
    }

    [Test]
    public void FilterDays_ShouldContainMonday()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        var filteredSchedule = weekDaySchedule.FilterDays();
        filteredSchedule?.Days.ShouldContainKey("Monday");
    }

    [Test]
    public void FilterDays_ShouldNotContainTuesday()
    {
        var weekDaySchedule = _weeklySchedule.ToWeekDaySchedule();
        var filteredSchedule = weekDaySchedule.FilterDays();
        filteredSchedule?.Days.ShouldNotContainKey("Tuesday");
    }

   
}