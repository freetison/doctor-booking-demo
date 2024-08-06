import { AfterViewInit, Component, EventEmitter, Output } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { DatePipe } from '@angular/common';
import { addDays, addYears, startOfWeek } from 'date-fns';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrl: './date-picker.component.scss',
  providers: [DatePipe],
})
export class DatePickerComponent {
  @Output() selectedDate = new EventEmitter<{ date: string | null }>();

  bsConfig: Partial<BsDatepickerConfig>;
  datePicked: Date | undefined;

  minDate: Date = new Date();
  maxDate: Date = new Date();
  availableDates: Date[] = [];
  selectedDate1?: Date;
  selectedDate2?: Date;
  isSecondDatePickerVisible: boolean = false;

  constructor(private datePipe: DatePipe) {
    this.maxDate.setFullYear(this.minDate.getFullYear() + 1);
    this.bsConfig = Object.assign({}, { containerClass: 'theme-default' });
  }

  onDateChange(date: Date): void {
    if (date) {
      const formattedDate = this.datePipe.transform(date, 'yyyyMMdd');
      console.log('Selected Date (YYYYMMdd):', formattedDate);
      this.isSecondDatePickerVisible = true;
      this.selectedDate.emit({ date: formattedDate });
    }
  }
}
