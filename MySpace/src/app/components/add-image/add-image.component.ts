import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl} from "@angular/forms";
import {ArticleAdditionEvent} from "../../events/article-addition.event";

@Component({
  selector: 'app-add-image',
  templateUrl: './add-image.component.html',
  styleUrls: ['./add-image.component.scss']
})
export class AddImageComponent implements OnInit {

  constructor(private fb: FormBuilder) { }

  @Input("sectionId")
  sectionId! : number;

  @Input("imageUrl")
  originalImageUrl : string | undefined;

  @Output()
  imageUrl = new EventEmitter<ArticleAdditionEvent>();

  imageUrlControl!: FormControl;

  ngOnInit(): void {
    this.imageUrlControl = this.fb.control(this.originalImageUrl?? '');
  }

  updateUrl() {
    this.imageUrl.emit(new ArticleAdditionEvent(`image-${this.sectionId}`, this.imageUrlControl.value));
  }
}
