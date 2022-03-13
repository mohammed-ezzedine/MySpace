import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {ArticleService} from "../../services/article.service";
import {Article} from "../../models/article";

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private articleService: ArticleService) { }

  id: string | undefined;
  article: Article | undefined;
  errorMessage: string | null = null;

  ngOnInit(): void {
    this.getArticleIdFromRoute();
    this.getArticle();
  }

  private getArticle() {
    if (this.id) {
      this.articleService.getArticle(this.id).subscribe(article => {
        this.article = article;
      }, error => {
        this.errorMessage = error.message;
      })
    }
  }

  private getArticleIdFromRoute() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    })
  }

  getDateString(date: any) : string {
    return new Date(date).toDateString();
  }

  getSectionType(section: any) {
    return section.type;
  }

  getSectionCode(section: any) {
    return section.code
  }

  getSectionCodeLanguage(section: any) {
    return section.language
  }
}
