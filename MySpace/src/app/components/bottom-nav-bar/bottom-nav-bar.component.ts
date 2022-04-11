import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-bottom-nav-bar',
  templateUrl: './bottom-nav-bar.component.html',
  styleUrls: ['./bottom-nav-bar.component.scss']
})
export class BottomNavBarComponent implements OnInit {

  constructor() { }

  tabs = [
    {
      icon: 'home',
      link: '/home'
    },
    {
      icon: 'search',
      link: '/search'
    },
    {
      icon: 'linkedin',
      link: '/jobs'
    },
    {
      icon: 'project',
      link: '/projects'
    },
  ];

  ngOnInit(): void {
  }



}
