import {Exam} from './exam.interface';

export interface mark {
  subject: string;
  value: number;
}

export interface Student {
  id: number;
  name: string;
  surname: string;
  groupNumber: string;
  avrMark: number;
  marks: Array<mark>;
  failsCount: number;
  exams: Array<Exam>;

  /*id: number = 0;
  name: string = "";
  surname: string = "";
  groupNumber: string = "";
  avrMark: number = 0;
  failsCount: number = 0;

  constructor(id: number, name: string, surname: string, groupNumber: string, avrMark: number, failsCount: number) {
    this.id = id;
    this.name = name;
    this.surname = surname;
    this.groupNumber = groupNumber;
    this.avrMark = avrMark;
    this.failsCount = failsCount;
  }*/
}
