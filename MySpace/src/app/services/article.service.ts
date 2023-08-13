import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Article} from "../models/article";
import {Injectable} from "@angular/core";
import {environment} from "../../environments/environment";
import {AuthService} from "./auth.service";
import {SeoShareDataModel} from "../models/seo-share-data.model";
import {Page} from "./page";


@Injectable({
  providedIn: 'root',
})
export class ArticleService {

  private static readonly ENDPOINT = environment.apiUrl + '/Article'

  constructor(private http: HttpClient,
              private authService: AuthService) {
  }

  getArticles(pageIndex: number, q?: string, tag? : string) : Observable<Page<Article>> {

    let query = "?p=" + pageIndex;
    query += q != undefined
      ? "&q=" + q
      : tag != undefined
        ? "&tag=" + tag
        : ''
    return this.http.get<Page<Article>>(ArticleService.ENDPOINT + query);
  }

  getArticle(id: string) : Observable<Article> {
    return this.http.get<Article>(ArticleService.ENDPOINT + "/" + id);
  }

  getArticleMeta(id: string) : Observable<SeoShareDataModel> {
    return this.http.get<SeoShareDataModel>(ArticleService.ENDPOINT + "/meta/" + id);
  }

  addArticle(form: any) : Observable<Article> {
    console.log(JSON.stringify(form))
       return this.http.post<Article>(ArticleService.ENDPOINT, JSON.stringify(form), this.getHttpOptions());
  }

  updateArticle(id: string, form: any) : Observable<Article> {
    return this.http.put<Article>(ArticleService.ENDPOINT + "/" + id, JSON.stringify(form), this.getHttpOptions());
  }

  deleteArticle(id: string) : Observable<Article> {
    return this.http.delete<Article>(ArticleService.ENDPOINT + "/" + id, this.getHttpOptions());
  }

  private getHttpOptions() {
    let token = this.authService.getToken();
    if (token == null) {
      throw new Error("Unauthorized.")
    }

    return {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization": "Bearer " + token
      })
    }
  }
}
