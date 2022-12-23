import axios from 'axios';
import environment from '../../config/environment.json';
import { Tournament } from '../../types/Models/Tournament';
import AuthorizedCall from './AuthorizedCall';

export async function GetAllTournaments() {
    const response = await AuthorizedCall.get<Tournament[]>(`${environment.backendUrl}/api/tournament`);
    return response;
}

export async function GetAllPublicTournaments() {
    const response = await axios.get<Tournament[]>(`${environment.backendUrl}/api/publictournament`);
    return response.data;
}

export async function GetTournamentDetails(id: string) {
    const response = await AuthorizedCall.get<Tournament>(`${environment.backendUrl}/api/tournament/${id}`);
    return response;
}

export async function SaveNewTournament(input: Tournament) {
    console.log(input);
    const response = await AuthorizedCall.post<Tournament>(`${environment.backendUrl}/api/tournament`, input);
    return response;
}

export async function AssignUserToTournament(tournamentId: number, userId: string) {
    const response = await AuthorizedCall.post<Tournament>(`${environment.backendUrl}/api/tournament/${tournamentId}/players`, [userId]);
    return response;
}

export async function RemoveUserToTournament(tournamentId: number, userId: string) {
    const response = await AuthorizedCall.delete<void>(`${environment.backendUrl}/api/tournament/${tournamentId}/players/${userId}`);
    return response;
}

type testing = {
    id: string;
    array: Tournament[];
};
