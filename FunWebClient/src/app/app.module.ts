import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Injector } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { createCustomElement } from '@angular/elements';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MediaList } from './media/media-list.component';
import { HttpClientModule } from '@angular/common/http';
import { FunApiProvider } from './providers/fun-api.provider';
import { FormsModule } from '@angular/forms';
import { MediaDetails } from './media/media-details.component';
import { MediaDetailsGuardService } from './media/media-details-guard.service';

@NgModule({
  declarations: [
    AppComponent,
    MediaList,
    MediaDetails
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    FormsModule
  ],
  providers: [FunApiProvider, MediaDetailsGuardService],
  bootstrap: [AppComponent],
  //entryComponents: [AppComponent, MediaDetails]
})
export class AppModule {
  constructor(private injector: Injector)
  {

  }

  ngDoBootstrap() {
    // const app = createCustomElement(AppComponent, {
    //   injector: this.injector, 
    // });

    // customElements.define('app-root', app);

    // const mediaDetails = createCustomElement(MediaDetails, {
    //   injector: this.injector
    // });

    // customElements.define('media-details', mediaDetails);
  }
}