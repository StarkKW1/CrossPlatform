import {Component, inject} from '@angular/core';
import {MatInput} from '@angular/material/input';
import {MatButton} from '@angular/material/button';
import {Student} from '../../data/student.interface';
import {StudentService} from '../../services/student.service';
import {ExamCardComponent} from '../../Components/exam-card/exam-card.component';
import {DatePipe, JsonPipe} from '@angular/common';
import {ActivatedRoute} from '@angular/router';
import {ExamService} from '../../services/exam.service';

@Component({
  selector: 'app-student-profile',
  imports: [
    DatePipe,
    MatButton
  ],
  templateUrl: './student-profile.component.html',
  styleUrl: './student-profile.component.css'
})
export class StudentProfileComponent {
  private route: ActivatedRoute = inject(ActivatedRoute);
  private examService: ExamService = inject(ExamService);
  private studentService: StudentService = inject(StudentService);
  profile!: Student;
  id: number = this.route.snapshot.params["id"];

  getData() {
    this.studentService.getStudent(this.id).subscribe((data) => {
      this.profile = data;
      console.log(this.profile);
      console.log(this.profile.marks);
    });
  }

  constructor() {
    this.getData();
  }

  updateStudent() {

  }

  daleteStudent() {
    this.studentService.delete(this.id).subscribe();
  }
}
