import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Questions } from '../models/Questions';
import { QuestionApiService } from '../services/question-api.service';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.css']
})
export class AddQuestionComponent implements OnInit {

  QandA: Questions = { qId: 0, question:'', answer:''}

  constructor(private questionAPI: QuestionApiService) { }

  ngOnInit() {
  }

  onSubmit(form: NgForm) {
    this.QandA = form.form.value;
    console.log(this.QandA);
    this.addQandA(this.QandA);
  }

  addQandA(qa: Questions): void {
    this.questionAPI.addQuestion(qa).subscribe(
      result => {
        console.log(result);
      },
      error => console.log(error)
    );
  }

}
