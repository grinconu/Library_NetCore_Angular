import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { Book } from '../../Models/Book';
import { BookService } from '../../services/book.service';
import { AuthorService } from '../../services/author.service';
import { CategoryService } from '../../services/category.service';


@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css']
})
export class BookFormComponent implements OnInit {

  book: Book = {
    name: '',
    idAuthor: 0,
    idCategory: 0,
    isbn: ''
  };
  myControl = new FormControl();
  authorData: any;
  categoryData: any;
  edit = false;
  keyword = 'name';
  initialValueAuthor: '';
  initialValueCategory: '';

  constructor(private bookService: BookService,
              private authorService: AuthorService,
              private categoryService: CategoryService,
              private router: Router,
              private route: ActivatedRoute) {}

  ngOnInit() {
    this.loadData();

    const params = this.route.snapshot.params;
    if (params.id) {
      this.bookService.getBook(params.id).subscribe(
        (res: any) => {
          if (res.transactionComplete) {
            this.book.id = res.data[0].idBook;
            this.book.name = res.data[0].name;
            this.book.idAuthor = res.data[0].idAuthor;
            this.book.idCategory = res.data[0].idCategory;
            this.book.isbn = res.data[0].isbn;
            this.edit = true;
            this.authorData.forEach(author => {
              if (author.idAuthor === this.book.idAuthor) {
                this.initialValueAuthor = author.name;
              }
            });
            this.initialValueCategory = res.data[0].idCategory;
          } else {
            console.log(res);
          }
        },
        err => {
          console.log(err);
        }
      );
    }
  }

  loadData() {
    this.authorService.getAuthors().subscribe(
      (res: any) => {
        if (res.transactionComplete) {
            this.authorData = res.data;
            this.authorData.forEach(author => {
              author.name = author.name + ' ' + author.lastName;
            });
        }
      },
      err => {
        console.log(err);
      }
    );

    this.categoryService.getCategories().subscribe(
      (res: any) => {
        if (res.transactionComplete) {
            this.categoryData = res.data;
        }
      },
      err => {
        console.log(err);
      }
    )
  }

  selectAuthor(item) {
    this.book.idAuthor = item.idAuthor;
  }

  selectCategory(item) {
    this.book.idCategory = item.idCategory;
  }

  saveInfo() {
    this.bookService.saveBook(this.book).subscribe(
      (res: any) => {
        console.log(res);
        if (res.transactionComplete) {
          this.router.navigate(['/book']);
        } else {
          console.log(res);
        }
      },
      err => {
        console.log(err);
      }
    );
  }

  updateInfo() {
    this.bookService.updateBook(this.book.id.toString(), this.book).subscribe(
      (res: any) => {
        console.log(res);
        if (res.transactionComplete) {
          this.router.navigate(['/book']);
        } else {
          console.log(res);
        }
      },
      err => {
        console.log(err);
      }
    );
  }

}
