import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ArticleAdditionEvent} from "../../../events/article-addition.event";
import {TagService} from "../../../services/tag.service";
import {ArticleService} from "../../../services/article.service";
import {ArticleUtils} from "../../../utils/article.utils";

@Component({
  selector: 'app-add-article',
  templateUrl: './add-article.component.html',
  styleUrls: ['./add-article.component.scss']
})
export class AddArticleComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private tagService: TagService,
              private articleService: ArticleService) { }

  articleForm!: FormGroup;
  contentControls: Array<{ id: number; controlInstance: string }> = [];
  availableTags : string[] | undefined;

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
      tags: [[], []]
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
    if (this.articleForm.valid) {
      let form = {
        title: this.articleForm.controls['title'].value,
        description: this.articleForm.controls['description'].value,
        imageUrl: this.articleForm.controls['imageUrl'].value,
        tags: this.articleForm.controls['tags'].value,
        content: ArticleUtils.getArticleContent(this.contentControls, this.articleForm)
      };
      this.articleService.addArticle(form).subscribe({
        next: _ => this.successMessage = 'Article created successfully!',
        error: err =>  this.errorMessage = 'ERROR: ' + err.message
      });

    } else {
      Object.values(this.articleForm.controls).forEach(control => {
        if (control.invalid) {
          console.log(control)
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  addParagraph(e?: MouseEvent) {
    if (e) {
      e.preventDefault();
    }
    const id = this.contentControls.length > 0 ? this.contentControls[this.contentControls.length - 1].id + 1 : 0;

    const control = {
      id,
      controlInstance: `paragraph-${id}`
    };

    const index = this.contentControls.push(control);
    this.articleForm.addControl(
      this.contentControls[index - 1].controlInstance,
      new FormControl(null, Validators.required)
    );

  }

  addImage(e?: MouseEvent) {
    if (e) {
      e.preventDefault();
    }
    const id = this.contentControls.length > 0 ? this.contentControls[this.contentControls.length - 1].id + 1 : 0;

    const control = {
      id,
      controlInstance: `image-${id}`
    };

    const index = this.contentControls.push(control);
    this.articleForm.addControl(
      this.contentControls[index - 1].controlInstance,
      new FormControl(null, Validators.required)
    );
  }

  addCode(e?: MouseEvent) {
    if (e) {
      e.preventDefault();
    }
    const id = this.contentControls.length > 0 ? this.contentControls[this.contentControls.length - 1].id + 1 : 0;

    const codeControl = {
      id: id,
      controlInstance: `code-${id}`
    };

    const languageControl = {
      id: id,
      controlInstance: `codelanguage-${id}`
    };

    this.contentControls.push(codeControl);

    this.articleForm.addControl(
      codeControl.controlInstance,
      new FormControl(null, Validators.required)
    );

    this.articleForm.addControl(
      languageControl.controlInstance,
      new FormControl(null, Validators.required)
    );
  }

  deleteSection(element: any) {
    this.contentControls = this.contentControls.filter(c => c.id != element.id);
    this.articleForm.removeControl(element.controlInstance)
  }

  getElementType(element: any) : string{
    return element.controlInstance.split('-')[0];
  }

  updateArticleControl(languageEvent: ArticleAdditionEvent) {
    this.articleForm.controls[languageEvent.id].setValue(languageEvent.content);
  }


}
