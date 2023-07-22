import {FormControl} from "@angular/forms";

export interface PageContentSection {
  type: string;
  metadata?: string;
  content: FormControl;
}
