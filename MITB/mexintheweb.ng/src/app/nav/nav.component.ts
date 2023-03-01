import { Component } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  isExpanded = false;
  public navigationEntries: NavigationEntryModel[] = new Array<NavigationEntryModel>();

  constructor() {
    this.navigationEntries = new Array<NavigationEntryModel>();
    this.navigationEntries.push({linkRoute: '/', linkText: 'home'});
    this.navigationEntries.push({linkRoute: '/mexlogin', linkText: 'login'});
    this.navigationEntries.push({linkRoute: '/about', linkText: 'über mich'});
    this.navigationEntries.push({linkRoute: '/impressum', linkText: 'impressum'});
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

export class NavigationEntryModel {
  public linkRoute: string | undefined;
  public linkText: string | undefined;
}
