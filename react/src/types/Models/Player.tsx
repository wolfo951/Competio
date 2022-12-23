import { UserData } from './UserData';

export interface Player {
    pkPlayerId: number;
    fkUserId: number;
    fkTournamentId: number;
    fkUser: UserData;
}
