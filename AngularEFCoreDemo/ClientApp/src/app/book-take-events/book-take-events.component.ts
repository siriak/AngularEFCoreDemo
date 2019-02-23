import { Component } from '@angular/core';
import { BookTakeEvent, BookTakeEventsClient } from '../clients';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'book-take-events',
  templateUrl: './book-take-events.component.html'
})
export class BookTakeEventsComponent {
  public showDeleted: boolean;
  bookTakeEvents: BookTakeEvent[];

  constructor(private client: BookTakeEventsClient, private route: ActivatedRoute) {
    this.get();
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
    return this.bookTakeEvents.filter(bookTakeEvent => this.showDeleted || !bookTakeEvent.isDeleted)
  }
  
  parseDate(dateString: string): Date {
    if (dateString) {
        return new Date(dateString);
    } else {
        return null;
    }
  }
}
