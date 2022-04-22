import {Component, Inject, OnInit, PLATFORM_ID, ViewEncapsulation} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ArticleService} from "../../services/article.service";
import {Article} from "../../models/article";
import {DateUtils} from "../../utils/date.utils";
import {AuthService} from "../../services/auth.service";
import {SeoService} from "../../services/seo.service";
import {SeoShareDataModel} from "../../models/seo-share-data.model";

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class ArticleComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private router: Router,
              private seoService: SeoService,
              private articleService: ArticleService,
              private authService: AuthService,
              @Inject(PLATFORM_ID) private platformId: Object) { }

  id: string | undefined;
  article: Article | undefined;
  errorMessage: string | null = null;

  ngOnInit(): void {
    this.getArticleIdFromRoute();
  }

  isAuthenticated() {
    return this.authService.isAuthenticated()
  }

  private getArticle() {
    if (this.id) {
      this.articleService.getArticle(this.id).subscribe({
        next: article => {
          this.article = article;
          this.setSeoShareData();
        },
        error: error => {
          if (error.status == 404) {
            this.router.navigateByUrl('/not-found');
          }
          else {
            this.errorMessage = error.message;
          }
        }
      })
    }
  }

  private setSeoShareData() {
    this.seoService.setData(new SeoShareDataModel(this.article!.title, this.article!.description, this.article!.imageUrl));
  }

  private getArticleIdFromRoute() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getArticle();
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
