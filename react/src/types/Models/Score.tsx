import { GameGroup } from './GameGroup';
import { Player } from './Player';

export interface Score {
    PkScoreId: number;
    Score1: number;
    FkGameGroupId: number;
    FkPlayerId: number;

    FkGameGroup: GameGroup;
    FkPlayer: Player;
}
