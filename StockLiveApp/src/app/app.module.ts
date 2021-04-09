import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgToggleModule } from 'ngx-toggle-button';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainComponent } from './main/main.component';
import { ProductComponent } from './main/product/product.component';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    ProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgToggleModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
