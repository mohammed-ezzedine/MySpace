export interface Page<T> {
  items: T[];
  pageNumber: number;
  totalNumberOfPages: number;
}
