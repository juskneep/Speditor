import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
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
  recentForums: ForumModel[] = [];

  async ngOnInit() {
    this.forumThemes = await this.forumService.getAllPosts();
    this.recentForums = this.forumThemes.sort(function (date1, date2) {
      if (date1 > date2) return 1;
      if (date1 < date2) return -1;
      return 0;
    }).slice(0, 3);
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
