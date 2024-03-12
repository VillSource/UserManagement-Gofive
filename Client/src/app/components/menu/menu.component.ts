import { Component, Input, input } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

type ICON = 'info' | 'keyboard_command_key';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss',
})
export class MenuComponent {
  @Input()
  icon: string | ICON = 'info';

  @Input({ required: true })
  name!: string;

  @Input()
  linkTo: string[] | null = null;
}
