import {Component, Inject, Input, OnInit, PLATFORM_ID} from '@angular/core';
import {Article} from "../../models/article";
import {DateUtils} from "../../utils/date.utils";
import {NzMessageService} from "ng-zorro-antd/message";
import {Router} from "@angular/router";
import {isPlatformBrowser} from '@angular/common';
import { faLink } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-article-thumbnail',
  templateUrl: './article-thumbnail.component.html',
  styleUrls: ['./article-thumbnail.component.scss']
})
export class ArticleThumbnailComponent implements OnInit {

  @Input("article")
  article!: Article;

  faLink = faLink;

  constructor(private message: NzMessageService,
              private router: Router,
              @Inject(PLATFORM_ID) private platformId: Object) { }

  ngOnInit(): void {
  }

  getDateString(date: any) : string {
    return DateUtils.getDateString(date);
  }

  copyArticleUrl() {
    navigator.clipboard.writeText(this.getArticleUrl())
      .then(_ => {
        this.message.info("Article address copied to clipboard.")
      });
  }

  getArticleUrl() : string {
    let path = this.router.parseUrl("/article/" + this.article.id).toString();
    return isPlatformBrowser(this.platformId)
      ? window.location.origin + path
      : path;
  }

  getArticleTags() {
    return this.article.tags.slice(0, 4)
  }
}
