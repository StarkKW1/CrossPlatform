import {Component, inject} from '@angular/core';
import {Student} from '../../data/student.interface';
import {GroupService} from '../../services/group.service';
import {Group} from '../../data/group.interface';
import {MatButton} from '@angular/material/button';
import {StudentCardComponent} from '../../Components/student-card/student-card.component';

@Component({
  selector: 'app-search-group',
  imports: [
    MatButton,
    StudentCardComponent
  ],
  templateUrl: './search-group.component.html',
  styleUrl: './search-group.component.css'
})
export class SearchGroupComponent {
  groupService = inject(GroupService);
  groups: Group[] = []

  constructor() {
    this.groupService.getGroups()
      .subscribe(val => {
        this.groups = val;
        /*console.log(this.groups);*/
      })
  }
}
