import { Component } from '@angular/core';
import { BooksClient, Book, BookEdition, BookEditionsClient, Section, SectionsClient } from '../clients';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'books',
  templateUrl: './books.component.html',
  styles: ['td { white-space: nowrap; }'],
})
export class BooksComponent {
  public showDeleted: boolean;
  books: Book[];
  bookEditions: BookEdition[];
  sections: Section[];

  constructor(
    private client: BooksClient,
    private bookEditionsClient: BookEditionsClient,
    private sectionsClient: SectionsClient,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    ) {
    this.get();
    this.bookEditionsClient.getAll().subscribe(result => this.bookEditions = result);
    this.sectionsClient.getAll().subscribe(result => this.sections = result);
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
    if (this.books) {
	  return this.books.filter(book => this.showDeleted || !book.isDeleted);
    }
  }

  entryDeleted(isDeleted: boolean, book: Book) {
		if (isDeleted) {
      const dialogRef = this.dialog.open(ConfirmDialogComponent);
      dialogRef.afterClosed().subscribe(res => {
        if (res !== 'yes') {
          book.isDeleted = false;
        }
      });
		}
  }
}
