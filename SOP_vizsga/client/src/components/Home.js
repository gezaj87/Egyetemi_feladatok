import {useEffect, useState} from 'react';
import { useNavigate  } from "react-router-dom";

function Home({states})
{
    const navigate = useNavigate();

    useEffect(() => {
        if (!states.getIsLoggedIn)
        {
            navigate('/login');
        }
    },[states.getIsLoggedIn])

    
    return(
        <div id="Home">

            <h3 className="text-center text-black pt-3">Home</h3>

            {/* <div id="loginError" className="alert alert-danger mt-3 text-center d-none" role="alert">
                {getResponseMessage} 
            </div> */}

            <div className='container'>
                <div className='row justify-content-center align-items-center'>
                    <div className='col-md-12'>
                        <div className="mt-2 p-5 text-info rounded content-background">
                            <h4 className='text-center'>Welcome to our Car Rent Service!</h4>
                            <div className='home-page'>
                                <img src='img/main.webp'/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Home;