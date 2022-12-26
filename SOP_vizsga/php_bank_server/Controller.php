<?php

class Controller
{
    const INPUT_MISSING = 'Input is missing!';
    const CUSTOMER_ID_ERROR = 'Customer ID does not match with TOKEN!';

    private static function CheckToken($token)
    {
        $query = 'select c_id from tokens where token = ?';
        $params = [$token];

        $result = Database::SQL($query, $params)->fetch_assoc();

        return $result;
    }


    private static function ProcessPayment($name, $card_number, $exp_date, $cvc, $price, $c_id)
    {
        // ($data['name'], $data['card_number'], $data['exp_date'], $data['cvc'], $data['price'], $c_id['c_id'])
        $payment_id = uniqid("", true);

        // INSERT INTO table_name (column1, column2, column3, ...)
        // VALUES (value1, value2, value3, ...);
        $query = 'insert into payments (p_id, name, card_number, exp_date, cvc, c_id, price) values (?, ?, ?, ?, ?, ?, ?)';
        $params = [$payment_id, $name, $card_number, $exp_date, $cvc, $c_id, $price];

        Database::SQL($query, $params);

        return $payment_id;

    }


    public static function Pay()
    {
        header('Content-Type: application/json');
        $data = json_decode(file_get_contents('php://input'), true);

        $respone['success'] = true;
        $respone['message'] = '';

        if (
            !isset($data['token']) ||
            !isset($data['name']) ||
            !isset($data['card_number']) ||
            !isset($data['exp_date']) ||
            !isset($data['cvc']) ||
            !isset($data['price'])
        )
        {
            $respone['success'] = false;
            $respone['message'] = self::INPUT_MISSING;

            echo json_encode($respone);
            die();
        }


        try
        {
            $c_id = self::CheckToken($data['token']);
        }
        catch(Exception $e)
        {
            $respone['success'] = false;
            $respone['message'] = $e->getMessage();

            echo json_encode($respone);
            die();
        }

        if (!$c_id)
        {
            $respone['success'] = false;
            $respone['message'] = self::CUSTOMER_ID_ERROR;
            echo json_encode($respone);
            die();
        }
        
        
        //sikeres azonosítás
        
        try
        {
            Validation::Name($data['name']);
            Validation::BankCard($data['card_number']);
            Validation::CVC($data['cvc']);
            Validation::PositiveNumber($data['price']);

        }
        catch(Exception $e)
        {
            $respone['success'] = false;
            $respone['message'] = $e->getMessage();

            echo json_encode($respone);
            die();
        }
        
        //sikeres validáció



        try
        {
            $payment_id = self::ProcessPayment($data['name'], $data['card_number'], $data['exp_date'], $data['cvc'], $data['price'], $c_id['c_id']);
        }
        catch(Exception $e)
        {
            $respone['success'] = false;
            $respone['message'] = $e->getMessage();

            echo json_encode($respone);
            die();
        }
        
        $respone['payment_id'] = $payment_id;

        echo json_encode($respone);
        die();
        
    }

}