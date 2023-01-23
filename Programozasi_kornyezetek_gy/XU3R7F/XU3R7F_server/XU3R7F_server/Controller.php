<?php

class Controller
{
    public static function Auth($data)
    {
        if (!isset($data['token']) || !isset($_SESSION['token']) || $_SESSION['token'] !== $data['token'])
        {
            header('Content-Type: application/json');
            echo json_encode(['error' => 'You are not logged in!']);
            exit();
        }
    }


    public static function Select_All_Employee()
    {
        $queryIsSuccessful = true;

        try
        {
            $result = Database::SQL('select id, name, email, department, salary, isAdmin from employee inner join department on employee.department_id = department.department_id');
            $respone = $result->fetch_all(MYSQLI_ASSOC);
        }
        catch(Exception $e)
        {
            $respone = ['error' => $e->getMessage()];
            $queryIsSuccessful = false;
        }
        
        if ($queryIsSuccessful && count($respone) === 0)
        {
            $respone = ['error' => 'There are not any employees in database!'];
        }

        header('Content-Type: application/json');
        echo json_encode($respone);
        exit();
    }

    public static function Select_By_Id($id)
    {
        $queryIsSuccessful = true;

        try
        {
            $result = Database::SQL('select * from employee inner join department on employee.department_id = department.department_id where employee.id = ?', [$id]);
            $respone = $result->fetch_assoc();
        }
        catch (Exception $e)
        {
            $queryIsSuccessful = false;
            $respone = ['error' => $e->getMessage()];
        }

        if ($queryIsSuccessful && $respone == null)
        {
            $respone = ['error' => 'Id is not found in database!'];
        }
        

        header('Content-Type: application/json');
        echo json_encode($respone);

    }

    public static function Get_Departments()
    {
        header('Content-Type: application/json');
        
        try 
        {
            $results = Database::SQL('select department from department')->fetch_all(MYSQLI_ASSOC);
            echo json_encode($results);
        }
        catch (Exception $e)
        {
            echo json_encode(['error' => $e->getMessage()]);
        }

        
    }

    public static function Login()
    {
        header('Content-Type: application/json');

        $data = json_decode(file_get_contents('php://input'), true);
        $email = ''; $password = '';

        if (isset($data['email']) && isset($data['password']))
        {
            $email =    $data['email'];
            $password = $data['password'];
        }
        

        try
        {
            $result = Database::SQL('select name, email, password from employee where email = ?', [$email])->fetch_assoc();

            if (!$result) throw new Exception('Email address is not found in database!');
            
            if (!password_verify($password, $result['password'])) throw new Exception("Wrong email or password!");

        }
        catch(Exception $e)
        {
            echo json_encode(['error' => $e->getMessage()]);
            exit();
        }


        $token = md5(uniqid(mt_rand(), true));
        $_SESSION['token'] = $token;
        echo json_encode(['ok' => 'Login Successful!', 'token' => $token, 'name' => $result['name']]);
        exit();

    }

    public static function Add()
    {
        $data = json_decode(file_get_contents('php://input'), true);

        self::Auth($data);
        header('Content-Type: application/json');
        
        $name =             isset($data['name'])            ? $data['name'] : '';
        $email =            isset($data['email'])           ? $data['email'] : '';
        $isNewDepartment =  isset($data['isNewDepartment']) ? $data['isNewDepartment'] : '';
        $department =       isset($data['department'])      ? $data['department'] : '';
        $salary =           isset($data['salary'])          ? $data['salary'] : '';
        $isAdmin =          isset($data['admin'])           ? $data['admin'] : '';                 
        $password1 =        isset($data['password1'])       ? $data['password1'] : '';
        $password2 =        isset($data['password2'])       ? $data['password2'] : '';

        try
        {
            Validator::Name($name);
            Validator::Email($email);
            Validator::Department($isNewDepartment, $department, $salary);
            Validator::Password($isAdmin, $password1, $password2);
        }
        catch (Exception $e)
        {
            echo json_encode(['error' => $e->getMessage()]);
            exit();
        }

        

        try
        {
            $department_id = Database::SQL('select department_id from department where department = ?', [$department])->fetch_assoc();

            if (!$department_id)
            {
                Database::SQL('insert into department (department, salary) values (?, ?)', [$department, $salary]);
                $department_id = Database::SQL('select department_id from department where department = ?', [$department])->fetch_assoc();
            }

            Database::SQL('insert into employee (name, email, department_id, isAdmin, password) values(?, ?, ?, ?, ?)', [$name, $email, $department_id['department_id'], $isAdmin, $isAdmin? password_hash($password1, PASSWORD_DEFAULT) : '0']);
        }
        catch(Exception $e)
        {
            echo json_encode(['error' => $e->getMessage()]);
            exit();
        }

        echo json_encode(['ok' => 'New employee has been added!']);
        exit();

        
        
    }

