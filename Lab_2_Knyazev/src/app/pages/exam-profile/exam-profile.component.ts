import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  inject,
  model,
  signal,
  SimpleChanges
} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ExamService} from '../../services/exam.service';
import { Exam } from '../../data/exam.interface';
import {Student} from '../../data/student.interface';
import {MatButton, MatButtonModule} from '@angular/material/button';
import {FormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import {MatOption, MatSelect} from '@angular/material/select';
import {StudentService} from '../../services/student.service';
import {catchError, tap} from 'rxjs/operators';
import {BehaviorSubject, throwError} from 'rxjs';

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

export interface DialogData {
  examCode: number;
}

@Component({
  selector: 'app-exam-profile',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatButton
  ],
  templateUrl: './exam-profile.component.html',
  styleUrl: './exam-profile.component.css',
  changeDetection: ChangeDetectionStrategy.Default,
})

export class ExamProfileComponent {
  readonly dialog = inject(MatDialog);
  readonly router =  inject(Router);
  ID!: number;
  exam!: Exam;
  students: Student[] = [];
  failStudents: Student[] = [];

  getData() {
    this.examService.getExam(this.ID).subscribe(val => {
      this.exam = val;
    });
    this.examService.getStudents(this.ID).subscribe(val => {
      this.students = val;
    })
    this.examService.getStudentsWithFails(this.ID).subscribe(val => {
      this.failStudents = val;
    })
  }

  constructor(private route: ActivatedRoute, private examService: ExamService, private studentService: StudentService) {
    this.ID = route.snapshot.params['id'];
    this.getData();
  }

  addStudent(): void {
    const dialogRef = this.dialog.open(AddStudentDialog, {
      data: {student: this.exam},
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        this.studentService.addOnExam(result, [this.route.snapshot.params["id"]]).subscribe(() => this.getData());
      }
    });
  }

  removeStudent(id: number) {
    this.studentService.removeFromExam(id, [this.route.snapshot.params["id"]]).subscribe(() => this.getData());
  }

  updateExam() {

  }

  deleteExam() {
    this.examService.delete(this.ID).subscribe(() => this.getData());
  }
}

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

interface DialogStudents {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'exam-profile-dialog',
  templateUrl: 'exam-profile__add-student-dialog.html',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    MatSelect,
    MatOption,
  ],
})

export class AddStudentDialog {
  readonly data = inject<DialogData>(MAT_DIALOG_DATA);

  students: DialogStudents[] = [];
  student!: Student;

  constructor(private studentService: StudentService) {
    this.studentService.getStudents()
      .subscribe(val => {
        val.forEach((student) => {
          this.students.push({value: student.id, viewValue: `${student.name} ${student.surname}`});
        });
      });
  }
}
