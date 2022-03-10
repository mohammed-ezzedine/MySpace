import { Component, OnInit } from '@angular/core';
import {TagService} from "../../services/tag.service";
import {Tag} from "../../models/tag";

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent implements OnInit {

  constructor(private tagService: TagService) { }

  searchText = null;
  tags: Tag[] | undefined;
  errorMessage : string | undefined;

  ngOnInit(): void {
    this.getRecommendedTags();
  }

  private getRecommendedTags() {
    this.tagService.getRecommendedTags().subscribe(tags => {
      this.tags = tags;
    }, error => {
      this.errorMessage = error;
    })
  }
}
