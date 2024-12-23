import {Component, inject} from '@angular/core';
import {StudentCardComponent} from '../../Components/student-card/student-card.component';
import {StudentService} from '../../services/student.service';
import {Student} from '../../data/student.interface';
import {MatButton} from '@angular/material/button';

@Component({
  selector: 'app-search-student',
  imports: [
    StudentCardComponent,
    MatButton
  ],
  templateUrl: './search-student.component.html',
  styleUrl: './search-student.component.css'
})
export class SearchStudentComponent {
  studentService = inject(StudentService);
  students: Student[] = []

  constructor() {
    this.studentService.getStudents()
      .subscribe(val => {
        this.students = val;
      })
  }
}
