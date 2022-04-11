import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {TagService} from "../../services/tag.service";

@Component({
  selector: 'app-recommended-tags',
  templateUrl: './recommended-tags.component.html',
  styleUrls: ['./recommended-tags.component.scss']
})
export class RecommendedTagsComponent implements OnInit {

  tags: string[] | undefined;
  errorMessage : string | undefined;

  constructor(private router: Router,
              private tagService: TagService) { }

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
