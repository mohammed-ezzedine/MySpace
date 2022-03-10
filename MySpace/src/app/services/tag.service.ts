import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Tag} from "../models/tag";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class TagService {
  constructor(private http: HttpClient) {
  }

  getRecommendedTags(): Observable<Tag[]> {
    return this.http.get<Tag[]>("http://localhost:8000" + "/api/tag")
  }
}
