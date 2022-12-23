import { TextField } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import TryAndGetSession from '../../services/backend/Session';
import sessionHook from '../../services/local/GetSession';
import { toast } from 'react-toastify';

type Values = {
    username: string;
    password: string;
};

const LoginPage = () => {
    let navigate = useNavigate();
    const [values, setValues] = useState<Values>({
        username: '',
        password: ''
    });

    const [session, setSession] = useState(sessionHook.getSession());
    useEffect(() => {
        if (session !== undefined) {
            navigate('/user', { replace: true });
            window.location.reload();
        }
    }, [session]);

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setValues({ ...values, [event.target.name]: event.target.value });
    };

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        TryAndGetSession(values.username, values.password)
            .then(() => {
                const sess = sessionHook.getSession();
                setSession(sess);
                navigate('/', { replace: true });
                window.location.reload();
            })
            .catch((err) => {
                if (err.response.status === 401) {
                    toast.error('Invalid username or password');
                    return;
                }
                toast.warning('Something went wrong, please try again later (more info in console)');
                console.log('err', err);
            });
    };

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <TextField onChange={handleChange} label="username" name="username" />
                <TextField onChange={handleChange} label="Password" name="password" type="password" />
                <button type="submit">Login</button>
            </form>
        </div>
    );
};

export default LoginPage;
