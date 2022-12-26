import {useEffect, useState} from 'react';
import { useNavigate  } from "react-router-dom";
import Fetch from '../core/Fetch';
import Helper from '../core/Helper';

function Cars({states})
{
    const navigate = useNavigate();

    const [getCars, setCars] = useState([]);

    useEffect(() => {
        if (!states.getIsLoggedIn)
        {
            navigate('/login');
        }
    },[states.getIsLoggedIn])

    useEffect(() => {
        
        GetCars();
        
    },[])

    useEffect(() => {

        if (states.getOrder)
        {
            navigate('/payment');
        }

    },[states.getOrder])


    async function GetCars(avaiableOnly = false)
    {
        Helper.ToggleSpinner(true);

        const body = {
            token: states.cookies.token,
            avaiableOnly: avaiableOnly
        }

        const response = await Fetch('/api/cars', 'POST', body);
        
        Helper.ToggleSpinner(false);

        if (response.success)
        {
            setCars(response.cars);
        }

        console.log(response)
    }

    function Order(plate, price, year, image, brand)
    {
        states.setOrder({
            plate: plate,
            price: price,
            year: year,
            image: image,
            brand: brand
        });
    }

    function LoadCars()
    {
        
        if (getCars.length > 0)
        {
            return(
                

                <div className="row row-cols-1 row-cols-md-3 g-4">

                    {getCars.map((e, i) => {
                        return (
                            <div key={'card' + i} className="col card-custom">
                                <div className="card h-100">
                                    <img src={`img/${e.image}`} className="card-img-top"/>
                                    <div className="card-body text-center">
                                        <h5 className="card-title">{e.plate}</h5>
                                        <p>Brand: {e.brand}</p>
                                        <p>Year: {e.year}</p>
                                        <p>Capacity: {e.capacity}</p>
                                        <p>Price: €{e.price} / 1 month</p>
                                        <h6 className={e.avaiable === 1? 'text-success' : 'text-danger'}>{e.avaiable === 1? 'AVAIABLE' : 'RENTED OUT'}</h6>
                                        <button className={e.avaiable === 1? 'btn btn-success' : 'd-none'} onClick={() => Order(e.plate, e.price, e.year, e.image, e.brand)}>Rent NOW</button>
                                    </div>
                                </div>
                            </div>
                        )
                    })}

                </div>
            )
        }
        
        
    }


    


    return (
        <div id="Cars">

            <h3 className="text-center text-black pt-3">Home</h3>

            {/* <div id="loginError" className="alert alert-danger mt-3 text-center d-none" role="alert">
                {getResponseMessage} 
            </div> */}

            <div className='container'>
                <div className='row justify-content-center align-items-center'>
                    <div className='col-md-12'>
                        <div className="mt-2 p-5 text-info rounded content-background">

                            <LoadCars/>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Cars;