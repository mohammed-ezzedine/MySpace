import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent implements OnInit {

  searchText!: FormControl;

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.initSearchQuery();
  }

  private initSearchQuery() {
    this.route.queryParams.subscribe({
      next: params => {
        this.searchText = this.fb.control((params['q']) ? params['q'] : '');
      }
    })
  }

  searchArticles() {
    if (this.searchText.value == '') {
      return;
    }

    this.router.navigateByUrl('/home?q=' + this.searchText.value);
  }
}
