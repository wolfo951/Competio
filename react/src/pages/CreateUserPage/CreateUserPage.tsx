import { TextField } from '@mui/material';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import TryAndGetSession from '../../services/backend/Session';
import { CreateNewUser } from '../../services/backend/UserController';

type Values = {
    email: string;
    firstName: string;
    lastName: string;
    username: string;
    password: string;
    role: string;
};

const CreateUserPage = () => {
    const [values, setValues] = useState<Values>({ email: '', username: '', password: '', firstName: '', lastName: '', role: '' });

    let navigator = useNavigate();
    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setValues({ ...values, [event.target.name]: event.target.value });
    };
    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        CreateNewUser(values)
            .then((res) =>
                TryAndGetSession(values.username, values.password).then(() => {
                    navigator('/');
                    window.location.reload();
                })
            )
            .catch((err) => {
                toast.error('Username/email already exists');
            })
            .finally(() => {
                console.log('Creation done');
            });
        console.log(values);
    };

    return (
        <div>
            <h2>Create User</h2>
            <form onSubmit={handleSubmit}>
                <TextField onChange={handleChange} label="email" name="email" />
                <TextField onChange={handleChange} label="username" name="username" />
                <TextField onChange={handleChange} label="Password" name="password" type="password" />
                <TextField onChange={handleChange} label="firstName" name="firstName" />
                <TextField onChange={handleChange} label="lastName" name="lastName" />
                <TextField onChange={handleChange} label="role" name="role" />
                <button type="submit">Create user</button>
            </form>
        </div>
    );
};

export default CreateUserPage;
