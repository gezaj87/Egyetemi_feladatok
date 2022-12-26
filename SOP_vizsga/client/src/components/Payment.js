import {useEffect, useState} from 'react';
import { useNavigate  } from "react-router-dom";
import Fetch from '../core/Fetch';
import Helper from '../core/Helper';

function Payment({states})
{
    const navigate = useNavigate();
    const [getOrder, setOrder] = useState('');

    const [getResponseMessage, setResponseMessage] = useState('');
    

    useEffect(() => {

        setOrder(states.getOrder);

        if (!states.getIsLoggedIn)
        {
            navigate('/login');
        }

        return (() => {
            states.setOrder(false);
        })

    },[])

    function AbortPayment()
    {
        states.setOrder(false);

        navigate('/cars');

    }

    async function SubmitPayment(e)
    {
        e.preventDefault();
        Helper.ErrorMessageShow();

        

        const name = document.getElementById('name').value;
        const cardNumber = document.getElementById('cardNumber').value;
        const expDate = document.getElementById('expDate').value;
        const cvc = document.getElementById('cvc').value;

        const body = {
            token: states.cookies.token,
            name: name,
            cardNumber: cardNumber,
            expDate: expDate,
            cvc: cvc,
            price: getOrder.price,
            plate: getOrder.plate
        }


        Helper.ToggleSpinner(true);
        const response = await Fetch('/api/pay', 'POST', body);
        Helper.ToggleSpinner(false);
        console.log(response);

        if (!response.success)
        {
            setResponseMessage(response.message);
            Helper.ErrorMessageShow("registerError", true, false);
        }
        else
        {
            setResponseMessage(response.message + " Your payment id: " + response.payment_id);
            Helper.ErrorMessageShow("registerSuccess", true, true);
            
            setTimeout(()=> {
                navigate('/');
            }, 5000)
        }
    }

    function CardPayment()
    {

        return(
            <div className='pay-card'>

                <form onSubmit={(e) => SubmitPayment(e)}>
                    <div>
                        <label>
                            <span>Name</span>
                            <input id='name' type='text' autoFocus={true} required onChange={() => Helper.ErrorMessageShow()}/>
                        </label>

                        <label>
                            <span>Card number</span>
                            <input id='cardNumber' type='text' autoFocus={true} maxLength={19} onKeyUp={(e)=> {
                                Helper.ErrorMessageShow();
                                Helper.checkDigit(e)? e.target.value = Helper.cc_format(e.target.value) : e.target.value = '';
                            }} required/>
                        </label>

                        <label>
                            <span>Lejárat</span>
                            <input id='expDate' type='text' autoFocus={true} maxLength={5} onKeyUp={(e) => {
                                if (e.target.value.length < 5)
                                {
                                    Helper.ErrorMessageShow();
                                    Helper.checkDigit(e)? e.target.value = e.target.value.replace(/\W/gi, '').replace(/(.{2})/g, '$1/') : e.target.value = '';
                                }
                            }} required/>
                        </label>

                        <label>
                            <span>CVC</span>
                            <input id='cvc' type='text' autoFocus={true} maxLength={3} onKeyUp={(e) => {
                                Helper.ErrorMessageShow();
                                Helper.checkDigit(e)? e.target.value = e.target.value : e.target.value = '';
                            }} required/>
                        </label>
                    </div>
                            
                    <button type="submit" className="btn btn-primary btn-lg btn-block">Pay €{getOrder.price}</button>
                    <button type="button" className="btn btn-danger btn-lg btn-block" onClick={()=> Helper.ToggleSpinner(true)}>Abort</button>
                    

                    
                    
                </form>
            </div>
        )

    }

    function CarPrewView()
    {
        return(
            <div className="col card-custom pt-3 pb-3">
                <div className="card h-100">
                    <img src={`img/${getOrder.image}`} className="card-img-top"/>
                    <div className="card-body text-center">
                        <h5 className="card-title">{getOrder.plate}</h5>
                        <p>Brand: {getOrder.brand}</p>
                        <p>Year: {getOrder.year}</p>
                        <p>Capacity: {getOrder.capacity}</p>
                        <p>Price: €{getOrder.price} / 1 month</p>
                    </div>
                </div>
            </div>
        )
    }

    

    return(

        <div id="Payment">

        <h3 className="text-center text-black pt-3">Online Payment</h3>

        {/* <div id="loginError" className="alert alert-danger mt-3 text-center d-none" role="alert">
            {getResponseMessage} 
        </div> */}

        <div className='container'>
            <div className='row justify-content-center align-items-center mt-2 p-3 text-info rounded content-background'>
                <div className='col-md-6'>
                    <div className="">
                        
                        <CarPrewView/>

                    </div>
                </div>
                <div className='col-md-6'>
                    <div className="">

                        <CardPayment/>

                    </div>

                    <div id="registerError" className="alert alert-danger mt-3 text-center d-none" role="alert">
                        {getResponseMessage} 
                    </div>
                    <div id="registerSuccess" className="alert alert-success mt-3 text-center d-none" role="alert">
                        {getResponseMessage} 
                    </div>
                    
                </div>
                
            </div>
        </div>
    </div>











    )
}

export default Payment;