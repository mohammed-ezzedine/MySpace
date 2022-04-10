export class DateService {
  public static getDate(timestamp: number) : Date {
    return new Date(this.convertToSeconds(timestamp))
  }

  private static convertToSeconds(valueInMilliseconds: number) : number {
    return valueInMilliseconds * 1000;
  }
}
