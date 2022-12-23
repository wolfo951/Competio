import axios from 'axios';
import environment from '../../config/environment.json';
import { Session } from '../../types/Models/Session';
import sessionHook from '../local/GetSession';

const TryAndGetSession = async (username: string, password: string) => {
    const response = await axios.post<Session>(`${environment.backendUrl}/api/session`, { username, password });
    sessionHook.setSession(response.data);
};

export default TryAndGetSession;
