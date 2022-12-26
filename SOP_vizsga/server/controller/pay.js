import Mysql from '../core/mysql.js';
import http from 'http';
import request from 'request';


class PayController
{
    static INPUT_NOT_FOUND = "Input is missing!";
    static BANKCARD_ERROR = "Bankcard must be 16 digits!";
    static EXP_DATE_ERROR = "Expiration date must be 4 digits!";
    static CVC_ERROR = "CVC number must be 3 digits!";
    static CAR_PRICE_ERROR = "The given price do not match with the one in database! Or plate number is not found!";
    static PAYMENT_SUCCESS = "Payment was successful!";
    static PAYMENT_ERROR = "Payment was unsuccessful!";

    static Pay(req, res, next)
    {
        if (!res.success) next();

        const name = req.body.name;
        const cardNumber = req.body.cardNumber.replace(/\s+/g, '');
        const expDate = req.body.expDate.replace(/\/+/g, '');
        const cvc = req.body.cvc;
        const price = req.body.price;
        const plate = req.body.plate;


        try
        {
            if (!name || !cardNumber || !expDate || !cvc || !price || !plate || name.length === 0 || plate === 0)
            {
                throw PayController.INPUT_NOT_FOUND;
            }

            if (cardNumber.length !== 16 || !Number.isInteger(parseInt(cardNumber)))
            {
                throw PayController.BANKCARD_ERROR;
            }

            if (expDate.length !== 4 || !Number.isInteger(parseInt(expDate)))
            {
                throw PayController.EXP_DATE_ERROR;
            }

            if (cvc.length !== 3 || !Number.isInteger(parseInt(cvc)))
            {
                throw PayController.CVC_ERROR;
            }

        }
        catch(error)
        {
            res.success = false;
            res.message = error;
            next();
        }

        
        request.post(
            'http://127.0.0.1/sop/pay',
            { json: {
                
                token: process.env.BANKTOKEN,
                name: name,
                card_number: cardNumber,
                exp_date: expDate,
                cvc: cvc,
                price: price,
                

            } }, (error, response, body) => {

                if (!error && response.statusCode == 200) {
                    
                    if (!body.success)
                    {
                        res.success = false;
                        res.message = body.message;
                        next();
                    }

                    console.log("Payment was successful.");

                    PayController.SavePayment(req, res, next, body.payment_id, res.userID, plate, price);

                }

                // console.log(response)
            }
        );
       

        


        // next();
    }


    static SavePayment(req, res, next, p_id, u_id, plate, price)
    {
        const query = "insert into payments (p_id, u_id, plate, price) values (?, ?, ?, ?)";
        const params = [p_id, u_id, plate, price];

        Mysql.SQL(query, params, (error, results, fields) => {
            
            if (results)
            {
                res.message = PayController.PAYMENT_SUCCESS;
                res.payment_id = p_id;
                next();
            }
            else
            {
                res.success = false;
                res.message = PayController.PAYMENT_ERROR;
                res.payment_id = '';
                next();
            }
        })
    }


    static CheckPrice(req, res, next)
    {
        const price = req.body.price;
        const plate = req.body.plate;

        try
        {
            if (!price || !Number.isInteger(parseInt(price)) || !plate || plate == '')
            {
                throw PayController.INPUT_NOT_FOUND;
            }

        }
        catch(error)
        {
            res.success = false;
            res.message = error;
            next();
        }


        const query = "select * from cars where plate = ? and price = ?";
        const params = [plate, price];

        Mysql.SQL(query, params, (error, results, fields) => {

            if (results.length > 0)
            {
                next();
            }
            else
            {
                res.success = false;
                res.message = PayController.CAR_PRICE_ERROR;
                next();
            }
        })






    }


    static SetAvailability(req, res, next)
    {
        if (!res.success) next();

        const plate = req.body.plate;

        const query = "update cars set avaiable = ? where plate = ?";
        const params = [0, plate];

        Mysql.SQL(query, params, (error, results, fields) => {
            
            next();
        })
    }


}


export default PayController;