import { MemberListResolver } from './../app/_resolvers/member-list.resolver';
import { MemberDetailResolver } from './../app/_resolvers/member-detail.resolver';
import { MemberListComponent } from './../app/members/member-list/member-list.component';
import { MessagesComponent } from './../app/messages/messages.component';
import { MemberDetailComponent } from './../app/members/member-detail/member-detail.component';
import {Routes} from '@angular/router';
import { HomeComponent } from 'src/app/home/home.component';


import { ListsComponent } from 'src/app/lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';


export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'members', component: MemberListComponent,
                              resolve: {users: MemberListResolver}},
            {path: 'members/:id', component: MemberDetailComponent,
                                  resolve: {user: MemberDetailResolver}},
            {path: 'messages', component: MessagesComponent},
            {path: 'lists', component: ListsComponent},
        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
