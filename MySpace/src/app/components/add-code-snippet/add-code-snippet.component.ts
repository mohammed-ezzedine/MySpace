import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl} from "@angular/forms";
import {ArticleAdditionEvent} from "../../events/article-addition.event";

@Component({
  selector: 'app-add-code-snippet',
  templateUrl: './add-code-snippet.component.html',
  styleUrls: ['./add-code-snippet.component.scss']
})
export class AddCodeSnippetComponent implements OnInit {

  constructor(private fb: FormBuilder) { }

  @Input("id")
  id! : number;

  @Input("language")
  originalLanguage: string | undefined;

  @Input("code")
  originalCode: string | undefined;

  @Output()
  code = new EventEmitter<ArticleAdditionEvent>();

  @Output()
  language = new EventEmitter<ArticleAdditionEvent>();

  codeControl!: FormControl;
  languageSelect =  '';

  languages = [
    { 'language': '.NET',             'code': 'aspnet'},
    { 'language': 'BASH',             'code': 'bash'},
    { 'language': 'C',                'code': 'c'},
    { 'language': 'C-Like',           'code': 'clike'},
    { 'language': 'C++',              'code': 'cpp'},
    { 'language': 'C#',               'code': 'csharp'},
    { 'language': 'CSS',              'code': 'css'},
    { 'language': 'CSS Extras',       'code': 'css-extras'},
    { 'language': 'Dart',             'code': 'dart'},
    { 'language': 'Docker',           'code': 'docker'},
    { 'language': 'F#',               'code': 'fsharp'},
    { 'language': 'GIT',              'code': 'git'},
    { 'language': 'GO',               'code': 'go'},
    { 'language': 'GO Module',        'code': 'go-module'},
    { 'language': 'GraphQL',          'code': 'graphql'},
    { 'language': 'HTTP',             'code': 'http'},
    { 'language': '.ignore',          'code': 'ignore'},
    { 'language': 'Java',             'code': 'java'},
    { 'language': 'Java Stacktrace',  'code': 'javastacktrace'},
    { 'language': 'Javascript',       'code': 'javascript'},
    { 'language': 'JSON',             'code': 'json'},
    { 'language': 'JSONP',            'code': 'jsonp'},
    { 'language': 'LESS',             'code': 'less'},
    { 'language': 'Markdown',         'code': 'markdown'},
    { 'language': 'MongoDB',          'code': 'mongodb'},
    { 'language': 'Powershell',       'code': 'powershell'},
    { 'language': 'Properties',       'code': 'properties'},
    { 'language': 'Protobuf',         'code': 'protobuf'},
    { 'language': 'Python',           'code': 'python'},
    { 'language': 'ReGex',            'code': 'regex'},
    { 'language': 'SASS',             'code': 'sass'},
    { 'language': 'SCSS',             'code': 'scss'},
    { 'language': 'SQL',              'code': 'sql'},
    { 'language': 'Typescript',       'code': 'typescript'},
    { 'language': 'Yaml',             'code': 'yaml'},
  ]

  ngOnInit(): void {
    this.codeControl = this.fb.control(this.originalCode?? "");
    this.languageSelect = this.originalLanguage?? '';
  }

  updateLanguage() {
    this.language.emit(new ArticleAdditionEvent(`codelanguage-${this.id}`, this.languageSelect));
  }

  updateCode() {
    this.code.emit(new ArticleAdditionEvent(`code-${this.id}`, this.codeControl.value))
  }

}
