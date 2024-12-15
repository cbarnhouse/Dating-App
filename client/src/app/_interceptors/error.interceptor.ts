import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router)
  const toasterService = inject(ToastrService);

  return next(req).pipe(
    catchError((httpErrorResponse: HttpErrorResponse) => {

        switch (httpErrorResponse.status) 
        {
          //400s can be caused by validation errors or generic BadRequests.
          case 400:
            var validationErrors = httpErrorResponse.error.errors;
            
            //If it's a validation error, there is an errors array. Grab all of them and throw them. 
            if (validationErrors) 
            {
              const errorMessagesArray = [];
              for (const key in validationErrors) 
              {
                errorMessagesArray.push(validationErrors[key])
              }

              throw errorMessagesArray.flat(); 
            }

            //if it's not a validation error, just toast the single error message
            else 
            {
              toasterService.error(httpErrorResponse.error, httpErrorResponse.status.toString())
            }
            break;

          case 401:
            toasterService.error("Unauthorized", httpErrorResponse.status.toString());
            break;

          case 404:
            router.navigateByUrl("/not-found");
            break;

          case 500:
            const navigationExtras: NavigationExtras = {state: {error: httpErrorResponse.error}};
            router.navigateByUrl("server-error", navigationExtras);
            break;

          default:
            toasterService.error("Something unexpected went wrong");
            break;
        }

        throw httpErrorResponse;
    })
  )
};
