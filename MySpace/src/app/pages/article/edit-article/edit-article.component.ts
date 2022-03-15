import { Component, OnInit } from '@angular/core';
import {Article} from "../../../models/article";
import {ArticleService} from "../../../services/article.service";
import {ActivatedRoute} from "@angular/router";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ArticleAdditionEvent} from "../../../events/article-addition.event";
import {ArticleUtils} from "../../../utils/article.utils";

@Component({
  selector: 'app-edit-article',
  templateUrl: './edit-article.component.html',
  styleUrls: ['./edit-article.component.scss']
})
export class EditArticleComponent implements OnInit {

  id: string | undefined;
  article: Article | undefined;

  errorMessage: string | undefined;
  successMessage: string | undefined;

  articleForm: FormGroup | undefined;
  contentControls: Array<{ id: number; controlInstance: string }> = [];

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private articleService: ArticleService) { }

  ngOnInit(): void {
    this.getArticleIdFromRoute();
    this.getArticle();
  }

  submitForm() {
    if (this.articleForm!.valid) {
      let form = {
        title: this.articleForm!.controls['title'].value,
        description: this.articleForm!.controls['description'].value,
        imageUrl: this.articleForm!.controls['imageUrl'].value,
        tags: this.articleForm!.controls['tags'].value,
        content: ArticleUtils.getArticleContent(this.contentControls, this.articleForm!)
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

  private initializeForm() {
    this.articleForm = this.fb.group({
      title: [this.article?.title, [Validators.required]],
      description: [this.article?.description, [Validators.required]],
      imageUrl: [this.article?.imageUrl, [Validators.pattern("https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#*?&//=]*)")]],
      tags: [this.article?.tags?? [], []]
    })

    console.log(this.articleForm)
    this.initializeContentControls();
  }

  private initializeContentControls() {
    if (!this.article) {
      return;
    }

    for (let section of this.article.content) {
      const id = this.contentControls.length > 0 ? this.contentControls[this.contentControls.length - 1].id + 1 : 0;

      this.addExistingContentSection(section, id);
    }
  }

  private addExistingContentSection(section: any, id: number) {
    switch (section.type) {
      case 'code':
        this.addExistingCodeSection(id, section);
        break;
      case 'paragraph':
        this.addExistingParagraphSection(id, section);
        break;
      case 'image':
        this.addExistingImageSection(id, section);
        break;
      default:
        break;
    }
  }

  private addExistingImageSection(id: number, section: any) {
    const control = {
      id,
      controlInstance: `image-${id}`
    };

    const index = this.contentControls.push(control);
    this.articleForm!.addControl(
      this.contentControls[index - 1].controlInstance,
      new FormControl(section.imageUrl, Validators.required)
    );
  }

  private addExistingParagraphSection(id: number, section: any) {
    const control = {
      id,
      controlInstance: `paragraph-${id}`
    };

    const index = this.contentControls.push(control);
    this.articleForm!.addControl(
      this.contentControls[index - 1].controlInstance,
      new FormControl(section.content, Validators.required)
    );
  }

  private addExistingCodeSection(id: number, section: any) {
    const control = {
      id,
      controlInstance: `code-${id}`
    };

    const languageControl = {
      id: id,
      controlInstance: `codelanguage-${id}`
    };

    const index = this.contentControls.push(control);
    this.articleForm!.addControl(
      this.contentControls[index - 1].controlInstance,
      new FormControl(section.content, Validators.required)
    );

    this.articleForm!.addControl(
      languageControl.controlInstance,
      new FormControl(section.language, Validators.required)
    );
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

  updateArticleControl(languageEvent: ArticleAdditionEvent) {
    this.articleForm!.controls[languageEvent.id].setValue(languageEvent.content);
  }

  deleteSection(element: any) {
    this.contentControls = this.contentControls.filter(c => c.id != element.id);
    this.articleForm!.removeControl(element.controlInstance)
  }

  getElementType(element: any) : string{
    return element.controlInstance.split('-')[0];
  }

  getControlValue(controlInstance: string) : string {
    return this.articleForm?.controls[controlInstance].value;
  }
}
