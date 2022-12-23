import { Session } from '../../types/Models/Session';

class SessionController {
    public getSession(): Session | undefined {
        const sessionKey = localStorage.getItem('session');
        if (sessionKey === null) return undefined;
        return JSON.parse(sessionKey);
    }
    public setSession(session: Session): void {
        localStorage.setItem('session', JSON.stringify(session));
    }
    public removeSession(): void {
        localStorage.removeItem('session');
    }
}

const sessionHook: SessionController = new SessionController();

export default sessionHook;
