import { Component, OnInit } from '@angular/core';
import {TagService} from "../../services/tag.service";

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent implements OnInit {

  constructor(private tagService: TagService) { }

  searchText = null;
  tags: string[] | undefined;
  errorMessage : string | undefined;

  ngOnInit(): void {
    this.getRecommendedTags();
  }

  private getRecommendedTags() {
    this.tagService.getRecommendedTags().subscribe({
      next: tags => {
        this.tags = tags;
      },
      error: error => {
        this.errorMessage = error;
      }
    })
  }
}
