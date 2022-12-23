import axios from 'axios';
import { Session } from '../../types/Models/Session';

class BackEndCall {
    private getToken() {
        const sessionString = localStorage.getItem('session') ?? '';
        const session: Session = JSON.parse(sessionString);
        return session.token;
    }

    public async get<T>(url: string): Promise<T> {
        const response = await axios.get<T>(url, { headers: { Authorization: `Bearer ${this.getToken()}` }, params: { private: true, public: true, userId: -1 } });
        return response.data;
    }
    public async post<T>(url: string, data: any): Promise<T> {
        const response = await axios.post<T>(url, data, { headers: { Authorization: `Bearer ${this.getToken()}` } });
        return response.data;
    }
    public async put<T>(url: string, data: any): Promise<T> {
        const response = await axios.put<T>(url, data, { headers: { Authorization: `Bearer ${this.getToken()}` } });
        return response.data;
    }
    public async delete<T>(url: string): Promise<T> {
        const response = await axios.delete<T>(url, { headers: { Authorization: `Bearer ${this.getToken()}` } });
        return response.data;
    }
}

const backEnd = new BackEndCall();
export default backEnd;
