// schedule-calendar.component.ts
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BusySlot } from '../models/BusySlot';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { CalendarData } from '../models/CalendarData';
import { Slot } from '../models/Slot';

@Component({
  selector: 'app-schedule-calendar',
  templateUrl: './schedule-calendar.component.html',
  styleUrl: './schedule-calendar.component.scss',
})
export class ScheduleCalendarComponent implements OnInit {
  @Input() schedule: CalendarData | undefined;
  @Output() selectedTime = new EventEmitter<{ time: string | null }>();
  @Output() selectedSlot = new EventEmitter<Slot>();

  calendarData: any;
  selectedDate: Slot | undefined;

  bsConfig: Partial<BsDatepickerConfig>;
  availableDates: Date[] = [];
  selectedDate2?: Date;

  isSecondDatePickerVisible = true;
  busySlots: BusySlot[] = [];

  constructor() {
    this.bsConfig = Object.assign({}, { containerClass: 'theme-default' });
  }

  ngOnInit(): void {}

  onDateSelect(date: string, slot: Slot): void {
    this.selectedDate = slot;
    console.log(`Selected date: ${date}, Slot: ${slot.start} - ${slot.end}`);
    this.selectedSlot.emit(slot);
  }
}
