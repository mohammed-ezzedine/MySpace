import {Component} from '@angular/core';
import {RouteHelperService} from "./services/route-helper.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private routeService: RouteHelperService) {
  }
}
