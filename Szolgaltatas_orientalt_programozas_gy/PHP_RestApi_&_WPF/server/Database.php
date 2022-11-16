<?php

class Database
{
    private const HOST = '127.0.0.1';
    private const USER = 'root';
    private const PASSWD = 'pass';
    private const DATABASE = "database";


    public static function Connect()
    {
        return new mysqli(self::HOST, self::USER, self::PASSWD, self::DATABASE);
    }

    public static function SQL($query, $params = [], bool $rows_info = false)
    {

        $conn = self::Connect();
        $stmt = $conn->prepare($query);

        $params_length = count($params);
        $param_types = "";
        for ($i = 0; $i < $params_length; $i++)
        {
            if (gettype($params[$i]) == 'integer') $param_types .= 'i';
            if (gettype($params[$i]) == 'string') $param_types .= 's';
            if (gettype($params[$i]) == 'double') $param_types .= 'f';
        }

        

        if ($params_length > 0) $stmt->bind_param($param_types, ...$params);
        
        $stmt->execute();

        $result = null;
        if ($rows_info) $result = $stmt->affected_rows;
        else $result = $stmt->get_result();
        
        $conn->close();
        return $result;
    }

    

}