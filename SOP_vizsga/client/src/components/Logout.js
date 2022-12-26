import {useEffect} from 'react';
import Fetch from '../core/Fetch';
import Auth from '../core/Auth';
import { useNavigate  } from "react-router-dom";
import Helper from '../core/Helper';

function Logout({states})
{
    const navigate = useNavigate();

    

    useEffect(() => {
        console.log('Logout component has started...');

        states.setCookie('token', '');

        Helper.ToggleSpinner(true);

        setTimeout(() => {
            Helper.ToggleSpinner(false);
            navigate('/');
            document.location.reload();
        }, 1000)

    },[])



    return(
        <div>
            
        </div>
    )
}

export default Logout;

