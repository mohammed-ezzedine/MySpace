import {AfterViewInit, Component, ElementRef, Input, OnInit, ViewEncapsulation} from '@angular/core';

import {highlight, languages} from "prismjs";
import {NzMessageService} from "ng-zorro-antd/message";


@Component({
  selector: 'app-code-snippet',
  templateUrl: './code-snippet.component.html',
  styleUrls: ['./code-snippet.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class CodeSnippetComponent implements AfterViewInit {

  constructor(private el: ElementRef,
              private message: NzMessageService) { }

  @Input("code")
  code! : string;

  @Input("language")
  language! : string;


  ngAfterViewInit(): void {
    if (!this.code || !this.language) {
      return;
    }

    const grammar = languages[this.language];

    let html = '<pre class="code-snippet">'
    for (let line of this.code.split("\n")) {
      let highlightedLine =  highlight(line, grammar, this.language);
      html += `<code>${highlightedLine}</code>\n`
    }
    html += '</pre>'

    document.getElementById("code-content")!.innerHTML = html;
  }

  copyCode() {
    navigator.clipboard.writeText(this.code)
      .then(_ => {
        this.message.info("Code copied to clipboard.")
      });
  }

}
