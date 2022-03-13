import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.scss']
})
export class TagsComponent implements OnInit {

  constructor() { }

  @Input("tags")
  tags: string[] | undefined;

  @Input("skeleton")
  skeleton: boolean = false;

  ngOnInit(): void {
  }

}
