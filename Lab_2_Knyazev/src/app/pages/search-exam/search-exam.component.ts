import {Component, inject} from '@angular/core';
import {StudentCardComponent} from '../../Components/student-card/student-card.component';
import {ExamCardComponent} from '../../Components/exam-card/exam-card.component';
import {ExamService} from '../../services/exam.service';
import {Exam} from '../../data/exam.interface';
import {MatIcon} from '@angular/material/icon';
import {RouterLink} from '@angular/router';
import {MatButton} from '@angular/material/button';

@Component({
  selector: 'app-search-exam',
  imports: [
    ExamCardComponent,
    MatIcon,
    RouterLink,
    MatButton
  ],
  templateUrl: './search-exam.component.html',
  styleUrl: './search-exam.component.css'
})
export class SearchExamComponent {
  examService = inject(ExamService);
  exams: Exam[] = []

  constructor() {
    this.examService.getExams()
      .subscribe(val => {
        this.exams = val;
      })
  }
}
