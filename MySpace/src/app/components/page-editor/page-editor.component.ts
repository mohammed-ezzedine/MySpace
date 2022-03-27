import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ArticleUtils} from "../../utils/article.utils";
import {ArticleAdditionEvent} from "../../events/article-addition.event";
import {FormGroup} from "@angular/forms";

@Component({
  selector: 'app-page-editor',
  templateUrl: './page-editor.component.html',
  styleUrls: ['./page-editor.component.scss']
})
export class PageEditorComponent implements OnInit {

  @Input("contentControls")
  contentControls!: Array<{ id: number; controlInstance: string }>;

  @Input("form")
  form!: FormGroup;

  constructor() { }

  ngOnInit(): void {
  }

  getElementType(element: any) : string{
    return ArticleUtils.getElementType(element);
  }

  getControlValue(controlInstance: string) : string {
    return this.form.controls[controlInstance].value;
  }

  updateArticleControl(languageEvent: ArticleAdditionEvent) {
    this.form.controls[languageEvent.id].setValue(languageEvent.content);
  }

  addParagraph($event: MouseEvent) {
    ArticleUtils.addParagraph(this.form, this.contentControls, $event);
  }

  addCode($event: MouseEvent) {
    ArticleUtils.addCode(this.form, this.contentControls, $event);
  }

  addImage($event: MouseEvent) {
    ArticleUtils.addImage(this.form, this.contentControls, $event);
  }

  deleteSection(element: { id: number; controlInstance: string }) {
    ArticleUtils.deleteSection(this.form, this.contentControls, element);
  }
}
