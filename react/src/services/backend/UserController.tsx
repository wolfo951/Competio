import axios from 'axios';
import environment from '../../config/environment.json';
import { UserData } from '../../types/Models/UserData';
import AuthorizedCall from './AuthorizedCall';

export async function GetAllUserInfo() {
    const response = await AuthorizedCall.get<UserData[]>(`${environment.backendUrl}/api/users`);
    return response;
}

export async function UpdateUserRole(userData: UserData) {
    const response = await AuthorizedCall.put<void>(`${environment.backendUrl}/api/users/${userData.pkUserId}`, userData);
    return response;
}

export async function CreateNewUser(input: any) {
    const response = await axios.post(`${environment.backendUrl}/api/users`, input);
    return response.data;
}
