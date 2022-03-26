import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ArticleService} from "../../services/article.service";
import {Article} from "../../models/article";
import {DateUtils} from "../../utils/date.utils";

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class ArticleComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private router: Router,
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
      this.articleService.getArticle(this.id).subscribe({
        next: article => {
          this.article = article;
          console.log(this.article)
        },
        error: error => {
          this.errorMessage = error.message;
        }
      })
    }
  }

  private getArticleIdFromRoute() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    })
  }

  getDateString(date: any) : string {
    return DateUtils.getDateString(date);
  }

  getSectionCodeLanguage(section: any) {
    return section.language
  }

  deleteArticle() {
    this.articleService.deleteArticle(this.id!).subscribe({
      next: _ => this.router.navigateByUrl('/home'),
      error: err => this.errorMessage = err
    })
  }
}
