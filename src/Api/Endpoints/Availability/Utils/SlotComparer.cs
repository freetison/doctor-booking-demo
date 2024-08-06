using System.Diagnostics.CodeAnalysis;
using Api.Endpoints.Availability.Models.Domain;

namespace Api.Endpoints.Availability.Utils;

[ExcludeFromCodeCoverage]
public partial class ScheduleHelper
{
    public class SlotComparer : IEqualityComparer<FreeSlot>
    {
        public bool Equals(FreeSlot? x, FreeSlot? y)
        {
            return x.Start == y.Start && x.End == y.End;
        }

        public int GetHashCode(FreeSlot obj)
        {
            return obj.Start.GetHashCode() ^ obj.End.GetHashCode();
        }
    }

}