import { GameGroup } from './GameGroup';
import { GameProperty } from './GameProperty';
import { GameType } from './GameType';
import { Tournament } from './Tournament';

export interface Game {
    pkGameId: number;
    gameTitle: string;
    pointsForScore: number;
    pointsForFirst: number;
    pointsForLast: number;
    isTemplate: boolean;
    fkTournamentId: number;
    fkGameTypeId: number;
    fkGameType: GameType;
    FkTournament: Tournament;
    gameGroups: Array<GameGroup>;
    gameProperties: Array<GameProperty>;
}
