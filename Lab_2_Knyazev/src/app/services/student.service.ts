import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Student, mark} from '../data/student.interface';
import {catchError, map} from 'rxjs/operators';
import {from, Observable, throwError} from 'rxjs';
import {Exam} from '../data/exam.interface';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  baseApiUrl: string = "http://localhost:5228/api/";
  http = inject(HttpClient);

  constructor() {  }

  getStudents() {
    return this.http.get<Student[]>(`${this.baseApiUrl}Students/`).pipe(map((students) => {
      students.forEach((student) => {
        this.http.get<number>(`${this.baseApiUrl}Students/${student.id}/GetAvrMark`).subscribe(val => {
          student.avrMark = val
        });
        this.http.get<number>(`${this.baseApiUrl}Students/${student.id}/GetFails`).subscribe(val => {
          student.failsCount = val
        });
        /*this.http.get<Exam[]>(`${this.baseApiUrl}Students/${student.id}/GetExams`).subscribe(val => {
          student.exams = val
        });
        this.http.get<Map<string, number>>(`${this.baseApiUrl}Students/${student.id}/GetMarks`).subscribe(val => {
          student.marks = val
        });*/
      })
      return students;
    }));
  }

  getStudent(id: number) {
    return this.http.get<Student>(`${this.baseApiUrl}Students/${id}`).pipe(map((student) => {
      this.http.get<number>(`${this.baseApiUrl}Students/${id}/GetAvrMark`).subscribe(val => {
        student.avrMark = val
      });
      this.http.get<number>(`${this.baseApiUrl}Students/${id}/GetFails`).subscribe(val => {
        student.failsCount = val
      });
      this.http.get<Exam[]>(`${this.baseApiUrl}Students/${id}/GetExams`).subscribe(val => {
        student.exams = val
      });
      this.http.get<mark[]>(`${this.baseApiUrl}Students/${id}/GetMarksTest`).subscribe(val => {
        student.marks = val;
      });
      return student;
    }));
  }

  getAvrMark(id: number) {
    return this.http.get<number>(`${this.baseApiUrl}Students/${id}/GetAvrMark`);
  }

  getMarks(id: number) {
    return this.http.get<mark[]>(`${this.baseApiUrl}Students/${id}/GetMarksTest`);
  }

  getFails(id: number) {
    return this.http.get<number>(`${this.baseApiUrl}Students/${id}/GetFails`);
  }

  getExams(id: number) {
    return this.http.get<Exam[]>(`${this.baseApiUrl}Students/${id}/GetExams`);
  }

  /*setMarks(id: number, current: boolean) {
    return this.http.post(`${this.baseApiUrl}Students/${id}/SetMarks`, { })
  }*/

  addOnExam(id: number, ExamsID: number[]) {
    return this.http.put(`${this.baseApiUrl}Students/${id}/AddToExam`, ExamsID);
  }

  removeFromExam(id: number, ExamsID: number[]) {
    return this.http.put(`${this.baseApiUrl}Students/${id}/RemoveFromExam`, ExamsID);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseApiUrl}Students/${id}`);
  }
}
