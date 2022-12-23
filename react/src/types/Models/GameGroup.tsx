import { Game } from './Game';
import { Score } from './Score';

export interface GameGroup {
    pkGroupId: number;
    FkGroupParentId: number;
    NextStagePlayerCount: number;
    GroupName: string;
    IsFinished: boolean;
    fkGameId: number;

    Game: Game;
    GameGroup: GameGroup;
    InverseFkGroupParent: Array<GameGroup>;
    Scores: Array<Score>;
}
