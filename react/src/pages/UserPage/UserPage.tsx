import sessionHook from '../../services/local/GetSession';
import AdministratorPage from '../AdministratorPage/AdministratorPage';

export const UserPage = () => {
    const user = sessionHook.getSession();
    return (
        <div>
            <h1>User Page</h1>
            <h2>Hi, {user?.username}</h2>
            <p>Email: {user?.email ?? 'no email provided'}</p>
            <p>Role: {user?.role ?? 'no role provided'}</p>

            {user?.role === 'administrator' && <AdministratorPage />}
        </div>
    );
};
