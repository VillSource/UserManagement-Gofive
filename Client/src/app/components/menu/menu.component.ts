import { Component, Input, input } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss',
})
export class MenuComponent {
  @Input()
  icon: string | undefined = 'info';

  @Input({ required: true })
  name!: string;

  @Input()
  linkTo: string[] | null = null;
}
