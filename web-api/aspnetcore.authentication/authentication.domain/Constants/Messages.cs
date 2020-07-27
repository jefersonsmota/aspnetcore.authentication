namespace authentication.domain.Constants
{
    public static class Messages
    {
        public const string SING_IN = "Sing-in success";
        public const string CREATED_SUCCESS = "Item created with success";
        public const string NOT_FOUND_USER = "User not found";
        public const string INVALID_FIELDS = "Invalid Fiels";
        public const string MISSING_FIELDS = "Missing fields";
        public const string EMAIL_ALREADY_EXISTS = "E-mail already exists";
        public const string INVALID_EMAIL_OR_PASSWORD = "Invalid e-mail or password";
    }

    public static class Templates
    {
        public const string FORGOT_PASSWORD_SUBJECT = "Forgot my password";

        public const string FORGOT_PASSWORD_BODY = @"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset = ""UTF-8"" >
                < meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Forgot Password</title>
            </head>
            <body>
                <h3>Olá, {EMAIL}!</h3>

                <p>Você solicitou a mudança da sua senha.</p> 
                <p>Clique no link a baixo para efetuar a mudança. </p>
                <p>Caso não tenha feito a solicitação, fique tranquilo, desconsidere esse email.</p>

                <a href=""{URL}""> Mudar Senha!</a>
            </body>
            </html>
        ";
    }
}
