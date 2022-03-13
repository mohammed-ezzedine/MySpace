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

  @Input("id")
  id! : number;

  @Output()
  imageUrl = new EventEmitter<ArticleAdditionEvent>();

  imageUrlControl!: FormControl;

  ngOnInit(): void {
    this.imageUrlControl = this.fb.control('');
  }

  updateUrl() {
    this.imageUrl.emit(new ArticleAdditionEvent(`image-${this.id}`, this.imageUrlControl.value));
  }
}
