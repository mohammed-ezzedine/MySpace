import { Component, OnInit } from '@angular/core';
import {Article} from "../../../models/article";
import {ArticleService} from "../../../services/article.service";
import {ActivatedRoute} from "@angular/router";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ArticleAdditionEvent} from "../../../events/article-addition.event";
import {ArticleEstimatedReadingTimeCalculator} from "../../../utils/articleEstimatedReadingTimeCalculator";
import {readingTime} from "reading-time-estimator";
import {TagService} from "../../../services/tag.service";
import {PageContentSection} from "../../../components/page-editor/page-content-section";

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
  controls : PageContentSection[] = [];

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
    if (this.articleForm!.valid && this.areSectionsValid()) {
      let form = {
        title: this.articleForm!.controls['title'].value,
        description: this.articleForm!.controls['description'].value,
        imageUrl: this.articleForm!.controls['imageUrl'].value,
        tags: this.articleForm!.controls['tags'].value,
        content: this.getContentSections(),
        estimatedReadingTime: ArticleEstimatedReadingTimeCalculator.calculate(this.controls)
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

  private areSectionsValid() {
    return this.controls.map(c => c.content.valid).reduce((p, c) => p && c);
  }

  private getContentSections() {
    return this.controls.map(c => {
      switch (c.type) {
        case 'code': return { type: 'code', language: c.metadata, content: c.content.value };
        case 'image': return { type: 'image', imageUrl: c.content.value };
        case 'paragraph': return { type: 'paragraph', content: c.content.value };
        default: return null;
      }
    })
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

    this.initializePageContent(this.article.content);

  }

  private initializePageContent(content: any[]) {
    for (let section of content) {
      if (section.type == 'paragraph') {
        this.controls.push({type: 'paragraph', content: new FormControl(section.content, Validators.required)})
      } else if (section.type == 'code') {
        this.controls.push({
          type: 'code',
          metadata: section.language,
          content: new FormControl(section.content, Validators.required)
        })
      } else {
        this.controls.push({type: 'image', content: new FormControl(section.imageUrl, Validators.required)})
      }
    }
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

  updateControls(updatedControls: PageContentSection[]) {
    this.controls = updatedControls
  }
}
