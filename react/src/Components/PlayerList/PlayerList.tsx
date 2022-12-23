import { Table, TableHead, TableRow, TableCell, TableBody } from '@mui/material';
import { useMemo } from 'react';
import { toast } from 'react-toastify';
import { AssignUserToTournament, RemoveUserToTournament } from '../../services/backend/Tournaments';
import sessionHook from '../../services/local/GetSession';
import { Player } from '../../types/Models/Player';
import { Tournament } from '../../types/Models/Tournament';

const PlayerList = (props: { players: Player[] | undefined; tournament: Tournament; update: React.Dispatch<React.SetStateAction<boolean>> }) => {
    const session = sessionHook.getSession();
    const assignForTournament = () => {
        if (session === null || session?.pkUserId === undefined) return;
        AssignUserToTournament(props.tournament.pkTournamentId, session?.pkUserId)
            .then(() => {
                toast.success('You have been assigned to this toursnament!');
                props.update((prev) => !prev);
            })
            .catch((err) => toast.error(err.response.data))
            .finally(() => console.log('done'));
    };

    const removeForTournament = () => {
        if (session === null || session?.pkUserId === undefined) return;
        RemoveUserToTournament(props.tournament.pkTournamentId, session?.pkUserId)
            .then(() => {
                toast.success('You have been removed from this toursnament!');
                props.update((prev) => !prev);
            })
            .catch((err) => toast.error(err.response.data))
            .finally(() => console.log('done'));
    };

    const button = useMemo(() => {
        if (props.players === undefined) return <></>;
        const userId: number | undefined = session === undefined ? -1 : parseInt(session?.pkUserId);
        if (props.players.find((player) => player.fkUserId === userId) === undefined) {
            return <button onClick={assignForTournament}>Assign for this tournament</button>;
        } else return <button onClick={removeForTournament}>Remove from this tournament</button>;
    }, [props.players, session?.pkUserId]);

    if (props.players === undefined) return <div>No players found.. sad..</div>;
    return (
        <div>
            <>This is the ammount of players: {props.players.length}</>
            <p></p>
            {button}
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>FirstName</TableCell>
                        <TableCell>LastName</TableCell>
                        <TableCell>Email</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {props.players.map((player) => (
                        <TableRow key={player.pkPlayerId}>
                            <TableCell>{player.fkUser.firstName}</TableCell>
                            <TableCell>{player.fkUser.lastName}</TableCell>
                            <TableCell>{player.fkUser.email}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </div>
    );
};

export default PlayerList;
