import { Component } from '@angular/core';
import { ReaderTicket, ReaderTicketsClient } from '../clients';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'reader-tickets',
  templateUrl: './reader-tickets.component.html'
})
export class ReaderTicketsComponent {
  public showDeleted: boolean;
  readerTickets: ReaderTicket[];

  constructor(private client: ReaderTicketsClient, private route: ActivatedRoute) {
    this.get();
  }

  update() {
    this.client.put(this.readerTickets).subscribe(
      () => {},
      () => {},
      () => this.get());
  }

  get() {
    this.route.params.subscribe(p => {
      const id = p['id'];
      if (id) {
        this.client.get(id).subscribe(
          result => this.readerTickets = [result],
          error => console.error(error));
      } else {
        this.client.getAll().subscribe(
          result => this.readerTickets = result,
          error => console.error(error));
      }
    });
  }

  getList() {
    return this.readerTickets.filter(readerTicket => this.showDeleted || !readerTicket.isDeleted)
  }
  
  parseDate(dateString: string): Date {
    if (dateString) {
        return new Date(dateString);
    } else {
        return null;
    }
  }
}
