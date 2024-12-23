import {Component, Input} from '@angular/core';
import {MatButton} from '@angular/material/button';
import {Student} from '../../data/student.interface';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-student-card',
  imports: [
    MatButton,
    RouterLink
  ],
  templateUrl: './student-card.component.html',
  styleUrl: './student-card.component.css'
})
export class StudentCardComponent {
  @Input() profile!: Student;
}
