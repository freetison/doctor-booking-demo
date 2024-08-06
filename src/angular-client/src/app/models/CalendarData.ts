import { Facility } from './Facility';
import { FreeSlots } from './FreeSlots';

export interface CalendarData {
  facility: Facility;
  freeSlots: FreeSlots[];
}
