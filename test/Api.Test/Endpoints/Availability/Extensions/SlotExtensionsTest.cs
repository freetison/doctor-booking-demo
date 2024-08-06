using Api.Endpoints.Availability.Extensions;
using Api.Endpoints.Availability.Models.Domain;
using NUnit.Framework;
using Shouldly;

namespace Api.Test.Endpoints.Availability.Extensions;

[TestFixture]
public class SlotExtensionsTests
{
    private List<FreeSlot> _freeSlots;
    private List<BusySlot>? _busySlots;
    private List<(string Day, List<FreeSlot>)> _weekFreeSlots;
    private Facility _facility;

    [SetUp]
    public void SetUp()
    {
        _freeSlots = new List<FreeSlot>
        {
            new FreeSlot { Start = new DateTime(2024, 7, 31, 9, 0, 0), End = new DateTime(2024, 7, 31, 9, 10, 0) },
            new FreeSlot { Start = new DateTime(2024, 7, 31, 9, 10, 0), End = new DateTime(2024, 7, 31, 9, 20, 0) },
            new FreeSlot { Start = new DateTime(2024, 7, 31, 9, 30, 0), End = new DateTime(2024, 7, 31, 9, 40, 0) }
        };

        _busySlots = new List<BusySlot>
        {
            new BusySlot { Start = new DateTime(2024, 7, 31, 9, 30, 0), End = new DateTime(2024, 7, 31, 9, 40, 0) }
        };

        _weekFreeSlots = new List<(string Day, List<FreeSlot>)>
        {
            ("Wednesday", new List<FreeSlot>
            {
                new FreeSlot { Start = new DateTime(2024, 7, 31, 9, 0, 0), End = new DateTime(2024, 7, 31, 9, 10, 0) },
                new FreeSlot { Start = new DateTime(2024, 7, 31, 9, 10, 0), End = new DateTime(2024, 7, 31, 9, 20, 0) }
            })
        };

        _facility = new Facility
        {
            FacilityId = "6f108222-6574-4b5f-83a7-dab014ea953d",
            Name = "Las Palmeras",
            Address = "Plaza de la independencia 36, 38006 Santa Cruz de Tenerife"
        };
    }

    [Test]
    public void SubtractBusySlots_ShouldNotReturnNull()
    {
        // Act
        var availableSlots = _freeSlots.SubtractBusySlots(_busySlots);

        // Assert
        availableSlots.ShouldNotBeNull();
    }

    [Test]
    public void SubtractBusySlots_ShouldReturnCorrectCount()
    {
        // Act
        var availableSlots = _freeSlots.SubtractBusySlots(_busySlots);

        // Assert
        availableSlots.Count.ShouldBe(2);
    }

    [Test]
    public void SubtractBusySlots_ShouldContainFirstSlot()
    {
        // Act
        var availableSlots = _freeSlots.SubtractBusySlots(_busySlots);

        // Assert
        availableSlots.ShouldContain(slot => slot.Start == new DateTime(2024, 7, 31, 9, 0, 0) && slot.End == new DateTime(2024, 7, 31, 9, 10, 0));
    }

    [Test]
    public void SubtractBusySlots_ShouldContainSecondSlot()
    {
        // Act
        var availableSlots = _freeSlots.SubtractBusySlots(_busySlots);

        // Assert
        availableSlots.ShouldContain(slot => slot.Start == new DateTime(2024, 7, 31, 9, 10, 0) && slot.End == new DateTime(2024, 7, 31, 9, 20, 0));
    }

    [Test]
    public void ToFreeWeekSchedule_ShouldNotReturnNull()
    {
        // Act
        var freeWeekSchedule = _weekFreeSlots.ToFreeWeekSchedule(_facility);

        // Assert
        freeWeekSchedule.ShouldNotBeNull();
    }

    [Test]
    public void ToFreeWeekSchedule_ShouldContainCorrectFacility()
    {
        // Act
        var freeWeekSchedule = _weekFreeSlots.ToFreeWeekSchedule(_facility);

        // Assert
        freeWeekSchedule.Facility.ShouldBe(_facility);
    }

    [Test]
    public void ToFreeWeekSchedule_ShouldContainFreeSlots()
    {
        // Act
        var freeWeekSchedule = _weekFreeSlots.ToFreeWeekSchedule(_facility);

        // Assert
        freeWeekSchedule.FreeSlots.ShouldNotBeNull();
    }

    [Test]
    public void ToFreeWeekSchedule_ShouldContainCorrectDay()
    {
        // Act
        var freeWeekSchedule = _weekFreeSlots.ToFreeWeekSchedule(_facility);

        // Assert
        freeWeekSchedule.FreeSlots.Count.ShouldBe(1);
        freeWeekSchedule.FreeSlots.First().Day.ShouldBe("Wednesday");
    }

    [Test]
    public void ToFreeWeekSchedule_ShouldContainCorrectNumberOfFreeSlots()
    {
        // Act
        var freeWeekSchedule = _weekFreeSlots.ToFreeWeekSchedule(_facility);

        // Assert
        freeWeekSchedule.FreeSlots.First().FreeSlots.Count.ShouldBe(2);
    }
}