import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BooksComponent } from './books/books.component';
import { BooksClient, PeopleClient, ReaderTicketsClient, AuthorityEntriesClient, BookTakeEventsClient, SectionsClient, BookEditionsClient } from './clients';
import { PeopleComponent } from './people/people.component';
import { ReaderTicketsComponent } from './reader-tickets/reader-tickets.component';
import { SectionsComponent } from './sections/sections.component';
import { AuthorityEntriesComponent } from './authority-entries/authority-entries.component';
import { BookTakeEventsComponent } from './book-take-events/book-take-events.component';
import { BookEditionsComponent } from './book-editions/book-editions.component';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    BooksComponent,
    PeopleComponent,
    ReaderTicketsComponent,
    AuthorityEntriesComponent,
    BookTakeEventsComponent,
    SectionsComponent,
    BookEditionsComponent,
    ConfirmDialogComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatDialogModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'books', component: BooksComponent },
      { path: 'books/:id', component: BooksComponent },
      { path: 'people', component: PeopleComponent },
      { path: 'people/:id', component: PeopleComponent },
      { path: 'reader-tickets', component: ReaderTicketsComponent },
      { path: 'reader-tickets/:id', component: ReaderTicketsComponent },
      { path: 'authority-entries', component: AuthorityEntriesComponent },
      { path: 'authority-entries/:id', component: AuthorityEntriesComponent },
      { path: 'book-take-events', component: BookTakeEventsComponent },
      { path: 'book-take-events/:id', component: BookTakeEventsComponent },
      { path: 'sections', component: SectionsComponent },
      { path: 'sections/:id', component: SectionsComponent },
      { path: 'book-editions', component: BookEditionsComponent },
      { path: 'book-editions/:id', component: BookEditionsComponent },
    ])
  ],
  providers: [
    BooksClient,
    PeopleClient,
    ReaderTicketsClient,
    AuthorityEntriesClient,
    BookTakeEventsClient,
    SectionsClient,
    BookEditionsClient,
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: { hasBackdrop: true },
    }
  ],
  bootstrap: [
    AppComponent,
  ],
  entryComponents: [
    ConfirmDialogComponent,
  ],
})
export class AppModule { }
