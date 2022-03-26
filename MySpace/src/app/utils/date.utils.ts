export class DateUtils {
  static getDateString(date: any) : string {
    return new Date(date).toDateString();
  }
}
