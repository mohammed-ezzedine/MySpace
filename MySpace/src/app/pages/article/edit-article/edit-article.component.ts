import { Component, OnInit } from '@angular/core';
import {Article} from "../../../models/article";
import {ArticleService} from "../../../services/article.service";
import {ActivatedRoute} from "@angular/router";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ArticleAdditionEvent} from "../../../events/article-addition.event";
import {ArticleUtils} from "../../../utils/article.utils";
import {readingTime} from "reading-time-estimator";
import {TagService} from "../../../services/tag.service";

@Component({
  selector: 'app-edit-article',
  templateUrl: './edit-article.component.html',
  styleUrls: ['./edit-article.component.scss']
})
export class EditArticleComponent implements OnInit {

  id: string | undefined;
  article: Article | undefined;
  availableTags: string[] | undefined;

  errorMessage: string | undefined;
  successMessage: string | undefined;

  articleForm: FormGroup | undefined;
  contentControls: Array<{ id: number; controlInstance: string }> = [];

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private tagService: TagService,
              private articleService: ArticleService) { }

  ngOnInit(): void {
    this.getArticleIdFromRoute();
    this.getArticle();
    this.getAvailableTags();
  }

  submitForm() {
    if (this.articleForm!.valid) {
      let form = {
        title: this.articleForm!.controls['title'].value,
        description: this.articleForm!.controls['description'].value,
        imageUrl: this.articleForm!.controls['imageUrl'].value,
        tags: this.articleForm!.controls['tags'].value,
        content: ArticleUtils.getArticleContent(this.contentControls, this.articleForm!),
        estimatedReadingTime: ArticleUtils.getArticleEstimatedReadingTime(this.contentControls, this.articleForm!)
      };
      this.articleService.updateArticle(this.id!, form).subscribe({
        next: _ => this.successMessage = 'Article updated successfully!',
        error: err =>  this.errorMessage = 'ERROR: ' + err.message
      });

    } else {
      Object.values(this.articleForm!.controls).forEach(control => {
        if (control.invalid) {
          console.log(control)
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  private getAvailableTags() {
    this.tagService.getAllTags().subscribe({
      next: tags => {
        this.availableTags = tags
      }
    })
  }

  private initializeForm() {
    this.articleForm = this.fb.group({
      title: [this.article?.title, [Validators.required]],
      description: [this.article?.description, [Validators.required]],
      imageUrl: [this.article?.imageUrl, [Validators.pattern("https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#*?&//=]*)")]],
      tags: [this.article?.tags?? [], []]
    })

    this.initializeContentControls();
  }

  private initializeContentControls() {
    if (!this.article) {
      return;
    }

    ArticleUtils.initializeContentControls(this.articleForm!, this.contentControls, this.article.content);
  }

  private getArticleIdFromRoute() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    })
  }

  private getArticle() {
    if (this.id) {
      this.articleService.getArticle(this.id).subscribe({
        next: article => {
          this.article = article;
          this.initializeForm();
        },
        error: error => {
          this.errorMessage = error.message;
        }
      })
    }
  }
}
