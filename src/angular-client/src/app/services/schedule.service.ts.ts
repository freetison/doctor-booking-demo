import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAppointment } from '../models/IAppointment';
import { CalendarData } from '../models/CalendarData';

@Injectable({
  providedIn: 'root',
})
export class ScheduleService {
  private apiUrl = 'http://localhost:5000/api';

  constructor(private http: HttpClient) {}

  getWeeklyAvailability(date: string | null): Observable<CalendarData> {
    return this.http.get<CalendarData>(
      `${this.apiUrl}/weekly-availability/${date}`
    );
  }

  createAppointment(appointment: IAppointment): Observable<string> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<string>(
      `${this.apiUrl}/appointment/create`,
      appointment,
      { headers }
    );
  }
}
