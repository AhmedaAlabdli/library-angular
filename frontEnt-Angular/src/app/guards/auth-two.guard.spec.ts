import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { authTwoGuard } from './auth-two.guard';

describe('authTwoGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => authTwoGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
