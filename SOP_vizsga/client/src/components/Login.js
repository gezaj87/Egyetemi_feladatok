import {useEffect, useState} from 'react';
import { useNavigate, Link } from "react-router-dom";
import Fetch from '../core/Fetch';
import Auth from '../core/Auth';
import Helper from '../core/Helper';


function Login({states})
{
    const navigate = useNavigate();

    const [getInputEmail, setInputEmail] = useState('');
    const [getInputPassword, setInputPassword] = useState('');
    const [getResponseMessage, setResponseMessage] = useState('');


    useEffect(() => {
        if (states.getIsLoggedIn)
        {
            navigate('/');
        }
    },[states.getIsLoggedIn])


    async function SubmitForm()
    {
        Helper.ToggleSpinner(true);
        const body = {
            email: getInputEmail,
            password: getInputPassword
        }

        const response = await Fetch('/api/login', 'POST', body);

        Helper.ToggleSpinner(false);
        if (!response.success)
        {
            setResponseMessage(response.message);
            ErrorMessageShow();
        }
        else
        {
            states.setCookie('token', response.token);
            let status = await Auth(states, response.token);

            navigate('/');

        }


    }

    function ErrorMessageShow(toggle = true)
    {
        const element = document.getElementById("loginError");

        if (toggle) element.classList.remove("d-none");
        else element.classList.add("d-none");
    }

    return (
    <div>
        <div id="login">
            <h3 className="text-center text-black pt-3">Login form</h3>

            <div id="loginError" className="alert alert-danger mt-3 text-center d-none" role="alert">
                {getResponseMessage} 
            </div>

            <div className="container">
                <div id="login-row" className="row justify-content-center align-items-center">
                    <div id="login-column" className="col-md-6">
                        <div id="login-box" className="col-md-12">
                            <div id="login-form" className="form">
                                <h3 className="text-center text-info">Login</h3>
                                <div className="form-group">
                                    <label htmlFor="email" className="text-info">Email:</label><br/>
                                    <input type="text" id="email" className="form-control" onChange={(e) => {
                                        ErrorMessageShow(false);
                                        setInputEmail(e.target.value);
                                    }}/>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="password" className="text-info">Password:</label><br/>
                                    <input type="text" id="password" className="form-control" onChange={(e) => {
                                        ErrorMessageShow(false);
                                        setInputPassword(e.target.value);
                                    }}/>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="remember-me" className="text-info"><span></span> <span></span></label><br/>
                                    <button className="btn btn-info btn-md" onClick={() => SubmitForm()}>Submit</button>
                                </div>
                                <div id="register-link" className="text-right">
                                    <Link className="text-info" to={'/register'}>Register here</Link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        

    </div>
    )
}

export default Login;