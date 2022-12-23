import './App.css';
import { Header } from './Components/Header/Header';
import { Route, Routes } from 'react-router-dom';
import HomePage from './pages/HomePage/HomePage';
import LoginPage from './pages/LoginPage/LoginPage';
import { UserPage } from './pages/UserPage/UserPage';
import TournamentsPage from './pages/TournamentsPage/TournamentsPage';
import TournamentPage from './pages/TournamentPage/TournamentPage';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import PublicTournamentsPage from './pages/PublicTournamentsPage/PublicTournamentsPage';
import CreateTournamentPage from './pages/CreateTournamentPage/CreateTournamentPage';
import CreateUserPage from './pages/CreateUserPage/CreateUserPage';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import { LocalizationProvider } from '@mui/x-date-pickers';

function App() {
    return (
        <div className="App">
            <Header />
            <LocalizationProvider dateAdapter={AdapterMoment}>
                <div style={{ margin: '15px' }}>
                    <Routes>
                        <Route path="/" element={<HomePage />} />
                        <Route path="login" element={<LoginPage />} />
                        <Route path="register" element={<CreateUserPage />} />
                        <Route path="user" element={<UserPage />} />
                        <Route path="tournaments" element={<TournamentsPage />} />
                        <Route path="tournament/:id" element={<TournamentPage />} />
                        <Route path="public-tournaments" element={<PublicTournamentsPage />} />
                        <Route path="create-tournament" element={<CreateTournamentPage />} />
                        <Route path="*" element={<div>404</div>} />
                    </Routes>
                </div>
                <ToastContainer position="bottom-left" autoClose={5000} hideProgressBar={false} newestOnTop={false} closeOnClick rtl={false} pauseOnFocusLoss draggable pauseOnHover />
            </LocalizationProvider>
            <footer style={{ position: 'absolute', left: '10px', bottom: 0, right: 0 }}>
                <p>Created by: Vilius Albrechtas</p>
            </footer>
        </div>
    );
}

export default App;
