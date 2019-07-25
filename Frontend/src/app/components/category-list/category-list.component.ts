import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {

  categories: any = [];

  constructor(private categoryService: CategoryService,
              private router: Router) { }

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.categoryService.getCategories().subscribe(
      (res: any ) => {
        if (res.transactionComplete) {
          this.categories = res.data;
        } else {
          console.log(res);
        }
      },
      err => console.log(err)
    );
  }

  deleteCategory(id: string) {
    this.categoryService.deleteCategory(id).subscribe(
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
