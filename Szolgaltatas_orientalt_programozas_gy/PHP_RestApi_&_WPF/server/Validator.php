<?php

class Validator
{
    const VALID_CHARS = "öüóqwertzuiopőúasdfghjkléáűíyxcvbnmÖÜÓQWERTZUIOPŐÚASDFGHJKLÉÁŰÍYXCVBNM .";

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
        if ($name_length < 5 || $name_length > 40) throw new Exception("Name must be between 5 and 40 characters.");

        self::SpecialChars($name, "Name");
    }

    public static function Email(string $email)
    {
        $email_length = strlen($email);
        if ($email_length < 5 || $email_length > 50) throw new Exception("Email must be between 5 and 50 characters!");

        if (!filter_var($email, FILTER_VALIDATE_EMAIL)) throw new Exception("Invalid email format");
    }

    public static function Department(bool $newDepartment, string $department, string $salary)
    {
        $department_length = strlen($department);

        if (!$newDepartment && $department_length == 0) throw new Exception("You must select a department!");

        if ($newDepartment)
        {
            if ($department_length == 0 || $department_length > 60) throw new Exception("Name of department must be between 1 and 60 characters!");
            
            if (!$salary) throw new Exception('You must set the Salary for "' .$department. '" !');
            else
            {
                if ($salary[0] == 0 || !is_numeric($salary)) throw new Exception('Salary is invalid!');
            }

        }

        self::SpecialChars($department, "name of department");
    }

    public static function Password(bool $isAdmin, string $passwd1, string $passwd2)
    {
        if ($isAdmin)
        {
            $passwd1_length = strlen($passwd1);

            if ($passwd1_length <= 3 || $passwd1_length > 10) throw new Exception("Password must be between 4 and 10 characters!");

            if ($passwd1 != $passwd2) throw new Exception("Passwords must match!");
        }
    }
}