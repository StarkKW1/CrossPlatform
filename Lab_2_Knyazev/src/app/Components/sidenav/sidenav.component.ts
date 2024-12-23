import {Component, Output, EventEmitter, ViewChild} from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatDrawer, MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { MatListItem, MatNavList } from '@angular/material/list';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidenav',
  imports: [
    MatIcon,
    MatSidenavModule,
    MatNavList,
    RouterModule,
    MatListItem,
    MatSidenav,
  ],
  templateUrl: './sidenav.component.html',
  styleUrl: './sidenav.component.css'
})
export class SidenavComponent {
  opened: boolean = false;

  @Output()
  public setSidenavControl: EventEmitter<MatDrawer> = new EventEmitter<MatDrawer>(true);

  @ViewChild('drawer', {static: true})
  public drawer!: MatDrawer;

  constructor() { }

  ngOnInit(): void {
    this.setSidenavControl.emit(this.drawer);
  }
}
