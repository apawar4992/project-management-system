import ProductsComponent from './ProductsComponent';
import user from '../Images/User-avatar.png';
import toggleIcon from '../Images/Toggle.png';
import { Link } from 'react-router-dom'
import '../Css/home-component.css';
import { useState } from 'react';

const HomeComponent = () => {

    const[isTrue, setIsTrue] = useState(false);

    const toggleClass = () => {
       if(isTrue === true)
       {
        setIsTrue(false);
        return false;
       }
       else
       {
        setIsTrue(true);
        return true;
       }
    }
    
    const logOut = () =>{
        alert('Logged out successfully.');
    }

    const isLoggedIn = ()=>{
        return false;
    }

    return(
        <div className='menu-container'>
           <div className='navbar-container'>
              <div className='navbar-right'>
                <div className='navbar-right__left'>
                    <div>
                  <Link to="/Home" style={{ visibility: 'visible', display: 'inline-block'}}>
                       <button className='navbar-right-login__btn'>
                          Home
                       </button>
                   </Link>
                   <Link to="/About" style={{ visibility: 'visible' }}>
                       <button className='navbar-right-login__btn'>
                          About
                       </button>
                   </Link>
                   </div>
                   <div className='navbar-right__right'>
                   <Link to="/Login" style={{ visibility: isLoggedIn() ? 'hidden' : 'visible' }}>
                            <button className='navbar-right-login__btn'>
                                Login
                            </button>
                   </Link>
                   <div className='account-profile'>
                            <div onClick={() => toggleClass()} 
                            className='account-profile-icon'>
                                <img className='account-profile-user-icon' src={user} alt="user Icon" />
                                <img className='account-profile-toggle-icon' src={toggleIcon} alt="toggle Icon"></img>
                            </div>
                            <div className='account-profile-container' 
                             style={{ visibility: isTrue ? 'visible' : 'hidden' }}
                            >
                                <ul className='account-profile-items'>
                                    <li>
                                        {
                                                <Link className='account-profile-items__profile' to="/Profile">Profile</Link>
                                        }
                                    </li>
                                    <li>
                                        <Link onClick={() => logOut()} className='account-profile-items__logout'>
                                            Log Out
                                        </Link>
                                    </li>
                                </ul>
                            </div>
                   </div>
                   </div>
                   </div>
               </div>
           </div>

            <ProductsComponent/>
        </div>
    )
}

export default HomeComponent;