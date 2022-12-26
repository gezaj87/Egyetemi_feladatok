import {useEffect, useState} from 'react';
import { useNavigate, Link } from "react-router-dom";
import Fetch from '../core/Fetch';
import Helper from '../core/Helper';

function Profile({states})
{
    const navigate = useNavigate();

    const [getEmail, setEmail] = useState('');
    const [getName, setName] = useState('');
    const [getAdress, setAdress] = useState('');

    const [getInputEmail, setInputEmail] = useState('');
    const [getInputPassword0, setInputPassword0] = useState('');
    const [getInputPassword1, setInputPassword1] = useState('');
    const [getInputPassword2, setInputPassword2] = useState('');
    const [getInputName, setInputName] = useState('');
    const [getInputAdress, setInputAdress] = useState('');
    const [getResponseMessage, setResponseMessage] = useState('');

    const [getIsEdit, setIsEdit] = useState(false);
    const [getIsPasswordChange, setIsPasswordChange] = useState(false);
    const [getHistoryData, setHistoryData] = useState(false);

    useEffect(() => {
        if (!states.getIsLoggedIn)
        {
            navigate('/');
        }
    },[states.getIsLoggedIn])

    useEffect(()=> {
        if(states.getIsLoggedIn)
        {
            GetUserData();
            GetHistory();
        }
    },[])

    useEffect(() => {
        if (!getIsPasswordChange)
        {
            setInputPassword0('');
            setInputPassword1('');
            setInputPassword2('');
        }
    },[getIsPasswordChange])
    

    async function GetUserData()
    {
        const body = {
            token: states.cookies.token
        }

        Helper.ToggleSpinner(true);
        const response = await Fetch('/api/user', 'POST', body);
        Helper.ToggleSpinner(false);

        console.log("ittttttt")
        console.log(response)

        if (!response.success)
        {
            setResponseMessage(response.message);
            Helper.ErrorMessageShow("registerError", true, false);
        }
        else
        {
            // setResponseMessage(response.message);
            // Helper.ErrorMessageShow("registerSuccess", true, true);
            console.log("UserData received from server.")

            setEmail(response.userData.email);
            setName(response.userData.name);
            setAdress(response.userData.address);

            setInputEmail(response.userData.email);
            setInputName(response.userData.name);
            setInputAdress(response.userData.address)
        }
    }

    async function GetHistory()
    {
        const body = {
            token: states.cookies.token
        }

        Helper.ToggleSpinner(true);
        const response = await Fetch('/api/history', 'POST', body);
        Helper.ToggleSpinner(false);

        console.log(response)

        const button = document.getElementById("historyButton");

        if (response.success)
        {
            setHistoryData(response.history);
            button.disabled = false;
        }
        else
        {
            button.disabled = true;
        }


    }

    function RestoreDatas()
    {
        setInputEmail(getEmail);
        setInputName(getName);
        setInputAdress(getAdress);
    }

    async function SubmitForm()
    {
        const body = {
            token: states.cookies.token,
            email: getInputEmail,
            name: getInputName,
            address: getInputAdress,
            isPasswordChanged: getInputPassword0 === ''? false : true,
            oldPassword: getInputPassword0,
            newPassword1: getInputPassword1,
            newPassword2: getInputPassword2,
        }

        const response = await Fetch('/api/user', 'PUT', body);

        if (!response.success)
        {
            setResponseMessage(response.message);
            setIsPasswordChange(false);
            Helper.ErrorMessageShow("registerError", true, false);
        }
        else
        {
            // setResponseMessage(response.message);
            // Helper.ErrorMessageShow("registerSuccess", true, true);

            
            setResponseMessage(response.message);
            Helper.ErrorMessageShow("registerSuccess", true, true);

            if (response.logout)
            {
                
                setTimeout(() => {
                    states.setIsLoggedIn(false);
                },1000)
            }
            else
            {
                GetUserData();
                setIsPasswordChange(false);
            }



            
        }
    }

    async function DeleteUser()
    {
        const body = {
            token: states.cookies.token,
        }

        const iAmSure = window.confirm("Are you sure to delete your profile?");

        if (!iAmSure) return;
        
        const response = await Fetch('/api/user', 'DELETE', body);
        
        if (!response.success)
        {
            setResponseMessage(response.message);
            Helper.ErrorMessageShow("registerError", true, false);
        }
        else
        {
            setResponseMessage(response.message);
            Helper.ErrorMessageShow("registerSuccess", true, true);

            if (response.logout)
            {
                
                setTimeout(() => {
                    states.setIsLoggedIn(false);
                },1000)
            }
        }
        

        
    }


    function ShowHistoryData()
    {
        if (!getHistoryData) return;

        return(
            <table className="table table-hover bg-light">
                <colgroup>
                    <col span="1" style={{width: '3%'}}/>
                    <col span="1" style={{width: '30%'}}/>
                    <col span="1" style={{width: '30%'}}/>
                    <col span="1" style={{width: '24%'}}/>
                    <col span="1" style={{width: '3%'}}/>
                </colgroup>
                <thead>
                    <tr>
                    <th scope="col">#</th>
                    <th scope="col">ID</th>
                    <th scope="col">Date</th>
                    <th scope="col">Car</th>
                    <th scope="col">Price</th>
                    </tr>
                </thead>
                <tbody>

                    {getHistoryData.map((e, i) => {
                        return(
                            <tr key={'history'+i}>
                                <th scope="row">{i+1}</th>
                                <td>{e.p_id}</td>
                                <td>{e.date}</td>
                                <td>{e.plate}</td>
                                <td>€{e.price}</td>
                            </tr>
                        )
                    })}
        
                </tbody>

            </table>
        )
    }

    return (
        <div id="Profile">

            <h3 className="text-center text-black pt-3">My Profile</h3>

            <div id="registerError" className="alert alert-danger mt-3 text-center d-none" role="alert">
                {getResponseMessage} 
            </div>
            <div id="registerSuccess" className="alert alert-success mt-3 text-center d-none" role="alert">
                {getResponseMessage} 
            </div>

            <div className='container'>
                <div className='row justify-content-center '>

                    <div className='col-md-9'>
                        <div className="mt-2 p-4 text-info rounded content-background">
                            <div id="login-box" className="col-md-12">
                                <div id="login-form" className="form">
                                <button type="button" className="btn btn-secondary btn-lg btn-block mb-2" onClick={()=> setIsEdit(!getIsEdit)}>EDIT your Profile</button>
                                <button type="button" className="btn btn-danger btn-sm btn-block mb-2" onClick={()=> DeleteUser()}>DELETE your Profile</button>
                                    <div className="form-group text-center">
                                        <label htmlFor="email" className="text-info">Email</label><br/>
                                        <input type="text" id="email" className="form-control text-center" onChange={(e) => {
                                            Helper.ErrorMessageShow();
                                            setInputEmail(e.target.value);
                                        }} value={getInputEmail} readOnly={getIsEdit? '' : 'readOnly'}/>
                                    </div>
                                    <div className="form-group text-center">
                                        <label htmlFor="name" className="text-info">Name</label><br/>
                                        <input type="text" id="name" className="form-control text-center" onChange={(e) => {
                                            Helper.ErrorMessageShow();
                                            setInputName(e.target.value);
                                        }} value={getInputName} readOnly={getIsEdit? '' : 'readOnly'}/>
                                    </div>
                                    <div className="form-group text-center">
                                        <label htmlFor="address" className="text-info">Adress</label><br/>
                                        <input type="text" id="address" className="form-control text-center" onChange={(e) => {
                                            Helper.ErrorMessageShow();
                                            setInputAdress(e.target.value);
                                        }} value={getInputAdress} readOnly={getIsEdit? '' : 'readOnly'}/>
                                    </div>

                                    <div className={getIsEdit? 'text-center mb-3' : 'd-none'}>
                                        <button className="btn btn-info" type="submit" onClick={() => setIsPasswordChange(!getIsPasswordChange)}>Change password</button>
                                    </div>

                                    <div className={getIsEdit && getIsPasswordChange? '' : 'd-none'}>
                                        <div className="form-group">
                                            <label htmlFor="password0" className="text-info">Old password:</label><br/>
                                            <input type="text" id="password0" className="form-control text-center" onChange={(e) => {
                                                Helper.ErrorMessageShow();
                                                setInputPassword0(e.target.value);
                                            }} value={getInputPassword0}/>
                                        </div>
                                        <div className="form-group">
                                            <label htmlFor="password1" className="text-info">New password:</label><br/>
                                            <input type="text" id="password1" className="form-control text-center" onChange={(e) => {
                                                Helper.ErrorMessageShow();
                                                setInputPassword1(e.target.value);
                                            }} value={getInputPassword1}/>
                                        </div>
                                        <div className="form-group">
                                            <label htmlFor="password2" className="text-info">New password:</label><br/>
                                            <input type="text" id="password2" className="form-control text-center" onChange={(e) => {
                                                Helper.ErrorMessageShow();
                                                setInputPassword2(e.target.value);
                                            }} value={getInputPassword2}/>
                                        </div>
                                    </div>
                                    
                                    <div className={getIsEdit? '' : 'd-none'}>
                                        <button type="button" className="btn btn-warning btn-lg btn-block" onClick={() => RestoreDatas()}>Restore</button>
                                        <button type="button" className="btn btn-primary btn-lg btn-block" onClick={() => SubmitForm()}>Save changes</button>
                                    </div>
                                </div>
                            </div>


                                        

                            <hr/>
                            <button type="button" id="historyButton" className="btn btn-secondary btn-md btn-block" onClick={() => {
                                const element = document.getElementById('history');
                                if (element.className === 'd-none') element.className = '';
                                else element.className = 'd-none';

                            }}>Payment history</button>
                            <div id="history" className="d-none">
                                
                                <ShowHistoryData/>

                                
                            </div>



                        </div>
                    </div>

                    
                    


                </div>
            </div>
        </div>
    )
}

export default Profile;