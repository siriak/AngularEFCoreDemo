import { Component } from '@angular/core';
import { BooksClient, Book } from '../clients';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'books',
  templateUrl: './books.component.html'
})
export class BooksComponent {
  public showDeleted: boolean;
  books: Book[];

  constructor(private client: BooksClient, private route: ActivatedRoute) {
    this.get();
  }

  update() {
    this.client.put(this.books).subscribe(
      () => {},
      () => {},
      () => this.get());
  }

  get() {
    this.route.params.subscribe(p => {
      const id = p['id'];
      if (id) {
        this.client.get(id).subscribe(
          result => this.books = [result],
          error => console.error(error));
      } else {
        this.client.getAll().subscribe(
          result => this.books = result,
          error => console.error(error));
      }
    });
  }

  getList() {
    return this.books.filter(book => this.showDeleted || !book.isDeleted)
  }
}
