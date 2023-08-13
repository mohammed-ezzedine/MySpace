import { Component, OnInit } from '@angular/core';
import {Article} from "../../models/article";
import {ArticleService} from "../../services/article.service";
import {ActivatedRoute} from "@angular/router";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent implements OnInit {

  articles: Article[] | undefined;
  errorMessage: string = '';
  pageSize: number = 5;

  tag : string | undefined;
  q : string | undefined;
  currentPage: number = 1;
  totalNumberOfPages: number = 1;

  constructor(private route: ActivatedRoute,
              private articleService: ArticleService,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.checkRouteForTag();
  }

  isAuthenticated() {
    return this.authService.isAuthenticated()
  }

  private checkRouteForTag() {
    this.route.queryParams.subscribe({
      next: params => {
        this.tag = params['tag']
        this.q = params['q']
        this.getArticles();
      }
    })
  }

  private getArticles() {
    this.articleService.getArticles(this.currentPage, this.q, this.tag).subscribe({
      next: articles => {
        this.articles = articles.items;
        this.totalNumberOfPages = articles.totalNumberOfPages;
      },
      error: error => {
        this.errorMessage = error.message;
      }
    });
  }

  getTotalNumberOfPotentialArticles() {
    return this.pageSize * this.totalNumberOfPages;
  }

  jumpToPage(pageNumber: number) {
    this.currentPage = pageNumber;
    this.getArticles();
  }
}
