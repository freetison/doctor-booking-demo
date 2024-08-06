import { Facility } from './Facility';
import { WeeklySchedule } from './WeeklySchedule';

export interface Schedule {
  facility: Facility;
  slotDurationMinutes: number;
  weeklySchedule: WeeklySchedule;
}
