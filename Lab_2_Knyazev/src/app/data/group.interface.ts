import {Student} from './student.interface';

export interface Group {
  number: string;
  leaderID: number
  leader: Student;
}
