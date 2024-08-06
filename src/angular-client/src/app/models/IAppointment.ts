import { IPatient } from './IPatient';

export interface IAppointment {
  FacilityId: string | undefined;
  Start: Date;
  End: Date;
  Comments: string;
  Patient: IPatient;
}