    public static function Edit()
    {
        $data = json_decode(file_get_contents('php://input'), true);

        self::Auth($data);
        header('Content-Type: application/json');

        $id =             isset($data['id'])              ? $data['id'] : '';
        $name =           isset($data['name'])            ? $data['name'] : '';
        $email =          isset($data['email'])           ? $data['email'] : '';
        $isNewDepartment= isset($data['isNewDepartment']) ? $data['isNewDepartment'] : '';
        $department =     isset($data['department'])      ? $data['department'] : '';
        $isAdmin =        isset($data['isAdmin'])         ? $data['isAdmin'] : '';
        $passwd1 =        isset($data['passwd1'])         ? $data['passwd1'] : '';
        $passwd2 =        isset($data['passwd2'])         ? $data['passwd2'] : '';
        $newPass =        isset($data['newPass'])         ? $data['newPass'] : '';
        $newPasswd1 =     isset($data['newPasswd1'])      ? $data['newPasswd1'] : '';
        $newPasswd2 =     isset($data['newPasswd2'])      ? $data['newPasswd2'] : '';
        $salary =         isset($data['salary'])          ? $data['salary'] : '';

        try
        {
            Validator::Name($name);
            Validator::Email($email);
            Validator::Department($isNewDepartment, $department, $salary);

            if ($isAdmin && !$passwd1 && !$passwd2 && !$newPasswd1 && !$newPasswd2)
            {
                $result = Database::SQL('select isAdmin from employee where id = ?', [$id])->fetch_assoc();
                if ($result['isAdmin'] != $isAdmin) throw new Exception("You have to set a password!");
            }

            if ($passwd1 || $passwd2) Validator::Password($isAdmin, $passwd1, $passwd2);
            if ($newPass) Validator::Password($isAdmin, $newPasswd1, $newPasswd2);

            $isIdValid = Database::SQL('select * from employee where id = ?', [$id])->fetch_assoc();
            if (!$isIdValid) throw new Exception("Id was not found!");

        }
        catch (Exception $e)
        {
            echo json_encode(['error' => $e->getMessage()]);
            exit();
        }


        try
        {

            $department_id = Database::SQL('select department_id from department where department = ?', [$department])->fetch_assoc();

            if (!$department_id)
            {
                Database::SQL('insert into department (department, salary) values (?, ?)', [$department, $salary]);
                $department_id = Database::SQL('select department_id from department where department = ?', [$department])->fetch_assoc();
            }

            $query_1 = 'update employee set name = ?, email = ?, department_id = ?, isAdmin = ? where id = ?';
            $query_2 = 'update employee set name = ?, email = ?, department_id = ?, isAdmin = ?, password = ? where id = ?';


            $query = '';
            $result = null;
            $passwordToDatabase = '';
            if ($isAdmin && !$passwd1 && !$passwd2 && !$newPasswd1 && !$newPasswd2)
            {
                $query = 'update employee set name = ?, email = ?, department_id = ?, isAdmin = ? where id = ?';
            }

            if ($isAdmin && $newPass && $newPasswd1 && $newPasswd2)
            {
                $query = 'update employee set name = ?, email = ?, department_id = ?, isAdmin = ?, password = ? where id = ?';
                $passwordToDatabase = $newPasswd1;
                
            }

            if ($isAdmin && $passwd1 && $passwd2) {
                $query = 'update employee set name = ?, email = ?, department_id = ?, isAdmin = ?, password = ? where id = ?';
                $passwordToDatabase = $passwd1;
            }

            if (strlen($passwordToDatabase) > 0) {
                $result = Database::SQL('update employee set name = ?, email = ?, department_id = ?, isAdmin = ?, password = ? where id = ?', [$name, $email, $department_id['department_id'], $isAdmin, password_hash($passwordToDatabase, PASSWORD_DEFAULT), $id], true);
            }
            else
            {
                $result = Database::SQL('update employee set name = ?, email = ?, department_id = ?, isAdmin = ? where id = ?', [$name, $email, $department_id['department_id'], $isAdmin, $id], true);
            }


            if ($result == 0) throw new Exception("Record did not change");

        }
        catch (Exception $e)
        {
            echo json_encode(['error' => $e->getMessage()]); exit();
        }

        if ($result == 1) echo json_encode(['ok' => $result. ' record successfully modified.']);
        exit();

    }


    public static function Delete($param)
    {
        header('Content-Type: application/json');

        $sss = explode('|', $param);
        $token = strlen($sss[0]) === 32 ? $sss[0] : null;

        self::Auth(['token' => $token]);

        $id = $sss[1];
         

        try
        {
            if (!$id || !is_numeric($id))
            {
                throw new Exception("ID is invalid!");
            }

            $result = Database::SQL('select * from employee where id = ?', [$id])->fetch_assoc();
            if (!$result) throw new Exception('ID '. $id .' not found in database!');

            $result = Database::SQL('delete from employee where id = ?', [$id], true);
            if ($result === 0) throw new Exception('The record could not be deleted.');
        }
        catch (Exception $e)
        {
            echo json_encode(['error' => $e->getMessage()]);
            exit();
        }

        //'ID ' .$id. ' was deleted successfully.'
        echo json_encode(['ok' => 'The record was deleted successfully.']);
        exit();


    }
}