using Api.Endpoints.Availability.Models.Domain;
using Api.Endpoints.Availability.Utils;
using NUnit.Framework;
using Shouldly;

namespace Api.Test.Endpoints.Availability.Utils;
 
[TestFixture]
public class ScheduleHelperTests
{
    private ScheduleHelper _scheduleHelper;
    private WorkPeriod? _workPeriod;
    private string _inputDate;
    private int _slotDurationMinutes;
    private List<BusySlot>? _busySlots;

    [SetUp]
    public void Setup()
    {
        _workPeriod = new WorkPeriod
        {
            StartHour = 9,
            EndHour = 17,
            LunchStartHour = 12,
            LunchEndHour = 13
        };
        _inputDate = "20230101";
        _slotDurationMinutes = 30;
        _scheduleHelper = new ScheduleHelper(_workPeriod, _inputDate, _slotDurationMinutes);
        _busySlots = new List<BusySlot>
        {
            new BusySlot { Start = DateTime.Parse("2023-01-01T09:30:00"), End = DateTime.Parse("2023-01-01T10:00:00") },
            new BusySlot { Start = DateTime.Parse("2023-01-01T14:00:00"), End = DateTime.Parse("2023-01-01T14:30:00") }
        };
    }

    [Test]
    public void GetFreeSlots_ShouldReturnCorrectFreeSlots()
    {
        // Act
        var freeSlots = _scheduleHelper.GetFreeSlots(_busySlots);

        // Assert
        freeSlots.Count.ShouldBe(12); 
        freeSlots.ShouldContain(slot => slot.Start == DateTime.Parse("2023-01-01T09:00:00") && slot.End == DateTime.Parse("2023-01-01T09:30:00"));
        freeSlots.ShouldNotContain(slot => slot.Start == DateTime.Parse("2023-01-01T09:30:00") && slot.End == DateTime.Parse("2023-01-01T10:00:00"));
        freeSlots.ShouldContain(slot => slot.Start == DateTime.Parse("2023-01-01T10:00:00") && slot.End == DateTime.Parse("2023-01-01T10:30:00"));
        freeSlots.ShouldContain(slot => slot.Start == DateTime.Parse("2023-01-01T13:30:00") && slot.End == DateTime.Parse("2023-01-01T14:00:00"));
        freeSlots.ShouldNotContain(slot => slot.Start == DateTime.Parse("2023-01-01T14:00:00") && slot.End == DateTime.Parse("2023-01-01T14:30:00"));
    }


    [Test]
    public void GetFreeSlots_ShouldHandleEdgeCases()
    {
        // Busy slot exactly at the start of work period
        _busySlots.Add(new BusySlot { Start = DateTime.Parse("2023-01-01T09:00:00"), End = DateTime.Parse("2023-01-01T09:30:00") });
        var freeSlots = _scheduleHelper.GetFreeSlots(_busySlots);
        freeSlots.ShouldNotContain(slot => slot.Start == DateTime.Parse("2023-01-01T09:00:00") && slot.End == DateTime.Parse("2023-01-01T09:30:00"));

        // Busy slot exactly at the end of work period
        _busySlots.Add(new BusySlot { Start = DateTime.Parse("2023-01-01T16:30:00"), End = DateTime.Parse("2023-01-01T17:00:00") });
        freeSlots = _scheduleHelper.GetFreeSlots(_busySlots);
        freeSlots.ShouldNotContain(slot => slot.Start == DateTime.Parse("2023-01-01T16:30:00") && slot.End == DateTime.Parse("2023-01-01T17:00:00"));
    }

    [TearDown]
    public void Teardown()
    {
        _scheduleHelper = null;
        _workPeriod = null;
        _inputDate = null;
        _slotDurationMinutes = 0;
        _busySlots = null;
    }
}
