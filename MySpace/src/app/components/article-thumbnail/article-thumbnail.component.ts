import {Component, Input, OnInit} from '@angular/core';
import {Article} from "../../models/article";

@Component({
  selector: 'app-article-thumbnail',
  templateUrl: './article-thumbnail.component.html',
  styleUrls: ['./article-thumbnail.component.scss']
})
export class ArticleThumbnailComponent implements OnInit {

  @Input("article")
  article!: Article;

  constructor() { }

  ngOnInit(): void {
  }

  getDateString(date: any) : string {
    return new Date(date).toDateString();
  }

}
