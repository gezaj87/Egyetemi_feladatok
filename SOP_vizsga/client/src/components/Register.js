import {useEffect, useState} from 'react';
import { useNavigate, Link } from "react-router-dom";
import Fetch from '../core/Fetch';
import Helper from '../core/Helper';

function Register()
{
    const navigate = useNavigate();

    const [getInputEmail, setInputEmail] = useState('');
    const [getInputPassword1, setInputPassword1] = useState('');
    const [getInputPassword2, setInputPassword2] = useState('');
    const [getInputName, setInputName] = useState('');
    const [getInputAdress, setInputAdress] = useState('');
    const [getResponseMessage, setResponseMessage] = useState('');

    async function SubmitForm()
    {
        const body = {
            email: getInputEmail,
            password1: getInputPassword1,
            password2: getInputPassword2,
            name: getInputName,
            address: getInputAdress,
        }

        Helper.ToggleSpinner(true);
        const response = await Fetch('/api/register', 'POST', body);
        Helper.ToggleSpinner(false);

        if (!response.success)
        {
            setResponseMessage(response.message);
            Helper.ErrorMessageShow("registerError", true, false);
        }
        else
        {
            setResponseMessage(response.message);
            Helper.ErrorMessageShow("registerSuccess", true, true);
            
            setTimeout(()=> {
                navigate('/');
            }, 1000)
        }
    }


    return (
        <div id="login" className='register'>
            <h3 className="text-center text-black pt-3">Register form</h3>

            <div id="registerError" className="alert alert-danger mt-3 text-center d-none" role="alert">
                {getResponseMessage} 
            </div>
            <div id="registerSuccess" className="alert alert-success mt-3 text-center d-none" role="alert">
                {getResponseMessage} 
            </div>

            <div className="container">
                <div id="login-row" className="row justify-content-center align-items-center">
                    <div id="login-column" className="col-md-6">
                        <div id="login-box" className="col-md-12 register">
                            <div id="login-form" className="form">
                                <h3 className="text-center text-info">Register</h3>
                                <div className="form-group">
                                    <label htmlFor="email" className="text-info">Email:</label><br/>
                                    <input type="text" id="email" className="form-control" onChange={(e) => {
                                        Helper.ErrorMessageShow();
                                        setInputEmail(e.target.value);
                                    }}/>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="password1" className="text-info">Password:</label><br/>
                                    <input type="text" id="password1" className="form-control" onChange={(e) => {
                                        Helper.ErrorMessageShow();
                                        setInputPassword1(e.target.value);
                                    }}/>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="password2" className="text-info">Password:</label><br/>
                                    <input type="text" id="password" className="form-control" onChange={(e) => {
                                        Helper.ErrorMessageShow();
                                        setInputPassword2(e.target.value);
                                    }}/>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="name" className="text-info">Name:</label><br/>
                                    <input type="text" id="name" className="form-control" onChange={(e) => {
                                        Helper.ErrorMessageShow();
                                        setInputName(e.target.value);
                                    }}/>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="address" className="text-info">Adress:</label><br/>
                                    <input type="text" id="address" className="form-control" onChange={(e) => {
                                        Helper.ErrorMessageShow();
                                        setInputAdress(e.target.value);
                                    }}/>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="remember-me" className="text-info"><span></span> <span></span></label><br/>
                                    <button className="btn btn-info btn-md" onClick={() => SubmitForm()}>Submit</button>
                                </div>
                                <div id="register-link" className="text-right">
                                    <Link className="text-info" to={'/login'}>Login here</Link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            

        </div>
    )
}

export default Register;