import { Component, OnInit } from '@angular/core';
import {TagService} from "../../services/tag.service";
import {FormBuilder, FormControl} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private tagService: TagService) { }

  searchText!: FormControl;
  tags: string[] | undefined;
  errorMessage : string | undefined;

  ngOnInit(): void {
    this.route.queryParams.subscribe({
      next: params => {
        this.searchText = this.fb.control((params['q'])? params['q'] :'');
      }
    })
    this.getRecommendedTags();
  }

  searchArticles() {
    if (this.searchText.value == '') {
      return;
    }

    this.router.navigateByUrl('/home?q=' + this.searchText.value);
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
