import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl} from "@angular/forms";
import {ArticleAdditionEvent} from "../../events/article-addition.event";

import 'prismjs/components/prism-aspnet';
import 'prismjs/components/prism-bash';
import 'prismjs/components/prism-c';
import 'prismjs/components/prism-clike';
import 'prismjs/components/prism-cpp';
import 'prismjs/components/prism-csharp';
import 'prismjs/components/prism-css';
import 'prismjs/components/prism-css-extras';
import 'prismjs/components/prism-dart';
import 'prismjs/components/prism-docker';
import 'prismjs/components/prism-fsharp';
import 'prismjs/components/prism-git';
import 'prismjs/components/prism-go';
import 'prismjs/components/prism-go-module';
import 'prismjs/components/prism-graphql';
import 'prismjs/components/prism-http';
import 'prismjs/components/prism-ignore';
import 'prismjs/components/prism-java';
import 'prismjs/components/prism-javastacktrace';
import 'prismjs/components/prism-javascript';
import 'prismjs/components/prism-json';
import 'prismjs/components/prism-jsonp';
import 'prismjs/components/prism-less';
import 'prismjs/components/prism-markdown';
import 'prismjs/components/prism-mongodb';
import 'prismjs/components/prism-powershell';
import 'prismjs/components/prism-properties';
import 'prismjs/components/prism-protobuf';
import 'prismjs/components/prism-python';
import 'prismjs/components/prism-regex';
import 'prismjs/components/prism-sass';
import 'prismjs/components/prism-scss';
import 'prismjs/components/prism-sql';
import 'prismjs/components/prism-typescript';
import 'prismjs/components/prism-yaml';
import 'prismjs/components/prism-xml-doc';
import 'prismjs/components/prism-textile';

@Component({
  selector: 'app-add-code-snippet',
  templateUrl: './add-code-snippet.component.html',
  styleUrls: ['./add-code-snippet.component.scss']
})
export class AddCodeSnippetComponent implements OnInit {

  constructor(private fb: FormBuilder) { }

  @Input("sectionId")
  sectionId! : number;

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
    { 'language': 'XML',              'code': 'xml'},
    { 'language': 'Plain Text',       'code': 'textile'},
  ]

  ngOnInit(): void {
    this.codeControl = this.fb.control(this.originalCode?? "");
    this.languageSelect = this.originalLanguage?? '';
  }

  updateLanguage() {
    this.language.emit(new ArticleAdditionEvent(`codelanguage-${this.sectionId}`, this.languageSelect));
  }

  updateCode() {
    this.code.emit(new ArticleAdditionEvent(`code-${this.sectionId}`, this.codeControl.value))
  }

}
