import { Component, OnInit } from '@angular/core';
import {Article} from "../../models/article";
import {ArticleService} from "../../services/article.service";

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent implements OnInit {

  articles: Article[] | undefined;
  errorMessage: string = '';

  constructor(private articleService: ArticleService) { }

  ngOnInit(): void {
    this.getArticles();
  }

  private getArticles() {
    this.articleService.getArticles().subscribe(articles => {
      this.articles = articles;
    }, error => {
      this.errorMessage = error.message();
    })
  }

}
