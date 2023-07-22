import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ArticleEstimatedReadingTimeCalculator} from "../../utils/articleEstimatedReadingTimeCalculator";
import {ArticleAdditionEvent} from "../../events/article-addition.event";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {PageContentSection} from "./page-content-section";

@Component({
  selector: 'app-page-editor',
  templateUrl: './page-editor.component.html',
  styleUrls: ['./page-editor.component.scss']
})
export class PageEditorComponent implements OnInit {

  @Input()
  controls: PageContentSection[] = [];

  @Output()
  updatedControls = new EventEmitter<PageContentSection[]>();

  constructor() { }

  ngOnInit(): void {
  }

  getElementType(element: any) : string{
    return element.type;
  }

  updateSectionContent(index: number, languageEvent: ArticleAdditionEvent) {
    this.controls[index].content.setValue(languageEvent.content)
  }


  updateSectionMetadata(index: number, languageEvent: ArticleAdditionEvent) {
    this.controls[index].metadata = languageEvent.content
  }

  private controlsUpdated() {
    console.log("controls updated", this.controls)
    this.updatedControls.emit(this.controls);
  }

  addParagraph(index: number) {
    this.addSectionAtIndex(index, this.getNewParagraphSection());
  }

  addCode(index: number) {
    this.addSectionAtIndex(index, this.getNewCodeSection());
  }

  addImage(index: number) {
    this.addSectionAtIndex(index, this.getNewImageSection());
  }

  private addSectionAtIndex(index: number, newParagraphSection: PageContentSection) {
    let copyOfControls = this.controls;
    this.controls = []
    for (let i = 0; i < copyOfControls.length; i++) {
      if (i == index) {
        this.controls.push(newParagraphSection)
      }

      this.controls.push(copyOfControls[i])
    }
    this.controlsUpdated();
  }

  private getNewParagraphSection() : PageContentSection {
    return {type: 'paragraph', content: new FormControl('', Validators.required)};
  }

  private getNewCodeSection() {
    return {type: 'code', content: new FormControl('', Validators.required)};
  }

  private getNewImageSection() {
    return {type: 'image', content: new FormControl('', Validators.required)};
  }

  deleteSection(index: number) {
    this.controls = this.controls.filter(c => c != this.controls[index])
    this.controlsUpdated();
  }

  private dragSections(indexOfDraggedElement: number, indexOfTargetElement: number) {
    if (indexOfDraggedElement > indexOfTargetElement) {
      let target = this.controls[indexOfDraggedElement]
      for (let i = indexOfTargetElement; i <= indexOfDraggedElement; i++) {
        let temp = this.controls[i];
        this.controls[i] = target;
        target = temp
      }
    } else {
      for (let i = indexOfDraggedElement; i < indexOfTargetElement; i++) {
        let temp = this.controls[i];
        this.controls[i] = this.controls[i + 1];
        this.controls[i + 1] = temp;
      }
    }
  }

  onDrop($event: DragEvent) {
    $event.preventDefault();

    let indexOfDraggedElement= Number.parseInt($event.dataTransfer?.getData("text/plain")!);
    let indexOfTargetElement = Number.parseInt(this.getHtmlElementId($event.target));

    console.log("drop from", indexOfDraggedElement, "to", indexOfTargetElement)

    this.dragSections(indexOfDraggedElement, indexOfTargetElement);
  }

  onDragStart($event: DragEvent) {
    console.log("drag", this.getHtmlElementId($event.target))
    $event.dataTransfer?.setData("text/plain", this.getHtmlElementId($event.target))
  }

  private getHtmlElementId(target: EventTarget | null) {
    return (target as HTMLElement).id;
  }

  allowDrop($event: DragEvent) {
    $event.preventDefault()
  }

  addParagraphAtEnd() {
    this.controls.push(this.getNewParagraphSection())
  }

  addCodeAtEnd() {
    this.controls.push(this.getNewCodeSection())
  }

  addImageAtEnd() {
    this.controls.push(this.getNewImageSection())
  }
}
