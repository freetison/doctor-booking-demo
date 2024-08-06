using System.Buffers;
using System.Text;
using System.Text.Json;
using Api.Endpoints.Availability.Models.Domain;
using Api.Endpoints.Availability.Utils;
using NUnit.Framework;
using Shouldly;

namespace Api.Test.Converters
{
    [TestFixture]
    public class FacilityScheduleJsonConverterTests
    {
        private FacilityScheduleJsonConverter _converter;
        private JsonSerializerOptions _options;
        private string _json;

        [SetUp]
        public void Setup()
        {
            _converter = new FacilityScheduleJsonConverter();
            _options = new JsonSerializerOptions { Converters = { _converter } };
            
            _json = @"{
                    ""Facility"": {
                        ""Address"": ""Plaza de la independencia 36, 38006 Santa Cruz de Tenerife"",
                        ""FacilityId"": ""ff506e69-3d8c-4be5-9ee9-8e712b12ef5b"",
                        ""Name"": ""Las Palmeras""
                    },
                    ""Friday"": {
                        ""BusySlots"": [
                            {
                                ""End"": ""2024-06-21T08:30:00"",
                                ""Start"": ""2024-06-21T08:20:00""
                            },
                            {
                                ""End"": ""2024-06-21T08:50:00"",
                                ""Start"": ""2024-06-21T08:40:00""
                            },
                            {
                                ""End"": ""2024-06-21T08:20:00"",
                                ""Start"": ""2024-06-21T08:10:00""
                            },
                            {
                                ""End"": ""2024-06-21T08:40:00"",
                                ""Start"": ""2024-06-21T08:30:00""
                            }
                        ],
                        ""WorkPeriod"": {
                            ""EndHour"": 16,
                            ""LunchEndHour"": 14,
                            ""LunchStartHour"": 13,
                            ""StartHour"": 8
                        }
                    },
                    ""SlotDurationMinutes"": 10
                }";

        }

        [Test]
        public void Read_ShouldThrowJsonException_WhenTokenIsNotStartObject()
        {

            Should.Throw<JsonException>(() =>
            {
                var json = "123";
                var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
                _converter.Read(ref reader, typeof(FacilitySchedule), _options);
            });
        }

       

        [Test]
        public void Write_ShouldSerializeFacilityProperty()
        {
            var schedule = new FacilitySchedule
            {
                Facility = new Facility { Name = "Test Facility" },
                SlotDurationMinutes = 30,
                Days = new Dictionary<string, DaySchedule>()
            };

            var buffer = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(buffer))
            {
                _converter.Write(writer, schedule, _options);
            }

            var json = Encoding.UTF8.GetString(buffer.WrittenSpan.ToArray());

            json.ShouldContain("Test Facility");
        }

        [Test]
        public void Write_ShouldSerializeSlotDurationMinutesProperty()
        {
            var schedule = new FacilitySchedule
            {
                Facility = new Facility { Name = "Test Facility" },
                SlotDurationMinutes = 30,
                Days = new Dictionary<string, DaySchedule>()
            };

            var buffer = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(buffer))
            {
                _converter.Write(writer, schedule, _options);
            }

            var json = Encoding.UTF8.GetString(buffer.WrittenSpan.ToArray());

            json.ShouldContain("\"slotDurationMinutes\":30");
        }

        [Test]
        public void Write_ShouldSerializeWeeklySchedule()
        {
            var schedule = new FacilitySchedule
            {
                Facility = new Facility { Name = "Test Facility" },
                SlotDurationMinutes = 30,
                Days = new Dictionary<string, DaySchedule>
                {
                    { "Monday", new DaySchedule { WorkPeriod = new WorkPeriod { StartHour = 8, EndHour = 16 }, BusySlots = new List<Slot>() } }
                }
            };

            var buffer = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(buffer))
            {
                _converter.Write(writer, schedule, _options);
            }

            var json = Encoding.UTF8.GetString(buffer.WrittenSpan.ToArray());

            json.ShouldContain("Monday");
        }
    }
}
