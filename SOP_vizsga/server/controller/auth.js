import Mysql from '../core/mysql.js';
import HashPassword from '../core/hashPassword.js';

class AuthController
{
    static AUTH_ERROR = "Email and password do not match!";
    static WRONG_TOKEN = "Token is not valid!";
    static TOKEN_NOT_FOUND = "Token is not found!";

    static Auth(req, res, next)
    {

        const token = req.body.token;
        res.success = true;
        res.message = '';


        if (token)
        {
            
            try
            {
                const decryptedToken = HashPassword.Decrypt(process.env.SECRETKEY, token);
                const temp = decryptedToken.split('#');
                

                if (temp.length === 2)
                {
                    const email = temp[0];
                    const password = temp[1];

                    const query = "select email, name, address, password, u_id from users where email = ? and password = ? and active = ?";
                    Mysql.SQL(query, [email, password, 1], (error, results, fields) => {
                        
                        if (results.length === 0)
                        {
                            res.success = false;
                            res.message = AuthController.AUTH_ERROR;
                            next()
                        }
                        else
                        {
                            //success!
                            res.userData = {
                                email: results[0].email,
                                name: results[0].name,
                                address: results[0].address
                            };
                            res.password = results[0].password;
                            res.userID = results[0].u_id;
                            next();
                        }                        

                    })
                }
                else
                {
                    res.success = false;
                    res.message = AuthController.WRONG_TOKEN;
                    next()
                }

            }
            catch(error)
            {
                res.success = false;
                res.message = AuthController.WRONG_TOKEN;
                next();
            }
            
        }
        else
        {
            res.success = false;
            res.message = AuthController.TOKEN_NOT_FOUND;
            next();
        }

        // return next(response);

    }

}

export default AuthController;