import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { TestErrorComponent } from './test-error/test-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { SectionHeaderComponent } from './section-header/section-header.component';
//import { ToastrModule } from 'ngx-toastr'; class 119



@NgModule({
  declarations: [
    NavBarComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent,
    SectionHeaderComponent
  ],
  imports:[
    CommonModule,
    RouterModule,
    // ToastrModule.forRoot({                            class 119
    //   positionClass:'toast-bottom-right',
    //   preventDuplicates: true
    // })
  ],
  exports:[
    NavBarComponent
  ]
})
export class CoreModule { }
