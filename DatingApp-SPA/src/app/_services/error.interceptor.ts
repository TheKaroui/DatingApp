import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpEvent,
  HttpErrorResponse,
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept( req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
                catchError(
                error => {
                            if (error instanceof HttpErrorResponse) {
                                const applicationError = error.headers.get('Application-Error');
                                if (applicationError) {
                                    console.error(applicationError);
                                    return throwError(applicationError);
                                }
                                const serverError = error.error;
                                let modalStateErrors = '';
                                // if server eroor is type object then its model state error.
                                if (serverError && typeof serverError === 'object') {
                                    for (const key in serverError) {
                                        if (serverError[key]) {
                                            modalStateErrors += serverError[key] + '\n';
                                        }
                                    }
                                }
                                if (modalStateErrors) {
                                    return throwError(modalStateErrors);
                                } else if (serverError) {
                                    return throwError(serverError);
                                } else {
                                    return throwError(error.statusText);
                                }
                            }
                        }
                )
            );
  }
}

export const ErrorIntercepptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};
