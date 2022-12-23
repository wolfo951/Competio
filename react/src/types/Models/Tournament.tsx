import { Game } from './Game';
import { Player } from './Player';

export interface Tournament {
    pkTournamentId: number;
    title: string;
    startsAt: string;
    address: string;
    isPrivate: boolean;
    tournamentReferee: string;
    players: Array<Player>;
    tournamentRefereeNavigation: Player | null;
    games: Array<Game>;
}
