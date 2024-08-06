import { DaySchedule } from './DaySchedule';

export interface WeeklySchedule {
  Friday: DaySchedule;
  Monday: DaySchedule;
  Saturday: DaySchedule;
  Sunday: DaySchedule;
  Thursday: DaySchedule;
  Tuesday: DaySchedule;
  Wednesday: DaySchedule;
}
