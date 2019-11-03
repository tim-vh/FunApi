import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MediaList } from './media/media-list.component';
import { MediaDetails } from './media/media-details.component';
import { MediaDetailsGuardService } from './media/media-details-guard.service';


const routes: Routes = [
    { path: 'home', component: MediaList },
    { path: 'details/:fileName', canActivate: [MediaDetailsGuardService], component: MediaDetails },
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: '**', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
