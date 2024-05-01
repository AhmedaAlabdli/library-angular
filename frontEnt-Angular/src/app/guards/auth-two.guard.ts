import { CanActivateFn } from '@angular/router';

export const authTwoGuard: CanActivateFn = (route, state) => {
  return true;
};
