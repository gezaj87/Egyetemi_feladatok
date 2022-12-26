class Validation
{
    static INPUT_NOT_FOUND = "Input is missing!";
    static PASSWORD_MATCH_ERROR = "Passwords do not match!";
    static INVALID_EMAIL = "Email is invalid!";
    static INSERT_FAILED = "Database insertion was not successful."
    static SUCCESS = "Registration was successfull!";

    static Email(input)
    {
        var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
      
        if (input.match(validRegex)) {
      
            return true;
      
        } else {
      
            return false;
        }
    }
}

export default Validation;