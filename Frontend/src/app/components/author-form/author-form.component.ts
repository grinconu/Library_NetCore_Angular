import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Author } from '../../Models/Author';
import { AuthorService } from '../../services/author.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-author-form',
  templateUrl: './author-form.component.html',
  styleUrls: ['./author-form.component.css']
})
export class AuthorFormComponent implements OnInit {

  author: Author = {
    name: '',
    lastName: '',
    birthdate: new Date(),
  };

  birthdateFormat = this.datePipe.transform(this.author.birthdate, 'dd/MM/yyyy');

  edit = false;

  constructor(private authorService: AuthorService,
              private router: Router,
              private route: ActivatedRoute,
              private datePipe: DatePipe) {}

  ngOnInit() {
    const params = this.route.snapshot.params;
    if (params.id) {
      this.authorService.getAuthor(params.id).subscribe(
        (res: any) => {
          if (res.transactionComplete) {
            this.author.id = res.data[0].idAuthor;
            this.author.name = res.data[0].name;
            this.author.lastName = res.data[0].lastName;
            this.author.birthdate = res.data[0].birthdate;
            this.edit = true;
            this.birthdateFormat = this.datePipe.transform(this.author.birthdate, 'dd/MM/yyyy');
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

  saveInfo() {
    this.author.birthdate = this.parse(this.birthdateFormat);
    this.authorService.saveAuthor(this.author).subscribe(
      (res: any) => {
        console.log(res);
        if (res.transactionComplete) {
          this.router.navigate(['/author']);
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
    this.author.birthdate = this.parse(this.birthdateFormat);
    this.authorService.updateAuthor(this.author.id.toString(), this.author).subscribe(
      (res: any) => {
        console.log(res);
        if (res.transactionComplete) {
          this.router.navigate(['/author']);
        } else {
          console.log(res);
        }
      },
      err => {
        console.log(err);
      }
    );
  }

  parse(value: any): Date | null {
    if ((typeof value === 'string') && (value.indexOf('/') > -1)) {
      const str = value.split('/');

      const year = Number(str[0]);
      const month = Number(str[1]) - 1;
      const date = Number(str[2]);

      console.log(str);

      return new Date(year, month, date);
    } else if ((typeof value === 'string') && value === '') {
      return new Date();
    }
    const timestamp = typeof value === 'number' ? value : Date.parse(value);
    return isNaN(timestamp) ? null : new Date(timestamp);
  }
}
