export interface JobModel {
  id: string;
  employer: string;
  employerUrl: string;
  employerImageUrl: string;
  position: string;
  startDate: Date;
  endDate: Date | null;
  active: boolean;
}
