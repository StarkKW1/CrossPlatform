import { Component } from '@angular/core';
import {MatDrawer} from '@angular/material/sidenav';
import {HeaderComponent} from '../header/header.component';
import {SidenavComponent} from '../sidenav/sidenav.component';

@Component({
  selector: 'app-layout',
  imports: [
    HeaderComponent,
    SidenavComponent
  ],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {
  public sidenavDrawer!: MatDrawer;

  public setSidenav(sidenavDrawer: MatDrawer) {
    this.sidenavDrawer = sidenavDrawer;
  }
}
