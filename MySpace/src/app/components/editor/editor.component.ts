import {AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {ArticleAdditionEvent} from "../../events/article-addition.event";
import {FormBuilder, FormControl} from "@angular/forms";
declare const MediumEditor: any;

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements AfterViewInit, OnInit  {

  contentControl!: FormControl;

  editor: any;

  @ViewChild('editable', { static: true })
  editable!: ElementRef;

  @Input("id")
  id! : number;

  @Output()
  content = new EventEmitter<ArticleAdditionEvent>()

  static mediumOptions = {
    autoLink: true,
    targetBlank: true,
    toolbar: {
      allowMultiParagraphSelection: true,
      diffLeft: 0,
      diffTop: -10,
      firstButtonClass: 'medium-editor-button-first',
      lastButtonClass: 'medium-editor-button-last',
      relativeContainer: null,
      standardizeSelectionStart: false,
      static: true,
      align: 'center',
      sticky: false,
      updateOnEmptySelection: false,
      buttons: [
        'bold', 'italic', 'underline', 'strikethrough', 'anchor', 'subscript', 'superscript', 'quote', 'pre',
        'h1', 'h2', 'h3', 'justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull', 'quote', 'removeFormat', 'html',
        'orderedlist', 'unorderedlist', 'indent', 'outdent'
      ]
    },
    paste: {
      cleanPastedHTML: true,
      cleanAttrs: ['style', 'dir'],
      cleanTags: ['label', 'meta'],
      unwrapTags: ['sub', 'sup']
    },
    keyboardCommands: {
      commands: [
        {
          command: 'bold',
          key: 'B',
          meta: true,
          shift: false,
          alt: false
        },
        {
          command: 'italic',
          key: 'I',
          meta: true,
          shift: false,
          alt: false
        },
        {
          command: 'underline',
          key: 'U',
          meta: true,
          shift: false,
          alt: false
        }
      ],
    }
  }

  constructor(private fb: FormBuilder) { }

  ngAfterViewInit(): void {
    this.editor = new MediumEditor(this.editable.nativeElement, EditorComponent.mediumOptions);

    this.editor.subscribe('editableInput', (event: any, editable: any) => {
      this.content.emit(new ArticleAdditionEvent(`paragraph-${this.id}`, editable.innerHTML))
    });
  }

  ngOnInit(): void {
    this.contentControl = this.fb.control('');
  }

}
