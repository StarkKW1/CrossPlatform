import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Student} from '../data/student.interface';
import {map} from 'rxjs/operators';
import {StudentService} from './student.service';
import {Group} from '../data/group.interface';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  baseApiUrl: string = "http://localhost:5228/api/";
  http = inject(HttpClient);

  constructor(private studentService: StudentService) { }

  addGroup(group: Group) {

  }

  modifyGroup(group: Group) {

  }

  getGroups() {
    return this.http.get<Group[]>(`${this.baseApiUrl}Groups`).pipe(map((groups) => {
      console.log(groups);
      groups.forEach((group) => {
        this.studentService.getStudent(group.leaderID).subscribe(val => {
          group.leader = val
          console.log(group.leader);
        });
      })
      return groups;
    }));
  }

  getGroup(number: string) {
    return this.http.get<Group[]>(`${this.baseApiUrl}Groups/${number}`).pipe(map((groups) => {
      groups.forEach((group) => {
        this.studentService.getStudent(group.leaderID).subscribe(val => {
          group.leader = val
        });
      })
      return groups;
    }));
  }

  getStudents(number: string) {
    this.http.get(`${this.baseApiUrl}Groups/${number}/GetStudents`).pipe(map((groups) => {}));
  }

  deleteGroup(number: string) {
    this.http.delete(`${this.baseApiUrl}Groups/${number}`).pipe(map((groups) => {}));
  }
}
