﻿import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Article} from "../models/article";
import {Injectable} from "@angular/core";
import {FormGroup} from "@angular/forms";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root',
})
export class ArticleService {

  static ENDPOINT = environment.apiUrl + '/Article'
  static HTTP_OPTIONS = {
    headers: new HttpHeaders({
      "Content-Type": "application/json"
    })
  }

  constructor(private http: HttpClient) {
  }

  getArticles() : Observable<Article[]> {
    return this.http.get<Article[]>(ArticleService.ENDPOINT);
  }

  getArticle(id: string) : Observable<Article> {
    return this.http.get<Article>(ArticleService.ENDPOINT + "/" + id);
  }

  addArticle(form: any) : Observable<Article> {
    return this.http.post<Article>(ArticleService.ENDPOINT, JSON.stringify(form), ArticleService.HTTP_OPTIONS);
  }

  updateArticle(id: string, form: any) : Observable<Article> {
    return this.http.put<Article>(ArticleService.ENDPOINT + "/" + id, JSON.stringify(form), ArticleService.HTTP_OPTIONS);
  }

  deleteArticle(id: string) : Observable<Article> {
    return this.http.delete<Article>(ArticleService.ENDPOINT + "/" + id);
  }
}