<?php

class Router
{
    private array $url;
    private int $url_length;

    public function __construct(array $url)
    {
        $this->url = $url;
        $this->url_length = count($url);

    }

    private function Handle_Routing(string $route, $function)
    {
        $str_arr = explode('/', $route);

        if ($this->url === $str_arr)
        {
            return call_user_func($function[0].'::'.$function[1]);
        }

        $str_arr_length = count($str_arr);
        if ($this->url_length === $str_arr_length)
        {
            $match = true;

            for ($i = 0; $i < $this->url_length - 1; $i++)
            {
                if ($this->url[$i] !== $str_arr[$i])
                {
                    $match = false;
                    break;
                }
            }

            if ($match && $str_arr[$str_arr_length - 1] === '{any}')
            {
                return call_user_func($function[0].'::'.$function[1], $this->url[$this->url_length - 1]);
            }
        }

        return http_response_code(404);
    }

    public function Get(string $route, $function)
    {
        if ($_SERVER['REQUEST_METHOD'] === 'GET')
        {
            $this->Handle_Routing($route, $function);
        }
    }

    public function Post(string $route, $function)
    {
        if ($_SERVER['REQUEST_METHOD'] === 'POST')
        {
            $this->Handle_Routing($route, $function);
        }
    }

    public function Put(string $route, $function)
    {
        if ($_SERVER['REQUEST_METHOD'] === 'PUT')
        {
            $this->Handle_Routing($route, $function);
        }
    }

    public function Delete(string $route, $function)
    {
        if ($_SERVER['REQUEST_METHOD'] === 'DELETE')
        {
            $this->Handle_Routing($route, $function);
        }
    }
}