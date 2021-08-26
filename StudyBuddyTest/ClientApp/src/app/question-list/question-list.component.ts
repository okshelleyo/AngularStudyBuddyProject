import { Component, OnInit } from '@angular/core';
import { Questions } from '../models/Questions';
import { QuestionApiService } from '../services/question-api.service';

@Component({
  selector: 'app-question-list',
  templateUrl: './question-list.component.html',
  styleUrls: ['./question-list.component.css']
})
export class QuestionListComponent implements OnInit {

  questionList: Questions[] = [];

  // showAnswer = false;
  selectedQ: Questions = { qId: 0, question:'', answer:''};

  constructor(private questionService: QuestionApiService) { }

  ngOnInit() {
    this.getQuestionsList();
  }

  getQuestionsList(){
  this.questionService.getAllQuestions().subscribe(
    result=> {
      this.questionList = result;
      console.log(this.questionList);
      console.log(this.questionList[3].answer)
    },
    error => console.log(error)
  ) }

    //on select - shows answer
    onSelect(question: Questions) : void {
      this.selectedQ = question;
    }


}
