import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import {useEffect, useState} from 'react';
// import { useCookies } from 'react-cookie';
import Fetch from './core/Fetch';
import Auth from './core/Auth';
import Header from './components/Header';
// import Login from './components/Login';
import Home from './components/Home';
import Login from './components/Login';
import Logout from "./components/Logout";
import Register from "./components/Register";
import Profile from "./components/Profile";
import Cars from "./components/Cars";
import Payment from "./components/Payment";



function App( {states} ) {

    useEffect(() => {
        
        if (!states.isLoggedIn)
        {
            Auth(states, states.cookies.token);
        }
    }, [states.isLoggedIn])



    return (
        <div className="App">
            {/* <Header />
            <RouterProvider router={router} /> */}
            {/* <Login /> */}
            {/* {getComponent == 'Login'? <Login/> : null} */}
            <Router>
                <div id="cover-spin"></div>
                <Header states={states} />
                <Routes>
                    <Route path="/" element={<Home states={states}/>} />
                    <Route path="/logout" element={<Logout states={states}/>} />
                    <Route path="/login" element={<Login states={states} />} />
                    <Route path="/register" element={<Register states={states} />} />
                    <Route path="/profile" element={<Profile states={states} />} />
                    <Route path="/cars" element={<Cars states={states} />} />
                    <Route path="/payment" element={<Payment states={states} />} />
                </Routes>
            </Router>
        </div>
    );
}

export default App;
