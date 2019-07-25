import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CategoryListComponent } from './components/category-list/category-list.component';
import { CategoryFormComponent } from './components/category-form/category-form.component';
import { AuthorFormComponent } from './components/author-form/author-form.component';
import { AuthorListComponent } from './components/author-list/author-list.component';
import { BookListComponent } from './components/book-list/book-list.component';
import { BookFormComponent} from './components/book-form/book-form.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/book',
    pathMatch: 'full'
  },
  {
    path: 'category',
    component: CategoryListComponent
  },
  {
    path: 'category/add',
    component: CategoryFormComponent
  },
  {
    path: 'category/edit/:id',
    component: CategoryFormComponent
  },
  {
    path: 'author',
    component: AuthorListComponent
  },
  {
    path: 'author/add',
    component: AuthorFormComponent
  },
  {
    path: 'author/edit/:id',
    component: AuthorFormComponent
  },
  {
    path: 'book',
    component: BookListComponent
  },
  {
    path: 'book/add',
    component: BookFormComponent
  },
  {
    path: 'book/edit/:id',
    component: BookFormComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
