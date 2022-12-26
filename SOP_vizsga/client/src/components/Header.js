import { Link } from "react-router-dom";

function Header({states}) {
    return(
        <div className="header_section nav-body">
         <ul className="nav justify-content-center">
            {states.getIsLoggedIn? 
            <li className="nav-item">
               <Link className="nav-link nav-button" to={'/'}>HOME</Link>
            </li> : null
            }
            {states.getIsLoggedIn? 
            <li className="nav-item">
               <a className="nav-link nav-button menu-off">ABOUT US</a>
            </li> : null
            }
            {states.getIsLoggedIn? 
            <li className="nav-item">
               <Link className="nav-link nav-button" to={'/cars'}>CARS</Link>
            </li> : null
            }
            {states.getIsLoggedIn? 
            <li className="nav-item">
               <Link className="nav-link nav-button" to={'/profile'}>MY PROFILE</Link>
            </li> : null
            }
            {states.getIsLoggedIn? 
            <li className="nav-item">
               <Link className="nav-link nav-button" to={'/logout'}>LOGOUT</Link>
            </li> : null
            }
            {!states.getIsLoggedIn? 
            <li className="nav-item">
               <Link className="nav-link nav-button" to={'/login'}>LOGIN</Link>
            </li> : null
            }
            {!states.getIsLoggedIn? 
            <li className="nav-item">
               <Link className="nav-link nav-button" to={'/register'}>REGISTER</Link>
            </li> : null
            }
            
         </ul>
      </div>
    )
}

export default Header;