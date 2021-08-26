import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Questions } from '../models/Questions';

@Injectable({
  providedIn: 'root'
})
export class QuestionApiService {

  apiUri: string = "https://localhost:44343/api/studybuddy";


  constructor(private http: HttpClient) {

    console.log(this.apiUri)
  }

  getAllQuestions() {
   return this.http.get<Questions[]>(`${this.apiUri}/getallquestions`);
  }

  getQuestionById(id: number) {
   return this.http.get<Questions>(`${this.apiUri}/${id}`);
  }

  deleteQuestion(id: number) {
    return this.http.delete(`${this.apiUri}/deletequestion/${id}`);
  }

  addQuestion(question: Questions){
    return this.http.post<Questions>(`${this.apiUri}/addquestion`, {"question":question.question, "answer":question.answer});
  }
}
