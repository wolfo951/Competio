export interface UserData {
    pkUserId: number;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    role: UserRole;
}

export enum UserRole {
    Administrator = 'administrator',
    Moderator = 'moderator',
    Player = 'player'
}
