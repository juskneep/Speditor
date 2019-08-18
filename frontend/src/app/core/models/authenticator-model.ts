export class AuthenticatorModel {
    constructor(token, username, IsAdmin, IsAuthenticated){
        this.token = token;
        this.username = username;
        this.IsAdmin = IsAdmin;
        this.IsAuthenticated = IsAuthenticated;
    }

    public token: string;
    public username: string;
    public IsAdmin: boolean;
    public IsAuthenticated: boolean;
}
