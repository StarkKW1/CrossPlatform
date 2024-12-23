import { Component, Input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbar } from '@angular/material/toolbar';
import { MatButton } from '@angular/material/button';
import { MatDrawer } from '@angular/material/sidenav';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [
    MatIconModule,
    MatToolbar,
    MatButton,
    RouterLink
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  @Input()
  public sidenavDrawer!: MatDrawer;

  constructor() { }

  ngOnInit(): void {
  }

  toggleSidenav(): void {
    this.sidenavDrawer.toggle();
  }

}
