import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorService } from '../../services/author.service';

@Component({
  selector: 'app-author-list',
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.css']
})
export class AuthorListComponent implements OnInit {
  authors: any = [];

  constructor(private authorService: AuthorService,
              private router: Router) { }

  ngOnInit() {
    this.getData();
  }
  getData() {
    this.authorService.getAuthors().subscribe(
      (res: any ) => {
        if (res.transactionComplete) {
          this.authors = res.data;
        } else {
          console.log(res);
        }
      },
      err => console.log(err)
    );
  }

  deleteAuthor(id: string) {
    this.authorService.deleteAuthor(id).subscribe(
      (res: any ) => {
        if (res.transactionComplete) {
          this.getData();
        } else {
          console.log(res);
        }
      },
      err => console.log(err)
    );
  }
}
