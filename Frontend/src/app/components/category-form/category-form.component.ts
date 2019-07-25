import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Category } from '../../Models/Category';

import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent implements OnInit {

  category: Category = {
    name: '',
    description: ''
  };

  edit = false;

  constructor(private categoryService: CategoryService,
              private router: Router,
              private route: ActivatedRoute) {}

  ngOnInit() {
    const params = this.route.snapshot.params;
    if (params.id) {
      this.categoryService.getCategory(params.id).subscribe(
        (res: any) => {
          if (res.transactionComplete) {
            this.category.id = res.data[0].idCategory;
            this.category.name = res.data[0].name;
            this.category.description = res.data[0].description;
            this.edit = true;
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
    this.categoryService.saveCategory(this.category).subscribe(
      (res: any) => {
        console.log(res);
        if (res.transactionComplete) {
          this.router.navigate(['/category']);
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
    this.categoryService.updateategory(this.category.id.toString(), this.category).subscribe(
      (res: any) => {
        console.log(res);
        if (res.transactionComplete) {
          this.router.navigate(['/category']);
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
