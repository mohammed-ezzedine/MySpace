import {Component, OnInit} from '@angular/core';
import {SeoService} from "../../services/seo.service";
import {SeoShareDataModel} from "../../models/seo-share-data.model";

@Component({
  selector: 'app-unauthorized',
  templateUrl: './unauthorized.component.html',
  styleUrls: ['./unauthorized.component.scss']
})
export class UnauthorizedComponent implements OnInit {

  constructor(private seoService: SeoService) { }

  ngOnInit(): void {
    this.seoService.setData(new SeoShareDataModel("401 - Unauthorized"));
  }

}
