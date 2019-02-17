import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BooksClient, Book } from '../clients';

@Component({
  selector: 'books',
  templateUrl: './books.component.html'
})
export class BooksComponent {
  public books: Book[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    var client = new BooksClient(http, baseUrl);
    client.getAll().subscribe(result => {
      this.books = result;
    }, error => console.error(error));
  }
}
