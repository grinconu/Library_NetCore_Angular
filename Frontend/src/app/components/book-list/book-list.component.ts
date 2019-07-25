import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookService } from '../../services/book.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  books: any = [];

    constructor(private bookService: BookService,
                private router: Router) { }

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.bookService.getBooks().subscribe(
      (res: any ) => {
        if (res.transactionComplete) {
          this.books = res.data;
        } else {
          console.log(res);
        }
      },
      err => console.log(err)
    );
  }

  deleteBook(id: string) {
    this.bookService.deleteBook(id).subscribe(
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
