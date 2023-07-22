import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ArticleAdditionEvent} from "../../../events/article-addition.event";
import {TagService} from "../../../services/tag.service";
import {ArticleService} from "../../../services/article.service";
import {ArticleEstimatedReadingTimeCalculator} from "../../../utils/articleEstimatedReadingTimeCalculator";
import {readingTime} from "reading-time-estimator";
import {PageContentSection} from "../../../components/page-editor/page-content-section";

@Component({
  selector: 'app-add-article',
  templateUrl: './add-article.component.html',
  styleUrls: ['./add-article.component.scss']
})
export class AddArticleComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private tagService: TagService,
              private articleService: ArticleService) {
  }

  articleForm!: FormGroup;
  controls : PageContentSection[] = [];
  availableTags: string[] | undefined;

  successMessage: string | undefined;
  errorMessage: string | undefined;

  ngOnInit(): void {
    this.initializeForm();
    this.getAvailableTags();
  }

  private initializeForm() {
    this.articleForm = this.fb.group({
      title: [null, [Validators.required]],
      description: [null, [Validators.required]],
      imageUrl: [null, [Validators.pattern("https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#*?&//=]*)")]],
      tags: [[], []],
    })
  }

  private getAvailableTags() {
    this.tagService.getAllTags().subscribe({
      next: tags => {
        this.availableTags = tags
      }
    })
  }

  submitForm(): void {
    if (this.articleForm.valid && this.areSectionsValid()) {
      let form = {
        title: this.articleForm.controls['title'].value,
        description: this.articleForm.controls['description'].value,
        imageUrl: this.articleForm.controls['imageUrl'].value,
        tags: this.articleForm.controls['tags'].value,
        content: this.getContentSections(),
        estimatedReadingTime: ArticleEstimatedReadingTimeCalculator.calculate(this.controls)
      };

      this.articleService.addArticle(form).subscribe({
        next: _ => this.successMessage = 'Article created successfully!',
        error: err => this.errorMessage = 'ERROR: ' + err.message
      });

    } else {
      this.displayFormDirtyControls();
    }
  }

  private displayFormDirtyControls() {
    Object.values(this.articleForm.controls).forEach(control => {
      if (control.invalid) {
        control.markAsDirty();
        control.updateValueAndValidity({onlySelf: true});
      }
    });
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

  private areSectionsValid() {
    return this.controls.map(c => c.content.valid).reduce((p, c) => p && c);
  }

  updateControls(updatedContent: PageContentSection[]) {
    this.controls = updatedContent;
  }
}
