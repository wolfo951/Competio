import { InputLabel } from '@mui/material';
import TextField from '@mui/material/TextField';
import { useState } from 'react';
import { toast } from 'react-toastify';
import { SaveNewTournament } from '../services/backend/Tournaments';
import { GetLocalUserInfo } from '../services/local/LocalStorage';
import { Tournament } from '../types/Models/Tournament';

type Values = {
    title: string;
    address: string;
    isPrivate: boolean;
    startsAt: string;
};

function CreateTournament(props: { onSave: any }) {
    const [values, setValues] = useState<Values>({
        title: '',
        address: '',
        isPrivate: false,
        startsAt: Date.now().toString()
    });

    const user = GetLocalUserInfo();

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setValues({ ...values, [event.target.name]: event.target.value });
    };

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const tournament: Tournament = {
            isPrivate: values.isPrivate,
            address: values.address,
            games: Array(0),
            players: Array(0),
            startsAt: values.startsAt,
            title: values.title,
            tournamentReferee: '0',
            pkTournamentId: 0,
            tournamentRefereeNavigation: null
        };

        toast.info('Saving tournament');
        SaveNewTournament(tournament).catch(() => toast.error('ups i did it again. sorry'));
    };

    return (
        <div>
            <h2>Add new tournament</h2>
            <form onSubmit={handleSubmit}>
                <TextField onChange={handleChange} style={{ margin: '5px' }} label="Tournament name" name="tournament" variant="filled" />
                <br />
                <TextField onChange={handleChange} style={{ margin: '5px' }} helperText="Is tournament private" name="is_private" type="checkbox" variant="filled" />
                <br />
                <TextField onChange={handleChange} style={{ margin: '5px' }} name="startsAt" type="date" variant="filled" />
                <br />
                <TextField onChange={handleChange} style={{ margin: '5px' }} label="Address" name="address" variant="filled" />
                <br />
                <button type="submit">Submit new tournament</button>
            </form>
        </div>
    );
}

export default CreateTournament;
