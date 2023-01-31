import Mysql from "../core/mysql.js";
import Validation from "../core/validation.js";
import HashPassword from "../core/hashPassword.js";

class UserController
{
    static INPUT_NOT_FOUND = "Input is missing!";
    static PASSWORD_MATCH_ERROR = "Passwords do not match!";
    static INVALID_EMAIL = "Email is invalid!";
    static UPDATE_FAILED = "Database update has failed."
    static DELETE_FAILED = "Database deletion has failed."
    static DELETE_SUCCESS = "Database deletion was successful."
    static SUCCESS = "Update was successfull!";
    static OLD_PASSWORD_ERROR = "Old password do not match with the one in database.";

    static GetUserData(req, res, next)
    {
        next();
        
    }

    static PutUserData(req, res, next)
    {
        if (!res.success)
        {
            next();
            return;
        }

        
        console.log(req.body)
        

        const email = req.body.email;
        const name = req.body.name;
        const address = req.body.address;
        const isPasswordChanged = req.body.isPasswordChanged;
        const oldPassword = req.body.oldPassword;
        const newPassword1 = req.body.newPassword1;
        const newPassword2 = req.body.newPassword2;

        try
        {
            if (!name || !address || !email)
            {
                throw UserController.INPUT_NOT_FOUND;
                
            }
            
            if (email === '' || name === '' || address === '')
            {
                throw UserController.INPUT_NOT_FOUND;
            }

            if (!Validation.Email(email))
            {
                throw UserController.INVALID_EMAIL;
            }

            if (isPasswordChanged)
            {
                if (!oldPassword || !newPassword1 || !newPassword2)
                {
                    throw UserController.INPUT_NOT_FOUND;
                }

                if (oldPassword === '' || newPassword1 === '' || newPassword2 === '')
                {
                    throw UserController.INPUT_NOT_FOUND;
                }

                if (oldPassword !== HashPassword.Decrypt(process.env.SECRETKEY, res.password))
                {
                    console.log(res.password)
                    throw UserController.OLD_PASSWORD_ERROR;
                }

                if (newPassword1 !== newPassword2)
                {
                    throw UserController.PASSWORD_MATCH_ERROR;
                }
            }

            
        }
        catch(error)
        {
            res.success = false;
            res.message = error;
            next();
            return;
        }

        const query1 = "update users set name = ?, email = ?, address = ? where email = ?";
        const params1 = [name, email, address, res.userData.email];

        const query2 = "update users set name = ?, email = ?, address = ?, password = ? where email = ?";
        const params2 = [name, email, address, HashPassword.Encrypt(process.env.SECRETKEY, newPassword1), res.userData.email];

        

        const query = isPasswordChanged? query2 : query1;
        const params = isPasswordChanged? params2 : params1;
        
        console.log(query)
        console.log(params)
        

        Mysql.SQL(query, params, (error, results, fields) => {
            if (results)
            {   
                if (res.userData.email !== email || isPasswordChanged)
                {
                    res.logout = true;
                }
                
                res.message = UserController.SUCCESS;
                next();
            }
            else
            {
                res.success = false;
                res.message = UserController.UPDATE_FAILED;
                next();
            }
        })

        
    }

    static DeleteUser(req, res, next)
    {
        const query = "update users set active = ? where email = ?";
        const params = [0, res.userData.email];

        Mysql.SQL(query, params, (error, results, fields)=> {
            
            if (results)
            {
                res.logout = true;
                res.message = UserController.DELETE_SUCCESS;
                next();
            }
            else
            {
                res.success = false;
                res.message = UserController.DELETE_FAILED;
                next();
            }

        })
    }
}


export default UserController;