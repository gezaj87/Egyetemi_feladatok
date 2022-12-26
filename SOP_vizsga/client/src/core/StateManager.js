import App from '../App';
import {useEffect, useState} from 'react';
import { useCookies } from 'react-cookie';
import Login from '../components/Login';
import Register from '../components/Register';

function StateManager()
{
    const [cookies, setCookie] = useCookies(['token']);

    const [getIsLoggedIn, setIsLoggedIn] = useState(false);
    const [getOrder, setOrder] = useState(false);


    const states = {
        cookies, setCookie,
        getIsLoggedIn, setIsLoggedIn,
        getOrder, setOrder
    }

    return (
        <>
            {/* {App( states )} */}
            <App states={states}/>
        </>
    )
}


export default StateManager;