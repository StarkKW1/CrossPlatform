import {HttpInterceptor, HttpInterceptorFn, HttpRequest} from '@angular/common/http';

export const authTokenInterceptor: HttpInterceptorFn = (req, next) => {
  req = req.clone({
    setHeaders: {
      authorization: 'Bearer ' + localStorage.getItem("token"),
    }
  })
  return next(req)
}
