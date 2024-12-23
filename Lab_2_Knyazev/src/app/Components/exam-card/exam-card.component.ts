import {Component, Input} from '@angular/core';
import {Exam} from '../../data/exam.interface';
import {MatButton} from '@angular/material/button';
import {DatePipe} from '@angular/common';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-exam-card',
  imports: [
    MatButton,
    DatePipe,
    RouterLink
  ],
  templateUrl: './exam-card.component.html',
  styleUrl: './exam-card.component.css'
})
export class ExamCardComponent {
  @Input() profile!: Exam;
}
