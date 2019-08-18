import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ForumModel } from '../../components/forum/models/ForumModel';
import { FormBuilder, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  formModel = this.formBuilder.group({
    Title: ['', Validators.required],
    Description: ['', Validators.required],
    Created: [Date, Validators.required],
    ImageUrl: ['', Validators.required]
  });

  baseUrl = 'https://localhost:44345/api';
  token = localStorage.getItem('JWT_TOKEN');

  constructor(private http: HttpClient, private formBuilder: FormBuilder) { }

  async getAllPosts() {
    return await this.http.get<ForumModel[]>(`${this.baseUrl}/forum`);
  }

  createPost() {
    const body = {
      Title: this.formModel.value.Title,
      Description: this.formModel.value.Description,
      Created: this.formModel.value.Created,
      ImageUrl: this.formModel.value.ImageUrl
    };
    const header = new HttpHeaders().set('Content-Type', 'application/json').set('Authorization', 'Bearer ' + this.token);
    return this.http.post<ForumModel>(`${this.baseUrl}/forum/create`, body, { headers: header });
  }
}
