import { Component, OnInit } from '@angular/core';
import {SeoService} from "../../services/seo.service";
import {SeoShareDataModel} from "../../models/seo-share-data.model";

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.scss']
})
export class NotFoundComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
