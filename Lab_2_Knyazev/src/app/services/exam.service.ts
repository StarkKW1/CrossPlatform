import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Exam} from '../data/exam.interface';
import {map} from 'rxjs/operators';
import {Student} from '../data/student.interface';

@Injectable({
  providedIn: 'root'
})
export class ExamService {
  baseApiUrl: string = "http://localhost:5228/api/";
  http = inject(HttpClient);

  constructor() { }

  getExams() {
    return this.http.get<Exam[]>(`${this.baseApiUrl}Exams`)
  }

  getExam(id: number) {
    return this.http.get<Exam>(`${this.baseApiUrl}Exams/${id}`).pipe(map(exam => {
      return exam;
    }));
  }

  getStudents(id: number) {
    return this.http.get<Student[]>(`${this.baseApiUrl}Exams/${id}/GetStudents`).pipe(map(students => {
      return students;
    }));
  }

  getStudentsWithFails(id: number) {
    return this.http.get<Student[]>(`${this.baseApiUrl}Exams/${id}/GetStudentsWithFail`).pipe(map(students => {
      return students;
    }));
  }

  delete(id: number) {
    return this.http.delete(`${this.baseApiUrl}Exams/${id}`);
  }
}
