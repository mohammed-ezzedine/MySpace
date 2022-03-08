import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Article} from "../models/article";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  constructor(private http: HttpClient) {
  }

  getArticles() : Observable<Article[]> {
    return this.http.get<Article[]>("http://localhost:8000" + "/api/Article");
  }
}
