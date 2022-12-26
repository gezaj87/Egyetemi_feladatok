<?php

class Validation
{
    const VALID_CHARS = "öüóqwertzuiopőúasdfghjkléáűíyxcvbnmÖÜÓQWERTZUIOPŐÚASDFGHJKLÉÁŰÍYXCVBNM .";
    const BANKCARD_INVALID = "Bank card is invalid!";
    const CVC_INVALID = "CVC is invalid!";
    const PRICE_INVALID = "The price is invalid!";

    private static function SpecialChars(string $str, $typeOfInput)
    {
        $stringArr = str_split($str);
        foreach($stringArr as $char)
        {
            if (!str_contains(self::VALID_CHARS, $char))
            {
                throw new Exception($typeOfInput." cannot contain special character: " .$char);
            }
        }
    }

    public static function Name(string $name)
    {
        $name_length = strlen($name);
        if ($name_length < 0 || $name_length > 120) throw new Exception("Name must be between 1 and 120 characters.");

        self::SpecialChars($name, "Name");
    }

    public static function BankCard(string $bankCard)
    {
        if (strlen($bankCard) !== 16 || !is_numeric($bankCard))
        {
            throw new Exception(self::BANKCARD_INVALID);
        }
    }

    public static function ExpDate(string $exp_date)
    {
        if (strlen($exp_date) !== 4 || !is_numeric($exp_date))
        {
            throw new Exception(self::BANKCARD_INVALID);
        }
    }

    public static function CVC(string $cvc)
    {
        if (strlen($cvc) !== 3 || !is_numeric($cvc))
        {
            throw new Exception(self::BANKCARD_INVALID);
        }
    }

    public static function PositiveNumber(int $number)
    {
        if (!is_numeric($number) || $number <= 0)
        {
            throw new Exception(self::PRICE_INVALID);
        }
        
    }




}