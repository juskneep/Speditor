import { Component, OnInit } from '@angular/core';
import { ForumModel } from './models/ForumModel';
import { ForumService } from '../services/forum.service';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit {



  constructor(
    private forumService: ForumService
  ) { }

  forumThemes: ForumModel[] = [];

  async ngOnInit() {
    //this.forumThemes = await this.forumService.getAllPosts();
  }

  onSubmit() {
    this.forumService.createPost().subscribe(
      (req: any) => {
        if (req) { console.log(req); }
      },
      (err: any) => {
        if (err) { console.log({"Fuck error": err}); }
      });
  }
}
