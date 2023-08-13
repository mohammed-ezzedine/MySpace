import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {Article} from "../../models/article";
import {ArticleService} from "../../services/article.service";

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent implements OnInit {

  searchText : string | null = null;
  nzFilterOption = (): boolean => true;
  suggestions = new Array<Article>();

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private articleService: ArticleService) { }

  ngOnInit(): void {
  }

  searchArticles() {
    if (!this.searchText || this.searchText == '') {
      return;
    }

    this.router.navigateByUrl('/home?q=' + this.searchText);
  }

  search(value: string): void {
    if (value == '') {
      return;
    }

    this.searchText = value;

    this.articleService.getArticles(1, value)
      .subscribe({
        next: articles => this.suggestions = articles.items
      })
  }
}
