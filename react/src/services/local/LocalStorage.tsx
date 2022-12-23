import { UserData } from '../../types/Models/UserData';
import config from '../../config/localStorage.json';

export function GetLocalUserInfo(): UserData | null {
    const userString = localStorage.getItem(config.userDataKey);
    const user: UserData | null = userString ? JSON.parse(userString) : null;
    return user;
}

export function SetLocalUserInfo(userInfo: UserData) {
    localStorage.setItem(config.userDataKey, JSON.stringify(userInfo));
}

export function RemoveLocalUserInfo() {
    localStorage.removeItem(config.userDataKey);
}
