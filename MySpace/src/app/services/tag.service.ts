import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: "root"
})
export class TagService {
  constructor(private http: HttpClient) {
  }

  static ENDPOINT = environment.apiUrl + '/tag'

  getRecommendedTags(): Observable<string[]> {
    return this.http.get<string[]>(TagService.ENDPOINT)
  }

  getAllTags() {
    return this.http.get<string[]>(TagService.ENDPOINT)
  }
}
