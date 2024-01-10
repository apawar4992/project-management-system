import { Suspense } from 'react';
import './App.css';
import HomeComponent from './Components/HomeComponent';
import LoginUser from './Components/LoginUser';
import RegisterUser from './Components/RegisterUser';
import UserProfile from './Components/ProfileComponent';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import About from './Components/AboutComponent';

function App() {
  return (
    <BrowserRouter>
    <div>
      <Routes>
      <Route path='/*' element={<HomeComponent />} />
      <Route path='/About' element={<Suspense fallback={<h4 style={{ padding: 50 }}>Loading...</h4>}>
                <About />
              </Suspense>} />
      <Route path='/Login' element={<Suspense fallback={<h4 style={{ padding: 50 }}>Loading...</h4>}>
                <LoginUser />
              </Suspense>} />
      <Route path='/Register' element={<Suspense fallback={<h4 style={{ padding: 50 }}>Loading...</h4>}>
                <RegisterUser />
              </Suspense>} />
      <Route path='/Profile' element={<Suspense fallback={<h4 style={{ padding: 50 }}>Loading...</h4>}>
                <UserProfile />
              </Suspense>} />
      </Routes>
    </div>
    </BrowserRouter>
  );
}

export default App;
