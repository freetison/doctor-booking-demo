import { Component, inject, OnInit } from '@angular/core';
import { ScheduleService } from './services/schedule.service.ts';
import { CalendarData } from './models/CalendarData';
import { IAppointment } from './models/IAppointment';
import { catchError, of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Slot } from './models/Slot.js';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title = 'angular-client';
  schedule: CalendarData | undefined;
  isVisibleSchedule: boolean = false;
  selectedSlot: Slot | null = null;
  selectedDate: string | null = '';
  appointment: IAppointment | undefined;

  constructor(
    private toastr: ToastrService,
    private dataService: ScheduleService
  ) {}

  ngOnInit(): void {}

  onDateSelected(eventData: { date: string | null }) {
    this.selectedDate = eventData.date;
    this.dataService
      .getWeeklyAvailability(eventData.date)
      .pipe(
        catchError((error) => {
          this.toastr.error(error.error.errors[0].reason, 'Error');
          this.isVisibleSchedule = false;
          return of(null);
        })
      )
      .subscribe((res) => {
        if (res) {
          this.schedule = res;
          this.isVisibleSchedule = true;
        }
      });
  }

  onSlotSelected(event: Slot): void {
    this.selectedSlot = event;
    console.log('Selected time:', event);
    this.appointment = {
      FacilityId: this.schedule?.facility.facilityId,
      Start: new Date(event.start),
      End: new Date(event.end),
      Comments: 'Routine checkup',
      Patient: {
        Name: 'Pedro',
        SecondName: 'Perez',
        Email: 'pp@gmail.com',
        Phone: 12345687,
      },
    };

    // this.dataService.createAppointment(this.appointment).subscribe((res) => {
    //   console.log('Appointment result:', res);
    // });

    this.dataService
      .createAppointment(this.appointment)
      .pipe(
        catchError((error) => {
          this.toastr.error(error.error.errors[0].reason, 'Error');
          this.isVisibleSchedule = false;
          return of(null);
        })
      )
      .subscribe((res) => {
        if (res) {
          this.toastr.info(res, 'Info');
        }
      });
  }
}
