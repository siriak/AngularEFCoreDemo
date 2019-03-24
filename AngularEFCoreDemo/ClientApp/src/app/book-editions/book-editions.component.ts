import { Component } from '@angular/core';
import { BookEdition, BookEditionsClient } from '../clients';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'book-editions',
  templateUrl: './book-editions.component.html',
  styles: ['td { white-space: nowrap; }'],
})
export class BookEditionsComponent {
  public showDeleted: boolean;
  bookEditions: BookEdition[];

  constructor(private client: BookEditionsClient, private route: ActivatedRoute) {
    this.get();
  }

  update() {
    this.client.put(this.bookEditions).subscribe(
      () => {},
      () => {},
      () => this.get());
  }

  get() {
    this.route.params.subscribe(p => {
      const id = p['id'];
      if (id) {
        this.client.get(id).subscribe(
          result => this.bookEditions = [result],
          error => console.error(error));
      } else {
        this.client.getAll().subscribe(
          result => this.bookEditions = result,
          error => console.error(error));
      }
    });
  }

  getList() {
		if (this.bookEditions) {
		  return this.bookEditions.filter(bookEdition => this.showDeleted || !bookEdition.isDeleted);
		}
  }
}
