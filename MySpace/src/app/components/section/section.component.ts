import {AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';

@Component({
  selector: 'app-section',
  templateUrl: './section.component.html',
  styleUrls: ['./section.component.scss']
})
export class SectionComponent implements AfterViewInit {

  constructor() { }

  @Input("html")
  html!: string;

  @ViewChild('content', { static: true })
  content!: ElementRef;

  ngAfterViewInit(): void {
    this.content.nativeElement.innerHTML = this.html;
  }

}
