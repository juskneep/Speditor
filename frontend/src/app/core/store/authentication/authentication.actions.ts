import { Action } from '@ngrx/store'
import { AuthenticatorModel } from '../../models/authenticator-model';

export const AUTHENTICATE = '[AUTHENTICATION] AUTHENTICATE'
export const DEAUTHENTICATE = '[AUTHENTICATION] DEAUTHENTICATE'

export class Authenticate implements Action {
  readonly type: string = AUTHENTICATE

  constructor(public payload: AuthenticatorModel) { }
}

export class Deauthenticate implements Action {
  readonly type: string = DEAUTHENTICATE
}
