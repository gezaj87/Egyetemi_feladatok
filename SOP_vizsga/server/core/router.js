import express from 'express';
import LoginController from '../controller/login.js';
import AuthController from '../controller/auth.js';
import RegisterController from '../controller/register.js';
import UserController from '../controller/user.js';
import CarsController from '../controller/cars.js';
import PayController from '../controller/pay.js';
import HistoryController from '../controller/history.js';

class RouterClass
{
    static ROUTER = express.Router();

    constructor()
    {
        const Router = RouterClass.ROUTER;
        

        Router.post('/auth', AuthController.Auth, (req, res) => {

            res.json({
                success: res.success,
                message: res.message
            })

            
            
        });

        Router.post('/login', LoginController.Login, (req, res) => {
            
            res.json({
                success: res.success,
                message: res.message,
                token: res.token
            })

        });

        Router.post('/register', RegisterController.Register, (req, res) => {

            res.json({
                success: res.success,
                message: res.message
            })

        })

        Router.post('/user', AuthController.Auth, UserController.GetUserData, (req, res) => {
            if (!res.success)
            {
                res.json({
                    success: res.success,
                    message: res.message,
                    userData: res.userData
                })
            }
            else
            {
                res.json({
                    success: res.success,
                    message: '',
                    userData: res.userData
                })

            }
        })

        Router.put('/user', AuthController.Auth, UserController.PutUserData, (req, res) => {
            
            res.json({
                success: res.success,
                message: res.message,
                logout: res.logout
            })
        })

        Router.delete('/user', AuthController.Auth, UserController.DeleteUser, (req, res) => {
            res.json({
                success: res.success,
                message: res.message,
                logout: res.logout
            })
        })

        Router.post('/cars', AuthController.Auth, CarsController.Cars, (req, res) => {
            
            res.json({
                success: res.success,
                message: res.message,
                cars: res.cars,

            });
        })

        Router.post('/pay', AuthController.Auth, PayController.CheckPrice, PayController.Pay, PayController.SetAvailability, (req, res) => {

            res.json({
                success: res.success,
                message: res.message,
                payment_id: res.payment_id
            })
        })

        Router.post('/history', AuthController.Auth, HistoryController.History, (req, res) => {
            res.json({
                success: res.success,
                message: res.message,
                history: res.history
            })
        })

    }
}


export default RouterClass;
