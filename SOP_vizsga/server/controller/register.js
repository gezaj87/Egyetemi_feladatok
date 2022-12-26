import Mysql from '../core/mysql.js';
import Validation from '../core/validation.js';
import HashPassword from '../core/hashPassword.js';

class RegisterController
{
    static INPUT_NOT_FOUND = "Input is missing!";
    static PASSWORD_MATCH_ERROR = "Passwords do not match!";
    static INVALID_EMAIL = "Email is invalid!";
    static INSERT_FAILED = "Database insertion was not successful."
    static SUCCESS = "Registration was successfull!";

    static async Register(req, res, next)
    {

        res.success = true;
        res.message = '';

        const email = req.body.email;
        const password1 = req.body.password1;
        const password2 = req.body.password2;
        const name = req.body.name;
        const address = req.body.address;

        try
        {
        
            if (!email || !password1 || !password2 || !name || !address)
            {
                throw RegisterController.INPUT_NOT_FOUND;
            }

            if (email === '' || password1 === '' || password2 === '' || name === '' || address === '')
            {
                throw RegisterController.INPUT_NOT_FOUND;
            }

            if (!Validation.Email(email))
            {
                throw RegisterController.INVALID_EMAIL;
            }

            if (password1 !== password2)
            {
                throw RegisterController.PASSWORD_MATCH_ERROR;
            }

        }
        catch (error)
        {
            res.success = false;
            res.message = error;
            next();
        }
        
        const hashedPassword = HashPassword.Encrypt(process.env.SECRETKEY, password1);
        
        const query = "insert into users (email, password, name, address) values(?, ?, ?, ?)";
        Mysql.SQL(query, [email, hashedPassword, name, address], (error, results, fields) => {
            if (results)
            {
                res.message = RegisterController.SUCCESS;
                next();
            }
            else
            {
                res.success = false;
                res.message = RegisterController.INSERT_FAILED;
                next();
            }
        })

    }

}

export default RegisterController;