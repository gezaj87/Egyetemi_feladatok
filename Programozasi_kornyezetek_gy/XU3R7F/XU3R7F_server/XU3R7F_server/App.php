<?php


class App
{
    private $request_method;
    private $url;
    private $url_length;

    

    public function __construct($request_method)
    {
        $this->request_method = $request_method;
        
        if (session_status() === PHP_SESSION_NONE) session_start();
        
        
        $url = '/'.$_GET['url'];

        $this->url = explode('/', $url);
        $this->url_length = count($this->url);

        
        $Router = new Router($this->url);

        $Router->Get('/', [Controller::class, 'Select_All_Employee']);
        $Router->Get('/departments', [Controller::class, 'Get_Departments']);
        $Router->Get('/id/{any}', [Controller::class, 'Select_By_Id']);

        $Router->Post('/login', [Controller::class, 'Login']);
        $Router->Post('/add', [Controller::class, 'Add']);

        $Router->Put('/', [Controller::class, 'Edit']);

        $Router->Delete('/id/{any}', [Controller::class, 'Delete']);







    }

}
