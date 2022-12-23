import { CheckBox } from '@mui/icons-material';
import { Button, FormControlLabel, MenuItem, TextField } from '@mui/material';
import { DateTimePicker } from '@mui/x-date-pickers';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import { SaveNewTournament } from '../../services/backend/Tournaments';
import sessionHook from '../../services/local/GetSession';
import { Tournament } from '../../types/Models/Tournament';
import { UserRole } from '../../types/Models/UserData';

type Values = {
    title: string;
    startDate: Date;
    address: string;
    isPrivate: boolean;
};

const CreateTournament = () => {
    const [values, setValues] = useState<Values>({ title: '', startDate: new Date(), address: '', isPrivate: true });
    const session = sessionHook.getSession();
    let navigate = useNavigate();

    if (session === undefined || session.role === UserRole.Player) {
        navigate('/not-found', { replace: true });
        return <></>;
    }

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setValues({ ...values, [event.target.name]: event.target.value });
    };
    const handleChangePrivate = (event: React.ChangeEvent<HTMLInputElement>) => {
        setValues({ ...values, isPrivate: event.target.value === 'true' ? true : false });
    };
    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const tourn: Tournament = {
            pkTournamentId: 0,
            title: values.title,
            games: [],
            isPrivate: values.isPrivate,
            address: values.address,
            players: [],
            startsAt: values.startDate.toJSON(),
            tournamentRefereeNavigation: null,
            tournamentReferee: session.pkUserId
        };
        SaveNewTournament(tourn)
            .then((res) => {
                console.log(res);
                toast.success('Created tournament');
                navigate('/tournaments', { replace: true });
            })
            .catch((err) => {
                console.log('err', err);
                toast.error('Failed to create tournament');
            })
            .finally(() => {
                console.log('Creation done');
            });
    };

    return (
        <div>
            <h2>Create Tournament</h2>
            <form onSubmit={handleSubmit}>
                <TextField onChange={handleChange} label="Title" name="title" />
                <DateTimePicker
                    label="Date Time picker"
                    onChange={(e) => {
                        if (e !== null) setValues({ ...values, startDate: e });
                    }}
                    renderInput={(params) => <TextField {...params} />}
                    value={values.startDate}
                />
                <TextField onChange={handleChange} label="Address" name="address" />
                <TextField onChange={handleChangePrivate} defaultValue={'true'} select label="Is Private" name="isPrivate">
                    <MenuItem key={'private'} value={'true'}>
                        Private event
                    </MenuItem>
                    <MenuItem key={'public'} value={'false'}>
                        Public event
                    </MenuItem>
                </TextField>
                <p />
                <button type="submit">Create tournament</button>
            </form>
        </div>
    );
};

export default CreateTournament;
