export class LoginModel
{
    public username: string | undefined;
    public password: string | undefined;

    constructor(username: string, password: string)
    {
        this.username = username;
        this.password = password;
    }
}