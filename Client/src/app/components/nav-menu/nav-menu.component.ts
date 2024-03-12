import { Component } from '@angular/core';
import { MenuComponent } from '../menu/menu.component';
import { JsonPipe, NgFor } from '@angular/common';

interface NavMenuList {
  icon?: string;
  name: string;
  linkTo: string[];
}

interface NavMenuGroup {
  listName?: string;
  menu: NavMenuList[];
}

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.scss',
  imports: [MenuComponent, JsonPipe, NgFor],
})
export class NavMenuComponent {
  private menus1: NavMenuGroup = {
    menu: [
      { name: 'Dashboard', icon: 'keyboard_command_key', linkTo: ['/'] },
      { name: 'Users', icon: 'bar_chart', linkTo: ['/'] },
      { name: 'Documents', icon: 'description', linkTo: ['/'] },
      { name: 'Photos', icon: 'photo', linkTo: ['/'] },
      { name: 'Hierarchy', icon: 'volunteer_activism', linkTo: ['/'] },
    ],
  };

  private menus2: NavMenuGroup = {
    menu: [
      { name: 'Message', icon: 'sms', linkTo: ['/'] },
      { name: 'Help', icon: 'help', linkTo: ['/'] },
      { name: 'Setting', icon: 'settings', linkTo: ['/'] },
    ],
  };

  navMenuItems = [this.menus1, this.menus2];
}
