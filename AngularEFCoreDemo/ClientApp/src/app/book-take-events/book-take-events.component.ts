import { Component } from '@angular/core';
import { BookTakeEvent, BookTakeEventsClient, ReaderTicket, Person, Book, PeopleClient, ReaderTicketsClient, BooksClient } from '../clients';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'book-take-events',
  templateUrl: './book-take-events.component.html',
  styles: ['td { white-space: nowrap; }'],
})
export class BookTakeEventsComponent {
  public showDeleted: boolean;
  bookTakeEvents: BookTakeEvent[];
  librarians: Person[];
  readerTickets: ReaderTicket[];
  books: Book[];

  constructor(
    private client: BookTakeEventsClient,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    peopleClient: PeopleClient,
    readerTicketsClient: ReaderTicketsClient,
    booksClient: BooksClient,
    ) {
    this.get();
    peopleClient.getLibrarians().subscribe(
      result => this.librarians = result,
      error => console.error(error));
    readerTicketsClient.getAll().subscribe(
      result => this.readerTickets = result,
      error => console.error(error));
    booksClient.getAll().subscribe(
      result => this.books = result,
      error => console.error(error));
  }

  update() {
    this.client.put(this.bookTakeEvents).subscribe(
      () => {},
      () => {},
      () => this.get());
  }

  get() {
    this.route.params.subscribe(p => {
      const id = p['id'];
      if (id) {
        this.client.get(id).subscribe(
          result => this.bookTakeEvents = [result],
          error => console.error(error));
      } else {
        this.client.getAll().subscribe(
          result => this.bookTakeEvents = result,
          error => console.error(error));
      }
    });
  }

  getList() {
		if (this.bookTakeEvents) {
		  return this.bookTakeEvents.filter(bookTakeEvent => this.showDeleted || !bookTakeEvent.isDeleted);
		}
  }

  parseDate(dateString: string): Date {
    if (dateString) {
        return new Date(dateString);
    } else {
        return null;
    }
  }

  entryDeleted(isDeleted: boolean, bookTakeEvent: BookTakeEvent) {
		if (isDeleted) {
      const dialogRef = this.dialog.open(ConfirmDialogComponent);
      dialogRef.afterClosed().subscribe(res => {
        if (res !== 'yes') {
          bookTakeEvent.isDeleted = false;
        }
      });
		}
  }
}
