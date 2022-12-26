import Mysql from '../core/mysql.js';
// import crypto from 'crypto';
import HashPassword from '../core/hashPassword.js';

class LoginController
{
    static INPUT_NOT_FOUND = "Input is missing!";
    static WRONG_EMAIL_PASSWD = "Wrong email or password!";
    
    static Login(req, res, next)
    {
        res.success = true;
        res.message = '';
        res.token = '';

        const email = req.body.email;
        const password = req.body.password;

        try
        {
            if (!email | !password)
            {
                throw LoginController.INPUT_NOT_FOUND;
            }
        }
        catch (error)
        {
            res.success = false;
            res.message = error;
            next();
        }

        const query = "select * from users where email = ? and password = ?";

        Mysql.SQL(query, [email, HashPassword.Encrypt(process.env.SECRETKEY, password)], (error, results, fields)=> {
            
            if (results.length > 0)
            {
                res.token = HashPassword.Encrypt(process.env.SECRETKEY, results[0].email + '#' + results[0].password);
            }
            else
            {
                res.success = false;
                res.message = LoginController.WRONG_EMAIL_PASSWD;
            }

            next();
        })
        
    }

    
}

export default LoginController;