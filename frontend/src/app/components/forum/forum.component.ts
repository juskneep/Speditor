import { Component, OnInit } from '@angular/core';
import { ForumModel } from './models/ForumModel';
import { ForumService } from '../../core/services/forum.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit {



  constructor(
    private forumService: ForumService,
    private toastrService: ToastrService
  ) { }

  forumThemes: ForumModel[] = [];

  async ngOnInit() {
    this.forumThemes = await this.forumService.getAllPosts();
    console.log(this.forumThemes);
  }

  onSubmit() {
    console.log(this.forumThemes);
    this.forumService.createPost().subscribe(
      (req: any) => {
        this.toastrService.success('Successfully created forum');
      },
      (err: any) => {
        console.log(err);
        this.toastrService.error('Forum was not created by reasons');
      });
  }

  getForums() {
    this.forumService.getAllPosts().then(x => console.log(x));
  }

}
