import { BusySlot } from './BusySlot';
import { WorkPeriod } from './WorkPeriod';

export interface DaySchedule {
  busySlots: BusySlot[];
  workPeriod: WorkPeriod;
}
