import { Component, OnInit } from '@angular/core';
import {Article} from "../../models/article";
import {ArticleService} from "../../services/article.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent implements OnInit {

  articles: Article[] | undefined;
  errorMessage: string = '';

  tag : string | undefined;

  constructor(private route: ActivatedRoute,
              private articleService: ArticleService) { }

  ngOnInit(): void {
    this.checkRouteForTag();
  }

  private checkRouteForTag() {
    this.route.queryParams.subscribe({
      next: params => {
        this.tag = params['tag']
        console.log(this.tag)
        this.getArticles();
      }
    })
  }

  private getArticles() {
    this.articleService.getArticles(this.tag).subscribe({
      next: articles => {
        this.articles = articles;
      },
      error: error => {
        this.errorMessage = error.message;
      }
    });
  }

}
